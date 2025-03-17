using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Dynamic;

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
                    string[] index = GetIndex(received);
                    for (int i = 0; i < index.Length; i++)
                    {
                        Console.WriteLine(index[i]);
                    }

                    switch(index[0])
                    {
                        case "reg": TCPClientProcessing.RegistrationClient(index[1], index[2], client);break;
                        case "avt": TCPClientProcessing.AuthorizationTcpClient(index[1],client); break;//проверка по никнейму и возврат из бд id
                        case "mes": TCPClientProcessing.sendMessage(index[1]); break;
                            
                    }

                    //TCPClientProcessing.Message=received;
                    //string response = "Ответ от сервера: " + received.ToUpper();
                    //byte[] data = Encoding.UTF8.GetBytes(received);
                    //stream.Write(data, 0, data.Length);
                    
                    //JSONProcessing.SaveMessageTojsonFile(received); //перенести в др функцию

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"YОшибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("HandleClient:отключение клиента");
                client.Close();
            }
        }

        public static string[] GetIndex(string str)
        {
            string[] tmp = str.Split( new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            return tmp;
        }



        public static void Start(string ipAdress, int port)
        {
            SQLiteProcessing.Start(); //проверка наличия БД
            Thread delclientnonconnect = new Thread(() =>  TCPClientProcessing.DelNonConnectTcp());
            delclientnonconnect.Start();

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
