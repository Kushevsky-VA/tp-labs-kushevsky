using System;

namespace ExpenseTracker.Core;

public class Expense
{
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }

    public Expense(DateTime date, string category, decimal amount)
    {
        Date = date;
        Category = category;
        Amount = amount;
    }
}