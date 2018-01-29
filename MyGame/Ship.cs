using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//Брижак Андрей Домашнее задание к уроку 3 Продвинутый курс C#
namespace MyGame
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        public int Energy => _energy;

        private int _bonus = 0;         /// init bonus energy
        public int Bonus => _bonus;

        public void BonusPlus(int n)    // add bonus enrgy
        {
            _bonus += n;
        }

        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        public void EnergyHigh(int n) //add energy high
        {
            _energy += n;
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) 
        {
        }

        public static event Message MessageDie;  //Внутри класса Ship создадим статическое событие: event Die starship


        public void Die()    // Когда корабль погибает вызываем это событие:
        {
            MessageDie?.Invoke();
            Console.WriteLine("Starship died!");
        }

        public static event Message LooseEnergy;  //Внутри класса Ship создадим статическое событие:
        public void LooseEnerg()    // Когда корабль теряет энергию вызываем это событие:
        {
            LooseEnergy?.Invoke();
            //Console.WriteLine("Starship died!");
        }

        public override void Draw()
        {
            //Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
            Image newImage = Image.FromFile("space-shuttle.png");
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, (Size.Width*2), (Size.Height*2));
        }

        public override void Update()
        {
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

       

    }
}
