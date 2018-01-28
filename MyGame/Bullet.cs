using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//Брижак Андрей Домашнее задание к уроку 3 Продвинутый курс C#
namespace MyGame
{
    class Bullet : BaseObject, ICollision
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public static event Message MessageBulletDestroyed;  // создадим статическое событие

        public void Destroyed()    // Когда корабль погибает вызываем это событие:
        {
            MessageBulletDestroyed.Invoke();
            Console.WriteLine("Bullet Destroyed!");
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + 3;
            if (Pos.X >Game.Width)
            {
                Pos.X = 0;
            }
        }

        //при столкновениях пули с астероидом пуля и астероид регенерировались
        //в разных концах экрана;
        /// <summary>
        /// при столкновении перенести положение снаряда на левый кран экрана
        /// </summary>
        public override void Crash() //при столкновении перенести положение снаряда на левый кран экрана
        {
            Pos.X = 0;
        }


    }
}
