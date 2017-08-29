using System;
using System.Collections.Generic;
using static System.Console;

namespace Task8
{
    class Program
    {
        public static int Input() // функция проверки ввода, разрешающая вводить только 0 и 1
        {
            int currentSymbol = 0; // переменная для введенного символа
            bool convertResult = false; // переменная, определяющая верно ли введен символ
            while (!convertResult)
            {
                ConsoleKeyInfo keyPress = ReadKey(true); // ввод символа
                int input = keyPress.KeyChar; // код введенного символа
                // символ введен верно, если его код совпадает с кодом нуля или единицы
                convertResult = Convert.ToInt32(input) == 48 || Convert.ToInt32(input) == 49;
                if (convertResult) // если символ введен верно, вывод его в консоль
                {
                    if (input == 48)
                    {
                        WriteLine(0);
                        currentSymbol = 0;
                    }
                    else
                    {
                        WriteLine(1);
                        currentSymbol = 1;
                    }
                }
            }
            return currentSymbol;
        }

        public static int InputNumber() // ввод числа N
        {
            int number = 0;
            bool ok;
            do
            {
                try
                {
                    number = Convert.ToInt32(ReadLine());
                    if (number < 1)
                    {
                        WriteLine("Вводимое число должно быть больше 1");
                        ok = false;
                    }
                    else ok = true;
                }
                catch (FormatException)
                {
                    WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
                catch (OverflowException)
                {
                    WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
            } while (!ok);
            return number;
        }

        public static bool Check(int[,] arr, int top, int edge)
        {
            int sum = 0;
            for (int i = 0; i < top; i++)
            {
                for (int j = 0; j < edge; j++)
                {
                    sum += arr[i, j];
                }
                if (sum != 2) return false;
                sum = 0;
            }
            return true;
        }

        public static int[,] ArrInput(int top, int edge)
        {
            int[,] incid = new int[top, edge];
            for (int i = 0; i < top; i++)
            {
                for (int j = 0; j < edge; j++)
                {
                    WriteLine("Введите значение ячейки {0} строки {1}", j+1, i+1);
                    incid[i, j] = Input();
                }
            }
            return incid;
        }
        
        private static List<int> Arr = new List<int>();

        public static int Action(int[,] a, int checkStart)
        {
            Arr.Add(checkStart);
            foreach (var p in Arr)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                    if (a[p,i] == 1) 
                        for (int j = 0; j<a.GetLength(1); j++)
                            if (j == 1 && !Arr.Contains(j))
                                Arr.Add(j);
            }

            int num = Arr[0];
            for (int i = 0; i < a.GetLength(0) && num == Arr[0]; i++)
                if (!Arr.Contains(i))
                    num = i;

            if (num != Arr[0]) return Action(a, num) + 1;
            return 1;
        }

        public static bool Exit() // выход из программы
        {
            WriteLine("Желаете начать сначала или нет? \nВведите да или нет");
            var word = Convert.ToString(ReadLine()); // ответ пользователя
            Clear();
            if (word == "да" || word == "Да" || word == "ДА")
            {
                Clear();
                return false;
            }
            Clear();
            WriteLine("Вы ввели 'нет' или что-то непонятное. Нажмите любую клавишу, чтобы выйти из программы.");
            ReadKey();
            return true;
        }

        static void Main(string[] args)
        {
            bool okay;
            do
            {
                WriteLine("Введите число вершин в графе:");
                int top = InputNumber();
                WriteLine("Введите число ребер в графе: ");
                int edge = InputNumber();
                WriteLine("Задайте матрицу инциденций:");
                int[,] incid = ArrInput(top, edge);
                bool ok;
                do
                {
                    ok = Check(incid, top, edge);
                    if (ok) continue;
                    WriteLine("Неверно введена матрица");
                    incid = ArrInput(top, edge);
                } while (ok);

                WriteLine("Полученная матрица:");
                for (var i = 0; i < top; i++)
                {
                    for (var j = 0; j < edge; j++)
                    {
                        Write(incid[i, j]);
                    }
                    WriteLine();
                }

                WriteLine(Action(incid, 0));
                okay = Exit();
            } while (!okay);
        }
    }
}
