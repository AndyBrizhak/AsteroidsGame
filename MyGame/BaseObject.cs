using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//Брижак Андрей Домашнее задание к уроку 3 Продвинутый курс C#

namespace MyGame
{
    public delegate void Message(); //добавим делегат: для обработки события 

    abstract class  BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

       

        public abstract void Draw();   //в базовом объекте оба метода обозначим абстрактными
                                       //{
                                       //    //Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
                                       //    Image newImage = Image.FromFile("Star Filled_48px.png");
                                       //    Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
                                       //}


        //Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в
        //наследниках.
        public abstract void Update();
        //{
        //    Pos.X = Pos.X + Dir.X;
        //    Pos.Y = Pos.Y + Dir.Y;
        //    Size.Width += 1;
        //    Size.Height += 1;
        //    if (Pos.X < 0) Dir.X = -Dir.X;
        //    if (Pos.X > Game.Width) Dir.X = -Dir.X;
        //    if (Pos.Y < 0) Dir.Y = -Dir.Y;
        //    if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        //}

        //при столкновениях пули с астероидом пуля и астероид регенерировались
        //в разных концах экрана;
        /// <summary>
        /// при столкновении перенести положение астероида на правый кран экрана
        /// </summary>
        public virtual void Crash() //при столкновении перенести положение астероида на правый кран экрана
        {
            Pos.X = Game.Width;
        }

        // Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы
        // можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения
        // с
        // нашим объектом
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);

    }

   

   
}
