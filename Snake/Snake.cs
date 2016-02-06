using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models
{
    [Serializable]
    public class Snake : Drawer // Наследование от класса Drawer
    {
        public Snake()
        {
            color = ConsoleColor.DarkRed;
            sign = 'o';
        }

        public void Move(int dx, int dy) // Координаты движения
        {

            for (int i = body.Count - 1; i > 0; --i) 
            {
                body[i].x = body[i - 1].x; 
                body[i].y = body[i - 1].y; 
            }

            if (body[0].x + dx < 0) dx = dx + 48; /// Чтобы снейк не выходил из консоли
            if (body[0].y + dy < 0) dy = dy + 48; 
            if (body[0].x + dx > 48) dx = dx - 48;
            if (body[0].y + dy > 48) dy = dy - 48;

            body[0].x = body[0].x + dx; /// Это голова
            body[0].y = body[0].y + dy;

            ///Проверка, можем ли скушать
            if (Game.snake.body[0].x == Game.food.body[0].x && Game.snake.body[0].y == Game.food.body[0].y) //Если координаты змейки равны коорд.еды 
            {
                /// Добавил к змейке новую точку, голову
                Game.snake.body.Add(new Point { x = Game.food.body[0].x, y = Game.food.body[0].y });
                // Переместил еду на новую позицию 

                /// Рандомная генерацию еды, чтобы она не попадала ни на стену ни на змейку 

                int fx = 0, fy = 0; /// Куда ставим еду
                bool area = false;
                while (!area)
                {
                    fx = new Random().Next(0, 47); /// Координаты (временные) в рандомной площади 
                    fy = new Random().Next(0, 47); ///Возвращает случайное целое число в указанном диапазоне
                    area = true;

                    for (int i = 0; i < Game.wall.body.Count; ++i) /// Пробегаем по координатам стены
                    {
                        if (fx == Game.wall.body[i].x && fy == Game.wall.body[i].y) //Еда появляется на новых координатах 
                        {
                            area = false;
                            break;
                        }


                    }
                    for (int j = 0; j < Game.snake.body.Count; ++j)  
                    {
                        if (fx == Game.snake.body[j].x &&
                        fy == Game.snake.body[j].y) 
                        {
                            area = false;
                            break;
                        }
                    }
                }
                Game.food.body[0].x = fx; /// Окончательные координаты еды
                Game.food.body[0].y = fy; ///

                Game.foodEat++; /// После того как покушал количество увеличивается
                if (Game.foodEat % 4 == 0) //Если съел 4 еды то включает следующий уровень
                {
                    Console.Clear(); 
                    Game.wall.body.Clear();
                    Game.food.body.Clear(); 
                    Game.LoadLevel((Game.foodEat / 4) + 1);
                    Game.Init();
                }
            }

            /// Проверка, есть ли столкновение со стеной 
            for (int i = 0; i < Game.wall.body.Count; ++i)
            {
                if (Game.snake.body[0].x == Game.wall.body[i].x && Game.snake.body[0].y == Game.wall.body[i].y)
                {
                    Console.Clear();
                    Console.SetCursorPosition(20, 20);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game Over!");
                    Game.inGame = false; /// Игра заканчивается
                }
            }
        }
    }
}