using Lab3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3.Models
{
    [Serializable]
    public class Game
    {
        public static int foodEat = 0; // Начальное съеденное количество еды
        public static bool inGame; // Начать игру
        public static Snake snake = new Snake(); // Создаем отдельный класс для змейки
        public static Wall wall = new Wall(); // Для стены
        public static Food food = new Food(); // Для еды
     

        public static void LoadLevel(int i) //Принимаем от снейк значение еды
        {
            FileStream fs = new FileStream(string.Format(@"C:\Users\Kuanish\Documents\Visual Studio 2015\Projects\Lab3\Snake\Levels\MapLevel{0}.txt", i),
                FileMode.Open, FileAccess.Read); // Имеем доступ к считыванию файл, открыв его

            StreamReader reader = new StreamReader(fs); /// Считываем файл

            string line;
            int row = -1;
            int col = 0;

            while ((line = reader.ReadLine()) != null) /// Пока в файле карты есть еще елемент продолжать действия
            {
                row++;
                col = -1;
                foreach (char c in line)
                {
                    col++;
                    if (c == '#')
                    {
                        Game.wall.body.Add(new Point { x = col, y = row });
                    }
                }
            }

            fs.Close();
        }

        public static void Resume()
        {
            wall.Resume();
            food.Resume();
            snake.Resume();
        }

        public static void Save()
        {
            wall.Save();
            food.Save();
            snake.Save();
        }

        public static void Init() /// Начальные значения
        {
            snake.body.Add(new Point { x = 10, y = 20 });
            food.body.Add(new Point { x = 30, y = 30 });
        }

        public static void Draw()
        {
            Console.Clear();
            snake.Draw();
            food.Draw();
            wall.Draw();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Eaten Foods:" + " " + foodEat);
        }
    }

}