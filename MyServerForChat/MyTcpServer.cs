using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyServerForChat
{
    internal class MyTcpServer
    {
        private static void HandleClient(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Получено: {received}");
                    //TCPClientProcessing.Message=received;
                    //string response = "Ответ от сервера: " + received.ToUpper();
                    //byte[] data = Encoding.UTF8.GetBytes(received);
                    //stream.Write(data, 0, data.Length);
                    TCPClientProcessing.sendMessage(received);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        public static void Start(string ipAdress, int port)
        {
            TCPClientProcessing.tcpClients.Clear();
            TcpListener _tcpListener = new TcpListener(IPAddress.Parse(ipAdress), port);
            _tcpListener.Start();
            Console.WriteLine("сервер запущен, ожидаю подключения");

            //ожидаем подключения клиентов
            while (true)
            {
                try
                {
                    //принимаем подключение
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    TCPClientProcessing.tcpClients.Add(client);
                    //var ff= client.Client;
                    //IPEndPoint clientEndPoint = (IPEndPoint)ff.RemoteEndPoint;
                    Console.WriteLine("Клиент подключился");

                    //обрабатываем подключение в отдельном потоке
                    Thread _listenerThread = new Thread(() => HandleClient(client));
                    _listenerThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при подключении {ex.Message}");
                }
            }


        }


    }
}
