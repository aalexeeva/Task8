﻿using System;
using System.Collections.Generic;
using static System.Console;

namespace Task8
{
    class Program
    {
        public static int Input() // функция проверки ввода, разрешающая вводить только 0 и 1
        {
            var currentSymbol = 0; // переменная для введенного символа
            var convertResult = false; // переменная, определяющая верно ли введен символ
            while (!convertResult)
            {
                ConsoleKeyInfo keyPress = ReadKey(true); // ввод символа
                int input = keyPress.KeyChar; // код введенного символа
                // символ введен верно, если его код совпадает с кодом нуля или единицы
                convertResult = Convert.ToInt32(input) == 48 || Convert.ToInt32(input) == 49;
                if (!convertResult) continue;
                // если символ введен верно, вывод его в консоль
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
            return currentSymbol;
        }

        public static int InputNumber() // ввод числа N
        {
            var number = 0;
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

        public static bool Check(int[,] arr, int top, int edge) // проверка матрицы на наличие двух единиц в столбце
        {
            var sum = 0; // переменная для суммы элементов в столбце
            for (var i = 0; i < top; i++)
            {
                for (var j = 0; j < edge; j++)
                    sum += arr[i, j]; // вычисление суммы
                if (sum != 2) return false;
                sum = 0;
            }
            return true;
        }

        public static int[,] ArrInput(int top, int edge) // ввод матрицы инциденций
        {
            var incid = new int[top, edge];
            for (var i = 0; i < top; i++)
            {
                for (var j = 0; j < edge; j++)
                {
                    WriteLine("Введите значение ячейки {0} строки {1}", j+1, i+1);
                    incid[i, j] = Input();
                }
            }
            return incid;
        }

        public static List<int> Arr = new List<int>(); // лист с проверенными вершинами

        public static int Action(int[,] a, int checkStart) // вычисление количества компонент связности
        {
            Arr.Add(checkStart); // добавление в лист первой вершины
            for (var k = 0; k < Arr.Count; k++)
            {
                var p = Arr[k];
                for (var i = 0; i < a.GetLength(0); i++)
                    if (a[p, i] == 1)
                        for (var j = 0; j < a.GetLength(1); j++)
                            if (j == 1 && !Arr.Contains(j)
                            ) // если до вершины можно дойти из первой, то добавляем ее в лист
                                Arr.Add(j);
            }
            // поиск числа, которого нет в листе
            var num = Arr[0]; 
            for (var i = 0; i < a.GetLength(0) && num == Arr[0]; i++)
                if (!Arr.Contains(i))
                    num = i;
            // если такое число найдено, то вызов функции заново с этим числом как стартовой позицией
            if (num != Arr[0]) return Action(a, num) + 1; 
            return 1; // если не найдено, вернуть единицу
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
                // ввод матрицы
                WriteLine("Введите число вершин в графе:");
                var top = InputNumber();
                WriteLine("Введите число ребер в графе: ");
                var edge = InputNumber();
                WriteLine("Задайте матрицу инциденций:");
                var incid = ArrInput(top, edge);
                bool ok;
                do
                {
                    ok = Check(incid, top, edge);
                    if (ok) continue;
                    WriteLine("Неверно введена матрица");
                    incid = ArrInput(top, edge);
                } while (ok);

                // вывод введенной матрицы для наглядности
                WriteLine("Полученная матрица:");
                for (var i = 0; i < top; i++)
                    for (var j = 0; j < edge; j++) Write(incid[i, j]);
                    WriteLine();

                // вычисление и вывод результата
                WriteLine("В данном графе {0} компонент связности", Action(incid, 0));
                okay = Exit(); // выход
            } while (!okay);
        }
    }
}
