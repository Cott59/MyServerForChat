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
        public static string Message = string.Empty;


        //РАССЫЛКА СООБЩЕНИЯ
        /// <summary>
        /// РАССЫЛКА СООБЩЕНИЯ
        /// </summary>
        /// <param name="mes"></param>
        public static void sendMessage(string mes)
        {
            byte[] data = Encoding.UTF8.GetBytes(mes);
            foreach (TcpClient client in tcpClients)
            {
                 NetworkStream stream = client.GetStream();
                 stream.Write(data, 0, data.Length);

            }
           
        }

        



    }
}
