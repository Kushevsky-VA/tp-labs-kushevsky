using ExpenseTracker.Core;
using Xunit;

namespace ExpenseTracker.Tests;

public class ExpenseManagerTests
{

    [Fact]
    public void AddExpense_ShouldIncreaseCount()
    {
        var manager = new ExpenseManager(new MemoryStorage());
        manager.AddExpense(DateTime.Now, "Еда", 100);
        Assert.Single(manager.GetAll());
    }


    [Fact]
    public void GetTotalForPeriod_ShouldSumOnlyInRange()
    {
        var manager = new ExpenseManager(new MemoryStorage());
        manager.AddExpense(new DateTime(2023, 1, 1), "Еда", 100);
        manager.AddExpense(new DateTime(2023, 1, 15), "Транспорт", 50);
        manager.AddExpense(new DateTime(2023, 2, 1), "Еда", 200);

        var total = manager.GetTotalForPeriod(new DateTime(2023, 1, 1), new DateTime(2023, 1, 31));

        Assert.Equal(150, total);
    }

 
    [Fact]
    public void GetTopCategories_ShouldReturnCorrectOrder()
    {
        var manager = new ExpenseManager(new MemoryStorage());
        manager.AddExpense(DateTime.Now, "Еда", 100);
        manager.AddExpense(DateTime.Now, "Транспорт", 500);
        manager.AddExpense(DateTime.Now, "Еда", 50);

        var top = manager.GetTopCategories(1).ToList();

        Assert.Single(top);
        Assert.Equal("Транспорт", top[0].Key);
    }


    [Fact]
    public void GetTotalForPeriod_EmptyList_ReturnsZero()
    {
        var manager = new ExpenseManager(new MemoryStorage());
        var total = manager.GetTotalForPeriod(DateTime.MinValue, DateTime.MaxValue);
        Assert.Equal(0, total);
    }

   
    [Fact]
    public void MemoryStorage_SaveAndLoad_Works()
    {
        var storage = new MemoryStorage();
        var manager = new ExpenseManager(storage);
        manager.AddExpense(DateTime.Now, "Тест", 999);

        // Создаем новый менеджер с тем же хранилищем
        var manager2 = new ExpenseManager(storage);
        Assert.Single(manager2.GetAll());
        Assert.Equal(999, manager2.GetAll()[0].Amount);
    }

  
    [Fact]
    public void GetTopCategories_CountMoreThanExists_ReturnsAll()
    {
        var manager = new ExpenseManager(new MemoryStorage());
        manager.AddExpense(DateTime.Now, "A", 10);
        manager.AddExpense(DateTime.Now, "B", 20);

        var top = manager.GetTopCategories(5).ToList();
        Assert.Equal(2, top.Count);
    }


    [Fact]
    public void AddExpense_CheckCategory()
    {
        var manager = new ExpenseManager(new MemoryStorage());
        manager.AddExpense(DateTime.Now, "Развлечения", 300);
        Assert.Equal("Развлечения", manager.GetAll()[0].Category);
    }


    [Fact]
    public void GetTotalForPeriod_WithNegativeAmounts()
    {
        var manager = new ExpenseManager(new MemoryStorage());
        manager.AddExpense(DateTime.Now, "Долг", -100);
        manager.AddExpense(DateTime.Now, "Зарплата", 500);

        var total = manager.GetTotalForPeriod(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
        Assert.Equal(400, total);
    }
}