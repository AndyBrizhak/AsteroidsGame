using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
//Брижак Андрей Домашнее задание к уроку 3 Продвинутый курс C#

namespace MyGame
{
   static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        private static Timer _timer = new Timer();  //Timer нужно вынести из метода Init в класс Game:
        static Game()
        {
        }

        

        public static Random Rnd = new Random();

        public static void Finish()
        {
            Console.WriteLine("The End");
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60,
            FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // Создаём объект - поверхность рисования и связываем его с формой
                                       // Запоминаем размеры формы
            
                Width = form.Width;
                Height = form.Height;
            if (Width <= 0 || Height <= 0)
            {
                throw new ArgumentOutOfRangeException();   //выбросить исключение
               
            }

            if (Width > 1000 || Height > 1000)                    //выбросить исключение
            {
                throw new ArgumentOutOfRangeException();
            }
           
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();    //Нужно добавить вызов метода Load в Init.

            //using (StreamWriter writer = File.CreateText("diary.txt"))

                //Добавим в Init таймер
                //Timer timer = new Timer { Interval = 100 };
                _timer.Start();
            
            _timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;  //В методе Init класса Game подпишемся на это событие:
            Bullet.MessageBulletDestroyed += Bullet_MessageBulletDestroyed;
            Ship.LooseEnergy += Ship_LooseEnergy;

        }

        private static void Ship_LooseEnergy()
        {
            //throw new NotImplementedException();
            Console.WriteLine("Starship loosing energy!!!");
            //StreamWriter.WriteLine("Starship loosing energy!!!");


        }

        private static void Bullet_MessageBulletDestroyed()
        {
            Console.WriteLine("Bullet destoeyd");

            //throw new NotImplementedException();
        }

        //private static void Diarywrite(string msg)
        //{
        //    StreamWriter (string msg);
        //}


        /// <summary>
        /// обработка события KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        /// <summary>
        /// Обработчик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        ///  вывод объектов на экран 
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
           
            foreach (Asteroid a in _asteroids)
                a?.Draw();

            _bullet?.Draw();

            _ship?.Draw();
            if (_ship != null)
            {
             Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                //int var = _ship.Energy;
                //Console.WriteLine("Energy:" + _ship.Energy.ToString());
            }
            Buffer.Render();
        }

      

         //создаем объекты
        public static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;  // Объекты bullet и asteroid должны быть описаны в классе Game.
        //Создадим в классе Game статический объект ship
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10)); //


        public static void Load()
        {
            int numgal = 2; // количестов галактик
            _objs = new BaseObject[numgal];
            Rnd[] _dirsX = new Rnd[numgal];  // определяем размер массива координат  более рандомный метод случайного задания направлений движения
            Rnd[] _dirsY = new Rnd[numgal];
            for (int i = 0; i < _dirsX.Length; i++)
            {
                _dirsX[i] = new Rnd();  // гене
            }
            for (int i = 0; i < _dirsY.Length; i++)
            {
                _dirsY[i] = new Rnd();
            }
            for (int i = 0; i < _objs.Length / 2; i++)   // первая часть будет galactic2
                _objs[i] = new galactic2(new Point(Width / 2, Height / 2), new Point( _dirsX[i].Gen, _dirsY[i].Gen), new Size(5, 5));
            for (int i = _objs.Length / 2; i < _objs.Length; i++)   // вторая часть будут Galactic1
                _objs[i] = new Galactic1(new Point(Width / 2, Height / 2), new Point(_dirsX[i].Gen, _dirsY[i].Gen), new Size(5, 5));

            //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[6];
            var rnd = new Random(); // создание экземпляра класса рандом
            for (var i = 0; i < _asteroids.Length; i++) // создание астероидов
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)), new Point(-r / 5, r), new
                Size(r, r));
            }
        }

              
        /// <summary>
        /// метод для изменения состояния объектов.
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();

            _bullet?.Update();
            // предшествующий вариант описания поведения астероидов
            //foreach (BaseObject obj in _asteroids)
            //{
            //    obj.Update();
            //    if (obj.Collision(_bullet))          //обнаружение столкновений
            //    {
            //        obj.Crash(); /*при столкновениях пули с астероидом пуля и астероид регенерировались*/
            //                        //в разных концах экрана;
            //        _bullet.Crash();
            //        System.Media.SystemSounds.Hand.Play();
            //    }
            //}
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue; // если объект не определен продолжить обход цикла далее
                _asteroids[i].Update();              // иначе обновить положение астероида стандартно

                if (_bullet != null && _bullet.Collision(_asteroids[i]))//если снаряд не нулл и снаряд столкнулся с астероидом
                {
                    System.Media.SystemSounds.Hand.Play();//звуковой сигнал
                    _asteroids[i] = null;  // этот астероид уничтожается - нулл
                    _bullet?.Destroyed();
                    _bullet = null;          // снаряд уничтожается - нулл
                    
                    continue;               // если продолжить обход цикла далее
                }
                if (!_ship.Collision(_asteroids[i])) continue;   //если корабль и этот астероид в цикле не столкнулись продолжить обход цикла
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));  // иначе корабль потерял энергию
                _ship.LooseEnerg();             // Loosing energy
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0)
                {
                    _ship?.Die();   //если энергия корабля меньше или равна нулю корабль погибает
                }
            }

        }


    }
}
