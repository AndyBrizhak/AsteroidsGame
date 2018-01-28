using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//Брижак Андрей Домашнее задание к уроку 3 Продвинутый курс C#

namespace MyGame
{
    class Galactic1 : BaseObject  //Создадим класс  который будет наследовать BaseObject.
    {
        public Galactic1(Point pos, Point dir, Size size) : base(pos, dir, size)  //создать конструктор, который будет
                                                                             //передавать параметры базовому объекту, чтобы создать его.
        {

        }

        public override void Draw()  //переопределите метод Draw
        {
            //Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height/2);
            Image newImage = Image.FromFile("galactic3.jpg");
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()   //Теперь переопределите метод Update.
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            Size.Width = Size.Width + 1;  // изменяем масштаб объектов
            Size.Height = Size.Height + 1;

            if (Pos.X > Game.Width)
            {
                Pos.X = Game.Width / 2;
                Size.Width = 1;
                Size.Height = 1;
            }
            if (Pos.Y > Game.Height)
            {
                Pos.Y = Game.Height / 2;
                Size.Width = 1;
                Size.Height = 1;
            }
            if (Pos.X < 0)
            {
                Pos.X = Game.Width / 2;   // возвращаются в центр
                Size.Width = 1;
                Size.Height = 1;
            }
            if (Pos.Y < 0)
            {
                Pos.Y = Game.Height / 2;
                Size.Width = 1;
                Size.Height = 1;
            }
        }

    }
}
