using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MyServerForChat
{
    internal class TCPClientProcessing
    {
        /// <summary>
        /// лист с подключенными клиентами
        /// </summary>
        public static List<TcpClient> tcpClients = new List<TcpClient>();

        //public static string Message = string.Empty;


        
        /// <summary>
        /// РАССЫЛКА СООБЩЕНИЯ
        /// </summary>
        /// <param name="mes"></param>
        public static void sendMessage(string mes)
        {
            //byte[] data = Encoding.UTF8.GetBytes(JSONProcessing.GetJsonFile(mes));
            byte[] data = Encoding.UTF8.GetBytes(mes);
            foreach (TcpClient client in tcpClients)
            {
                 NetworkStream stream = client.GetStream();
                 stream.Write(data, 0, data.Length);

            }
           
        }

        /// <summary>
        /// ДОБАВЛЕНИЕ КЛИЕНТА В ЛИСТ ПОДКЛЮЧЕННЫХ  
        /// </summary>
        /// <param name="TClient"></param>
        public static void AddTcpClient(TcpClient TClient)
        {
            tcpClients.Add(TClient);
        }



        /// <summary>
        /// УДАЛЕНИЕ ОТКЛЮЧЁННЫХ КЛИЕНТОВ ИЗ СПИСКА ПОДКЛЮЧЁНИЙ
        /// </summary>
        public static void DelNonConnectTcp()
        {
            //for (int i = 0; i < tcpClients.Count; i++)
            //{
            //    Socket socket = tcpClients[i].Client;
            //    if (socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0)
            //    {
            //        Console.WriteLine("КRлиент отключился.");
            //        tcpClients.Remove(tcpClients[i]);
            //        socket.Close();
            //    }
            //}
            Thread.Sleep(1000);
            if (tcpClients.Count > 0)
            {
                for (int i = 0; i < tcpClients.Count; i++)
                {
                    TcpClient client = tcpClients[i];
                    if (client.Connected == false)
                    {
                        tcpClients.Remove(tcpClients[i]);
                        Console.WriteLine("КRлиент удалён.");
                    }
                }
            }
        }

        /// <summary>
        /// АВТОРИЗАЦИЯ ПОЛЬЗОВАТЕЛЯ
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="client"></param>
        public static void AuthorizationTcpClient(string nickname, TcpClient client)
        {
            string mes = DBQuerys.PassUser(nickname);
            Console.WriteLine($"AuthorizationTcpClient: {mes}");

            if (mes != "")
            {
                AddTcpClient(client);
                Console.WriteLine("Клинт добавлен \r\n");
                Console.WriteLine($"К: {mes}");
                mes = $"avt-{mes}";
            }
            else
            {
                mes = "avt-Проверьте верность введённых данных или зарегистрируйтесь!";
            }
            byte[] data = Encoding.UTF8.GetBytes($"{mes}");
            NetworkStream networkStream = client.GetStream();
            networkStream.Write(data, 0, data.Length);

        }


        /// <summary>
        /// РЕГИСТРАЦИЯ ПОЛЬЗОВАТЕЛЯ
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <param name="client"></param>
        public static void RegistrationClient(string Name, string Password, TcpClient client)
        {
            DBWork.AddUser($"INSERT INTO Persons (name, password) VALUES ('{Name}', '{Password.GetHashCode()}');");
            string mes = "okk";
            byte[] data = Encoding.UTF8.GetBytes($"{mes}");
            NetworkStream networkStream = client.GetStream();
            networkStream.Write(data, 0, data.Length);


        }


    }
}
