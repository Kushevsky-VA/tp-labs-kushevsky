using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ExpenseTracker.Core;

public class FileStorage : IStorage
{
    private readonly string _filePath;

    public FileStorage(string filePath)
    {
        _filePath = filePath;
    }

    public void Save(IEnumerable<Expense> expenses)
    {
        var lines = expenses.Select(e => $"{e.Date:yyyy-MM-dd};{e.Category};{e.Amount.ToString(CultureInfo.InvariantCulture)}");
        File.WriteAllLines(_filePath, lines);
    }

    public List<Expense> Load()
    {
        if (!File.Exists(_filePath)) return new List<Expense>();

        return File.ReadAllLines(_filePath)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var parts = line.Split(';');
                return new Expense(
                    DateTime.Parse(parts[0]),
                    parts[1],
                    decimal.Parse(parts[2], CultureInfo.InvariantCulture)
                );
            })
            .ToList();
    }
}