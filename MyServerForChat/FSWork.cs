using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServerForChat
{
    internal class FSWork // Класс для работы с файловой системой
    {
        static string _name = "PersonsChat.db";
        static public void Making_DB () // Метод для создания БД, если она ещё не существует
        {            
            if (!IsDBExists()) 
            {
                DBWork.MakeDB();
                Console.WriteLine("База данных пользователей создана и готова к работе.");
            }
            else
                Console.WriteLine("База данных пользователей существует и готова к работе.");            
        }
        static public bool IsDBExists() // Проверка существования БД с  пользователями
        {
            if (File.Exists(_name)) return true;
            return false;
        }


        //static public List<string> ReadIniFile(List<string> _param, string path) // Метод формирования списка настроек из ini-файла
        //{
        //    List<string> result = new List<string>();
        //    string _file = ReadAllFile(path); // Записываем всё из файла настроек
        //    string[] values = _file.Split('\n'); // Каждый параметр из ini-файла записываем в элемент массива
        //    int _start, _count;
        //    foreach (string item in values)
        //    {
        //        for (int i = 0; i < _param.Count; ++i)
        //        {
        //            if (item.Contains(_param[i]))
        //            {
        //                _start = item.IndexOf('\"') + 1;
        //                _count = item.LastIndexOf('\"') - _start;
        //                result.Add(item.Substring(_start, _count));
        //            }
        //        }
        //    }
        //    return result;
        //}


        //static public string ReadAllFile(string path) // Метод чтения ini-файла до конца
        //{
        //    string result = string.Empty;
        //    using (StreamReader sr = new StreamReader(path))
        //    {
        //        result = sr.ReadToEnd();
        //    }
        //    return result;
        //}
    }
}
