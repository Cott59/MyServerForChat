using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MyServerForChat.JSONProcessing;

namespace MyServerForChat
{
    internal class JSONProcessing
    {
        private static string _PathCurDirectory = Directory.GetCurrentDirectory();


        public static readonly object consoleLock = new object();

        //СОХРАНЕНИЕ СООБЩЕНИЯ В json ФАЙЛ АКТИВНОГО ЧАТА
        public static void SaveMessageTojsonFile(string message)
        {
            //добавить локер или мьютекс
           Thread thread = new Thread(() =>
           {
               string[] datames= getNameGroupFromMes(message);
               string Chatname = datames[1];
               Console.WriteLine(Chatname);
               string filePath = $"{Chatname}.json"; 
            
               string json = File.ReadAllText(filePath);// Читаем JSON из файла

                // Десериализуем в объект
               var data = JsonConvert.DeserializeObject<Chat>(json);
                // Добавляем новый элемент
               //data.messages.Add(new Message { _Id = 5, _Name = "Eve", _Message = "Welcome!" });
               data.messages.Add(new Message { _Id = Int32.Parse(datames[2]), _Name = datames[3], _Message = datames[0] });
               // Сериализуем обратно в JSON
               string updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                // Записываем в файл
               File.WriteAllText(filePath, updatedJson);
           });
           thread.Start();
            
        }

        //ВОЗВРАТ json ФАЙЛ АКТИВНОГО ЧАТА
        public static byte[] GetJsonFile(string mes)
        {
            string[] datames = getNameGroupFromMes(mes);
            string Chatname = datames[1];
            string filePath = $"{Chatname}.json";
            //string json = File.ReadAllText(filePath);
            byte[] data = File.ReadAllBytes(filePath);
            



            return data;
        }





        //ИЗВЛЕЧЕНИЕ НАЗВАНИЯ ГРУППЫ/ЧАТА ИЗ ВХОДЯЩЕГО СООБЩЕНИЯ
        public static string[] getNameGroupFromMes(string str)
        {
            string[] splitstr = str.Split('/','-');
            //Console.WriteLine(splitstr[1]);

            return splitstr;
        }



        


        public class Chat
        {
            [JsonProperty("messages")]
            public List<Message> messages { get; set; } = new List<Message>();
        }

        public class Message
        {
            [JsonProperty("id")]
            public int _Id { get; set; }

            [JsonProperty("name")]
            public string _Name { get; set; }

            [JsonProperty("message")]
            public string _Message { get; set; }
        }


    }
}
