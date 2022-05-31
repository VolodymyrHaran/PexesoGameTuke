using System;
using NUnit.Framework;
using PexesoCore.Entity;
using PexesoCore.Service;
using PexesoCore.Core;


namespace PexesoTest;

public class Tests
{
    [Test]
    public void TimeEzTest()
    {
        var service = CreateService();
        service.AddTime(new Time{PlayerName = "Aspid",PlayedTime = 100,PlayedAt = DateTime.Now});
        Assert.AreEqual(1,service.GetTopTime().Count);
        Assert.AreEqual("Aspid",service.GetTopTime()[0].PlayerName);
        Assert.AreEqual(100,service.GetTopTime()[0].PlayedTime);
    }
    
    [Test]
    public void TimeEzTest3()
    {
        var service = CreateService();
        service.AddTime(new Time{PlayerName = "Aspid",PlayedTime = 100,PlayedAt = DateTime.Now});
        service.AddTime(new Time{PlayerName = "Kate",PlayedTime = 0,PlayedAt = DateTime.Now});
        service.AddTime(new Time{PlayerName = "Vova",PlayedTime = 9999,PlayedAt = DateTime.Now});
        Assert.AreEqual(3,service.GetTopTime().Count);
        
        Assert.AreEqual("Kate",service.GetTopTime()[0].PlayerName);
        Assert.AreEqual(0,service.GetTopTime()[0].PlayedTime);

        Assert.AreEqual("Aspid",service.GetTopTime()[1].PlayerName);
        Assert.AreEqual(100,service.GetTopTime()[1].PlayedTime);
        
        Assert.AreEqual("Vova",service.GetTopTime()[2].PlayerName);
        Assert.AreEqual(9999,service.GetTopTime()[2].PlayedTime);
    }
    [Test]
    public void ResetTests()
    {
        var service = CreateService();
        service.AddTime(new Time{PlayerName = "Aspid",PlayedTime = 100,PlayedAt = DateTime.Now});
        service.AddTime(new Time{PlayerName = "Kate",PlayedTime = 0,PlayedAt = DateTime.Now});
        service.ResetTime();
        Assert.AreEqual(0,service.GetTopTime().Count);
    }
    [Test]
    public void TopTime()
    {
        var service = CreateService();
        service.AddTime(new Time{PlayerName = "Aspid",PlayedTime = 100,PlayedAt = DateTime.Now});
        service.AddTime(new Time{PlayerName = "Kate",PlayedTime = 1,PlayedAt = DateTime.Now});
        Assert.AreEqual(2,service.GetTopTime().Count);
    }

    [Test]
    
    public void CheckNullField()
    {
        var field = CreateField();
        Assert.NotNull(field);
    }
    
    [Test]
    
    public void CheckWinNewField()
    {
        var field = CreateField();
        
        Assert.IsFalse(field.CheckWin());
    }
    
    [Test]
    public void CheckWinOpenAllCards()
    {
        var field = CreateField();
    
        foreach (var card in field.GetCards())
        {
            field.OpenCard(card);
        }
        
        Assert.IsTrue(field.CheckWin());
    }
    
    [Test]
    public void CheckWinOpenOneCard()
    {
        var field = CreateField();
    
        foreach (var card in field.GetCards())
        {
            field.OpenCard(card);
            break;
        }
        
        Assert.IsFalse(field.CheckWin());
    }
    
    [Test]
    public void OpenCard()
    {
        var field = CreateField();
    
        var card = field.GetCard(0, 0);
        field.OpenCard(0,0);
        
        Assert.IsTrue(card.Shown);
    }
    
    
    
    private Field CreateField()
    {
        return new Field(4,5);
    }
    private ITimeService CreateService()
    { 
        return new TimeService();
    }
}