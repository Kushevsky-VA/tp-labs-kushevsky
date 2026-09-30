using System.Collections.Generic;

namespace ExpenseTracker.Core;

public interface IStorage
{
    void Save(IEnumerable<Expense> expenses);
    List<Expense> Load();
}