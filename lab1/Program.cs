using System;

namespace Прилунение
{
    class Program
    {
        static void Main(string[] args)
        {
            //данные
            const double G = -1.68; //ускорение свободного падения 
            const double A = 3.5; //ускорение от двигателей
            const double CRASH_SPEED = 10;
            
            double h = 1000; //высота в метрах
            double f = 20; //fuel
            double v = 0; //скорость
            double t = 0; //отрезок времени для пересчета высоты/скорости/топлива
            double a = G; //текущее ускорение
            bool onDrive = false; //индикатор вкл двигателя
            
            // основной цикл
            while(h >= 0 && f > 0)
            {
                Console.Clear();
                //вывод текущего состояния
                Console.WriteLine();
                Console.WriteLine($"Высота: {Math.Floor(h)}, Скорость: {Math.Floor(v)}, Топливо: {f}");

                //ввод игрока (проверка/корректировка)
                Console.WriteLine("Нажмите space - падать, enter - вкл двигатель, esc - выход");
                var ki = Console.ReadKey();
                if (ki.Key == ConsoleKey.Escape) break;
                if (ki.Key == ConsoleKey.Spacebar) {
                    onDrive = false;
                    a = G;
                }
                if (ki.Key == ConsoleKey.Enter) {
                    onDrive = true;
                    a = A;
                }
                t = 1;


                //пересчёт
                if (onDrive) f -= t;
                h = h + v*t + A/2*t*t;
                v = v + a*t;
            }

            //поздравить (или нет)
            if (f <= 0){
                Console.WriteLine("У вас закончилось топливо!!!");
                if (h > 10) {
                    Console.WriteLine("Вы попали в ДТП!");
                } else {
                    Console.WriteLine("Прилунение прошло успешно!!!");
                }
            } else if (Math.Abs(v) > CRASH_SPEED){
                Console.WriteLine($"Вы попали в ДТП! Скорость была слишком большая: {Math.Abs(v)} м/с");
            } else {
                Console.WriteLine("Прилунение прошло успешно!!!");
            }
        }
    }
}