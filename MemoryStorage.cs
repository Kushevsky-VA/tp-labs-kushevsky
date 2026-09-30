using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Core;

public class ExpenseManager
{
    private readonly List<Expense> _expenses = new();
    private readonly IStorage _storage;

    // Внедрение зависимости через конструктор (требование №3)
    public ExpenseManager(IStorage storage)
    {
        _storage = storage;
        _expenses = _storage.Load();
    }

    public void AddExpense(DateTime date, string category, decimal amount)
    {
        _expenses.Add(new Expense(date, category, amount));
        _storage.Save(_expenses); // Автосохранение при добавлении
    }

    public decimal GetTotalForPeriod(DateTime start, DateTime end)
    {
        return _expenses
            .Where(e => e.Date >= start && e.Date <= end)
            .Sum(e => e.Amount);
    }

    public IEnumerable<IGrouping<string, Expense>> GetTopCategories(int count)
    {
        return _expenses
            .GroupBy(e => e.Category)
            .OrderByDescending(g => g.Sum(e => e.Amount))
            .Take(count);
    }

    public IReadOnlyList<Expense> GetAll() => _expenses;
}