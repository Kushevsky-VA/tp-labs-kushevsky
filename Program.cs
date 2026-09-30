using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab3
{
    
    public class FileSplitter
    {
       

        public static void SplitFile(string sourceFilePath, long partSizeBytes)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException($"Исходный файл не найден: {sourceFilePath}");

            if (partSizeBytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(partSizeBytes), "Размер части должен быть больше 0.");

            string directory = Path.GetDirectoryName(sourceFilePath) ?? ".";
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourceFilePath);
            string extension = Path.GetExtension(sourceFilePath);

            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[partSizeBytes];
                int partNumber = 1;
                int bytesRead;

                while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                   
                    string partName = $"{fileNameWithoutExt}.{partNumber:D3}{extension}";
                    string partPath = Path.Combine(directory, partName);

                    using (var partStream = new FileStream(partPath, FileMode.Create, FileAccess.Write))
                    {
                        partStream.Write(buffer, 0, bytesRead);
                    }

                    Console.WriteLine($"Создана часть: {partName} ({bytesRead} байт)");
                    partNumber++;
                }
            }
        }

        

        public static void MergeFiles(string sourceDirectory, string baseFileName, string outputFilePath)
        {
            if (!Directory.Exists(sourceDirectory))
                throw new DirectoryNotFoundException($"Папка не найдена: {sourceDirectory}");


            var parts = Directory.GetFiles(sourceDirectory, $"{baseFileName}.*")
                                 .OrderBy(f => f)
                                 .ToList();

            if (parts.Count == 0)
                throw new FileNotFoundException("Не найдено ни одной части для склейки.");

            using (var outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            {
                foreach (var part in parts)
                {
                    using (var partStream = new FileStream(part, FileMode.Open, FileAccess.Read))
                    {
                        partStream.CopyTo(outputStream);
                    }
                    Console.WriteLine($"Добавлена часть: {Path.GetFileName(part)}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Лабораторная работа 3. Вариант 12: Разрезание файла ===");
                Console.WriteLine("1. Разрезать файл");
                Console.WriteLine("2. Склеить файл");
                Console.WriteLine("3. Проверить размеры");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите путь к исходному файлу: ");
                            string source = Console.ReadLine();
                            Console.Write("Введите размер части в байтах (например, 1048576 для 1 МБ): ");
                            if (long.TryParse(Console.ReadLine(), out long size))
                            {
                                FileSplitter.SplitFile(source, size);
                                Console.WriteLine("Разрезание завершено.");
                            }
                            else
                            {
                                Console.WriteLine("Некорректный размер.");
                            }
                            break;

                        case "2":
                            Console.Write("Введите папку с частями: ");
                            string dir = Console.ReadLine();
                            Console.Write("Введите базовое имя файла (без номера части, например 'myfile.txt'): ");
                            string baseName = Console.ReadLine();
                            Console.Write("Введите путь для сохранения итогового файла: ");
                            string output = Console.ReadLine();
                            FileSplitter.MergeFiles(dir, baseName, output);
                            Console.WriteLine("Склейка завершена.");
                            break;

                        case "3":
                            Console.Write("Введите путь к исходному файлу: ");
                            string original = Console.ReadLine();
                            Console.Write("Введите путь к склеенному файлу: ");
                            string merged = Console.ReadLine();

                            if (File.Exists(original) && File.Exists(merged))
                            {
                                var info1 = new FileInfo(original);
                                var info2 = new FileInfo(merged);
                                Console.WriteLine($"Размер оригинала: {info1.Length} байт");
                                Console.WriteLine($"Размер склейки:   {info2.Length} байт");
                                Console.WriteLine(info1.Length == info2.Length ? "✅ Размеры совпадают!" : "❌ Размеры НЕ совпадают!");
                            }
                            else
                            {
                                Console.WriteLine("Один из файлов не найден.");
                            }
                            break;

                        case "0":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
