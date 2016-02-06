using Lab3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab3.Models
{
    [Serializable]
    public class Drawer
    {
        public List<Point> body = new List<Point>(); /// Коллекция в котором хранятся точки объекта
        public ConsoleColor color;
        public char sign;
        public Drawer()
        {
            color = ConsoleColor.Green; /// Цвет
        }

        public void Draw()
        {
            Console.ForegroundColor = color;
            foreach (Point p in body)
            {
                Console.SetCursorPosition(p.x, p.y); /// Заполняем объект его знаками
                Console.Write(sign);
            }
        }

        public void Save() /// Сохранение через boolean
        {
            Type t = GetType();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(string.Format(@"C:\Users\Kuanish\Documents\Visual Studio 2015\Projects\Lab3\Snake\bin\Debug\{0}.dat",
                t.Name), FileMode.Create, FileAccess.Write);
            bf.Serialize(fs, t);
            fs.Close();
        }

        public void Resume() /// Возобновление
        {
            Type t = GetType();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(string.Format(@"C:\Users\Kuanish\Documents\Visual Studio 2015\Projects\Lab3\Snake\bin\Debug\{0}.dat", 
                t.Name), FileMode.Open, FileAccess.Read);
            //BinaryFormatter bf = new BinaryFormatter();
            if (t == typeof(Wall)) Game.wall = bf.Deserialize(fs) as Wall;
            if (t == typeof(Snake)) Game.snake = bf.Deserialize(fs) as Snake;
            if (t == typeof(Food)) Game.food = bf.Deserialize(fs) as Food;
            fs.Close();
        }
    
    }
}
