using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Брижак Андрей Домашнее задание к уроку 3 Продвинутый курс C#

namespace MyGame
{
    /// <summary>
    /// класс для генерации случайных чисел в заданном диапазоне
    /// </summary>
    class Rnd
    {
        //private int _minValue;
        //private int _maxValue;
        

        //public Rnd(int minValue, int maxValue)
        //{
        //    _minValue = minValue;
        //    _maxValue = maxValue;
            
        //}

        private static Random random = new Random();
        /// <summary>
        /// свойство класса выдающее случайное число в заданном диапазоне
        /// </summary>
        public int Gen
        {
            get { return random.Next(-10, 10); } /*(_minValue, _maxValue); }*/
        }
        
       
    }
}
