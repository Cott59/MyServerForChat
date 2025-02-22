using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServerForChat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int _inport = 3000;

            MyTcpServer.Start("127.0.0.1", _inport);

        }
    }
}
