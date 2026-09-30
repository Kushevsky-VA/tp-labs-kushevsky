using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
          
            Console.Write("Адрес сервера (Enter = localhost): ");
            string host = Console.ReadLine() is { Length: > 0 } h ? h : "localhost";

            
            Console.Write("Ваш ник: ");
            string nick = Console.ReadLine() ?? "anon";

            using var client = new TcpClient();

            try
            {
                
                await client.ConnectAsync(host, 5555);
            }
            catch (SocketException)
            {
                Console.WriteLine("Ошибка: Не удалось подключиться к серверу. Проверьте, запущен ли сервер и правильность адреса.");
                return;
            }

            var stream = client.GetStream();
            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream) { AutoFlush = true };

            
            while (true)
            {
                await writer.WriteLineAsync(nick);
                string? response = await reader.ReadLineAsync();

                if (response != null && response.StartsWith("ERROR:"))
                {
                    Console.WriteLine(response);
                    Console.Write("Введите другой ник: ");
                    nick = Console.ReadLine() ?? "anon";
                }
                else
                {
                    
                    break;
                }
            }

            
            _ = Task.Run(async () =>
            {
                try
                {
                    string? line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
                catch (IOException) { }
                Console.WriteLine("Соединение с сервером потеряно.");
            });

            Console.WriteLine("Подключено. Пишите сообщения, /exit — выход.");

            
            while (true)
            {
                string? msg = Console.ReadLine();
                if (msg == null) continue;

                await writer.WriteLineAsync(msg);
                if (msg == "/exit") break;
            }
        }
    }
}