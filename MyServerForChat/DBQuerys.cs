using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServerForChat
{
    internal static class DBQuerys // Класс для запросов в БД
    {
        public static string PassUser(string nickname) // Метод для допуска пользователя в чат (true - допуск, false - недопуск)
        {
            string ff = DBWork.CheckUser($"SELECT * FROM Persons WHERE name = '{nickname}';");
            Console.WriteLine($"PassUser: {ff} ");
            return ff;
        }
        
        
    }
}
//static bool Autorization(string nickname) // Процедура авторизации пользователя
        //{
        //    Console.WriteLine($"Пользователь {nickname} есть в базе данных!");
        //    // Тут надо отправить запрос пользователю на ввод пароля
        //    Console_Text("Введите пароль -> ", ConsoleColor.Yellow);
        //    string password = Console.ReadLine(); // В эту переменную надо записать пароль от подключающегося пользователя
        //    if (DBWork.CheckUser($"SELECT id FROM Users WHERE password = '{password.GetHashCode()}' AND nickname = '{nickname}';"))
        //    {
        //        Console_Text("Авторизация завершена успешно!", ConsoleColor.Green);
        //        Console.WriteLine();
        //        return true;
        //    }
        //    Console_Text("Неверный пароль!", ConsoleColor.Red); // ОБЛОМ, ПОЛЬЗОВАТЕЛЬ НЕ ПОДКЛЮЧИЛСЯ В ЧАТ
        //    Console.WriteLine();
        //    return false;
        //}

            //else // Если пользователя нет в БД
            //{
            //    // Тут надо отправить пользователю запрос на регистрацию
            //    Console.WriteLine($"Пользователя {nickname} ещё нет в базе данных!\nЗарегистрироваться?\n" +
            //        $"Нажмите '1' для выхода или любую другую клавишу для регистрации.");
            //    Console_Text("Ваш выбор -> ", ConsoleColor.Yellow);
            //    char choice = Console.ReadKey().KeyChar; // В переменную записываем выбор пользователя
            //    Console.WriteLine();
            //    if (choice != '1')
            //    {
            //        Console_Text("Введите пароль для регистрации -> ", ConsoleColor.Yellow);
            //        string password = Console.ReadLine(); // В эту переменную надо записать пароль от регистрируещегося пользователя
            //        DBWork.AddUser($"INSERT INTO Users (nickname, password) VALUES ('{nickname}', '{password.GetHashCode()}');");
            //        Console_Text($"Пользователь {nickname} зарегистрирован в системе!", ConsoleColor.Green);
            //        Console.WriteLine();
            //        result = Autorization(nickname);
            //    }
            //}