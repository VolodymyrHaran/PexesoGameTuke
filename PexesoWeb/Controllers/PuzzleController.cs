using Microsoft.AspNetCore.Mvc;
using PexesoCore;
using PexesoCore.Service;
using PexesoCore.Core;
using PexesoCore.Entity;
using PexesoWeb.Models;

namespace PexesoWeb.Controllers;

[Route("[controller]")]
public class PuzzleController : Controller
{

    // GET
    private const string FieldSessionKey = "field";
    private ITimeService timeService = new TimeServiceEF();
    private ICommentService commentService = new CommentService();
    private IRatingService ratingService = new RatingService();
    
    [HttpGet]
    public IActionResult Index()
    {
        Field field = new (4, 7);
        HttpContext.Session.SetObject(FieldSessionKey,field);
        return View("Index",CreateModel());
    }
    [HttpGet("Comment")]
    public IActionResult Comment()
    {
        return View("Comment");
    }
    
    [HttpGet("OpenCard")]
    public IActionResult OpenCard(int card)
    {
        var field = (Field)HttpContext.Session.GetObject(FieldSessionKey);
        Card getCard = field.GetCardByIndex(card);
        if (!getCard.Shown)
        {
            if (field.firstCard == null)
            {
                field.firstCard = getCard;
                field.OpenCard(getCard);
            }
            else if(field.secondCard==null)
            {
                if(getCard!=field.firstCard)
                {
                    field.secondCard = getCard;
                    field.OpenCard(getCard);
                }
            }
            
       
            if (field.CheckWin())
            {
                Time time = new Time
                {
                    PlayedTime = field.GetTime(), PlayerName = field.nick, PlayedAt = DateTime.UtcNow
                };
                timeService.AddTime(time);
            }
            HttpContext.Session.SetObject(FieldSessionKey,field);
        }
        return View("Index", CreateModel());
    }

    [HttpGet("OpenRandomPair")]
    public IActionResult OpenRandomPair()
    {
        var field = (Field)HttpContext.Session.GetObject(FieldSessionKey);
        foreach (var card in field.GetCards())
        {
            if (!card.Shown)
            {
                field.OpenCard(card);
                card.IsGuessed = true;
                foreach (var secondCard in field.GetCards())
                {
                    if (secondCard.Id == card.Id)
                    {
                        field.OpenCard(secondCard);
                        secondCard.IsGuessed = true;
                    }
                }
                if (field.CheckWin())
                {
                    Time time = new Time
                    {
                        PlayedTime = field.GetTime(), PlayerName = field.nick, PlayedAt = DateTime.UtcNow
                    };
                    timeService.AddTime(time);
                }
                break;
            }
        }
        HttpContext.Session.SetObject(FieldSessionKey,field);
        return View("Index", CreateModel());
    }

    private PuzzleModel CreateModel()
    {
        Field field = (Field)HttpContext.Session.GetObject(FieldSessionKey);
        var times = timeService.GetTopTime();
        var comments = commentService.GetAllComments();
        var rating = ratingService.GetAllRatings();

        return new PuzzleModel { Field = field, Times = times, Comments = comments,Ratings = rating};
    }
    
    [HttpPost]
    public IActionResult Index(string nickname)
    {
        var field = (Field)HttpContext.Session.GetObject(FieldSessionKey);
        field.nick = nickname;
        field.StartTime();
        field.FirstTimePlay = false;
        HttpContext.Session.SetObject(FieldSessionKey,field);
        return View("Index", CreateModel());
    }
    
    
    [HttpGet("Check")]
    public IActionResult Check()
    {
        var field = (Field)HttpContext.Session.GetObject(FieldSessionKey);
        var state = field.CheckCards();
        if (state == false)
        {
            foreach (var card in field.GetCards())
            {
                if(field.firstCard.Index==card.Index) field.CloseCard(card);
                if(field.secondCard.Index==card.Index) field.CloseCard(card);
            }
        }
        else
        {
            foreach (var card in field.GetCards())
            {
                if (field.firstCard.Index == card.Index) card.IsGuessed = true;
                if(field.secondCard.Index==card.Index) card.IsGuessed = true;;
            }
        }
        field.firstCard = null;
        field.secondCard = null;
        HttpContext.Session.SetObject(FieldSessionKey,field);
        return View("Index", CreateModel());
    }

    [HttpPost("Comment")]
    public IActionResult Comment(int rate,string comment)
    {
        var field = (Field)HttpContext.Session.GetObject(FieldSessionKey);
        
        commentService.AddComment(new Comment
        {
            PlayerComment = comment, PlayerName = field.nick
        });
        ratingService.AddRating(new Rating
        {
            PlayerName = field.nick, Rate = rate
        });
        return Index();
    }

}

