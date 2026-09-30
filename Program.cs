using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

class ChatServer
{
    
    static Dictionary<string, StreamWriter> clients = new();
    static object lockObj = new object();
    static string logFile = "chat.log";

    static async Task Main(string[] args)
    {
        
        int port = args.Length > 0 ? int.Parse(args[0]) : 5555;

        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Log($"Сервер запущен на порту {port}");
        Console.WriteLine($"Сервер запущен на порту {port}. Ctrl+C - остановка.");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client); 
        }
    }

    static async Task HandleClientAsync(TcpClient client)
    {
        var endpoint = client.Client.RemoteEndPoint;
        var stream = client.GetStream();
        var reader = new StreamReader(stream);
        var writer = new StreamWriter(stream) { AutoFlush = true };

        string? nick = null;
        try
        {
            
            while (true)
            {
                nick = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(nick)) { client.Close(); return; }

                bool isBusy = false;
                lock (lockObj)
                {
                    if (clients.ContainsKey(nick))
                    {
                        isBusy = true;
                    }
                    else
                    {
                        clients.Add(nick, writer);
                    }
                }

                if (isBusy)
                {
                    await writer.WriteLineAsync("ERROR: Ник занят. Введите другой:");
                    continue;
                }
                break;
            }

            Log($"{nick} подключился ({endpoint})");
            await BroadcastAsync($"*** {nick} вошёл в чат ***", nick);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line == "/exit") break;

                
                if (line == "/list")
                {
                    string userList;
                    lock (lockObj)
                    {
                        userList = "Пользователи онлайн: " + string.Join(", ", clients.Keys);
                    }
                    await writer.WriteLineAsync(userList); 
                    continue;
                }

                
                if (line.StartsWith("/w "))
                {
                    string[] parts = line.Split(' ', 3);
                    if (parts.Length == 3)
                    {
                        string targetNick = parts[1];
                        string msg = parts[2];
                        await SendPrivateMessageAsync(nick, targetNick, msg);
                    }
                    else
                    {
                        await writer.WriteLineAsync("Ошибка формата. Используйте: /w ник текст");
                    }
                    continue;
                }

                
                string formattedMsg = $"[{DateTime.Now:HH:mm:ss}] {nick}: {line}";
                Log(formattedMsg);
                await BroadcastAsync(formattedMsg, nick);
            }
        }
        catch (IOException) { /* Клиент оборвал соединение */ }
        finally
        {
            
            if (nick != null)
            {
                lock (lockObj) clients.Remove(nick);
                client.Close();
                Log($"{nick} отключился");
                await BroadcastAsync($"*** {nick} покинул чат ***", nick);
            }
        }
    }

    
    static async Task BroadcastAsync(string message, string senderNick)
    {
        List<StreamWriter> snapshot;
        lock (lockObj) snapshot = new List<StreamWriter>(clients.Values);

        foreach (var w in snapshot)
        {
            try { await w.WriteLineAsync(message); }
            catch { /* Игнорируем ошибки отвалившихся */ }
        }
    }

    
    static async Task SendPrivateMessageAsync(string sender, string target, string message)
    {
        StreamWriter? targetWriter = null;
        lock (lockObj)
        {
            if (clients.ContainsKey(target)) targetWriter = clients[target];
        }

        if (targetWriter != null)
        {
            await targetWriter.WriteLineAsync($"[ЛС от {sender}]: {message}");
            Log($"[ЛС] {sender} -> {target}: {message}");
        }
        else
        {
            
            lock (lockObj)
            {
                if (clients.ContainsKey(sender))
                    clients[sender].WriteLineAsync($"Пользователь {target} не найден.");
            }
        }
    }

    
    static void Log(string message)
    {
        string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        Console.WriteLine(logLine);
        try { File.AppendAllText(logFile, logLine + Environment.NewLine); } catch { }
    }
}