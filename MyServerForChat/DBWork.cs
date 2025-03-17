using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace MyServerForChat
{
    internal class DBWork // Класс для работы с БД пользователей
    {
        static private string _dbName = "PersonsChat.db"; // Название файла с БД пользователей
        static private string _path = $"Data Source={_dbName};"; // Путь к файлу с БД


        // Метод создания БД в файле _dbName
        static public void MakeDB() 
        {
            string password = "123"; // Пароль для первого тестового пользователя
            string create_table_users = "CREATE TABLE IF NOT EXISTS Users " +
                " (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                " nickname VARCHAR, password VARCHAR);"; // Создаём таблицу в БД
            string init_data_user = "INSERT INTO Users (nickname, password)"; //+
            //    $" VALUES ('Ёжик Боря', '{password.GetHashCode()}');"; // Заполняем таблицу тестовыми данными
            SQLiteConnection conn = new SQLiteConnection(_path); // Устанавливаем соединение с БД
            SQLiteCommand cmd_create_table = conn.CreateCommand();
            SQLiteCommand cmd_init_data = conn.CreateCommand();
            cmd_create_table.CommandText = create_table_users;
            cmd_init_data.CommandText = init_data_user; 
            conn.Open();
            cmd_create_table.ExecuteNonQuery();
            cmd_init_data.ExecuteNonQuery();
            conn.Close();
        }



        /// <summary>
        /// Метод проверки наличия пользователя в БД
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static  string CheckUser(string query)
        {
            string str = string.Empty;
            //============================================================================================================

            using (SQLiteConnection conn = new SQLiteConnection(_path))
            {
                conn.Open(); // Открываем подключение

                //string query = "SELECT id, name FROM users WHERE id = 1"; // Запрос к БД
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Читаем первую строку результата
                        {
                            int id = reader.GetInt32(0); // Получаем значение из первого столбца
                            string name = reader.GetString(1); // Получаем значение из второго столбца

                            Console.WriteLine($"ID: {id}, Name: {name}");
                            str = $"{id}-{name}";
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                    }
                }
            }

            return str;


        }



        // Метод занесения записи о новом пользователе в БД 
        public static  void AddUser(string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_path))
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}