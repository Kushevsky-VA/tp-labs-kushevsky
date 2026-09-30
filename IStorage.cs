using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Core;

public class MemoryStorage : IStorage
{
    private List<Expense> _expenses = new();

    public void Save(IEnumerable<Expense> expenses)
    {
        _expenses = expenses.ToList();
    }

    public List<Expense> Load()
    {
        return _expenses;
    }
}