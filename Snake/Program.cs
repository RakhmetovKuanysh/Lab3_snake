using Lab3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3
{
    class Program
    {

        static void Main(string[] args)
        {

            Game.inGame = true;
            Game.Init(); /// Начальные значения
            Game.LoadLevel(1); ///Переход для каждлого уровня

            Console.SetWindowSize(48, 48); /// Устанавливаем размер консоли

            while (Game.inGame) /// То есть пока мы играем
            {
                Game.Draw(); 

                ConsoleKeyInfo pressedKey = Console.ReadKey();

                switch (pressedKey.Key) /// Если нажимаем клавиши вверх, вниз, влево, вправо
                {
                    case ConsoleKey.UpArrow:
                        Game.snake.Move(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        Game.snake.Move(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        Game.snake.Move(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        Game.snake.Move(1, 0);
                        break;
                    case ConsoleKey.Escape:
                        Game.inGame = false; ///При esc выходим из игры
                        break;
                    case ConsoleKey.F2:
                        Game.Save(); /// Сохраняем игру, сериализируем
                        break;
                    case ConsoleKey.F3:
                        Game.Resume(); /// Десериализация
                        break;
                }
            }


        }
    }
}
