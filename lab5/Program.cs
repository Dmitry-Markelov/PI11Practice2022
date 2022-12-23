using System;
using System.Globalization;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Calculator c = new Calculator();
            //c.PressKey("1");
            //c.PressKey("0");
            //c.PressKey("+");
            //c.PressKey("5");
            //c.PressKey("=");
            //Console.WriteLine(c.Screen);

            double x = double.Parse("123.45", CultureInfo.InvariantCulture);
            Console.WriteLine(x);
        }
    }
}
