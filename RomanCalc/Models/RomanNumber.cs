using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalc.Models
{
    public class RomanNumber : ICloneable, IComparable
    {
        protected ushort number;
        protected string romanNumber;
        public RomanNumber(ushort n)
        {
            if (n > 0 && n < 4000)
            {
                number = n;
                romanNumber = "";

                Dictionary<ushort, string> rDigits = new Dictionary<ushort, string>()
            {
                {1000, "M"},
                {500, "D"},
                {100, "C"},
                {50, "L"},
                {10, "X"},
                {5, "V"},
                {1, "I"}
            };

                int buff = number;
                ushort wrapper = 1000;

                void writeSymbols(int n, string symb)
                {
                    for (int i = 0; i < n; i++)
                    {
                        romanNumber += symb;
                    }
                }

                while (buff > 0)
                {
                    int count = buff / wrapper;
                    if (count > 0 && count < 4)
                    {
                        writeSymbols(count, rDigits[wrapper]);
                        buff -= count * wrapper;
                    }
                    if (count == 4)
                    {
                        romanNumber += rDigits[(ushort)(wrapper)];
                        romanNumber += rDigits[(ushort)(wrapper * 5)];
                        buff -= count * wrapper;
                    }
                    if (count == 5)
                    {
                        romanNumber += rDigits[(ushort)(wrapper * count)];
                        buff -= count * wrapper;
                    }
                    if (count > 5 && count < 9)
                    {
                        romanNumber += rDigits[(ushort)(wrapper * 5)];
                        writeSymbols(count - 5, rDigits[(ushort)(wrapper)]);
                        buff -= count * wrapper;
                    }
                    if (count == 9)
                    {
                        romanNumber += rDigits[(ushort)(wrapper)];
                        romanNumber += rDigits[(ushort)(wrapper * 10)];
                        buff -= count * wrapper;
                    }
                    wrapper /= 10;
                }
            }
            else
                throw new RomanNumberException("Некорректное число");
        }

        public object Clone()
        {
            return new RomanNumber(number);
        }

        public int CompareTo(object? n)
        {
            if (n is RomanNumber rNumber)
                return number - rNumber.number;
            throw new RomanNumberException("Некорректный параметр передан в функцию");
        }
        public static RomanNumber operator +(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 != null && n2 != null && n1.number + n2.number < 4000)
                return new RomanNumber((ushort)(n1.number + n2.number));
            else
                throw new RomanNumberException("Сложение этих чисел невозможно");
        }
        public static RomanNumber operator -(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 != null && n2 != null && n1.number - n2.number > 0)
                return new RomanNumber((ushort)(n1.number - n2.number));
            else
                throw new RomanNumberException("Вычитание этих чисел невозможно");
        }
        public static RomanNumber operator *(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 != null && n2 != null && n1.number * n2.number < 4000)
                return new RomanNumber((ushort)(n1.number * n2.number));
            else
                throw new RomanNumberException("Умножение этих чисел невозможно");
        }
        public static RomanNumber operator /(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 != null && n2 != null && n1.number / n2.number > 0)
                return new RomanNumber((ushort)(n1.number / n2.number));
            else
                throw new RomanNumberException("Деление этих чисел невозможно");
        }
        public override string ToString()
        {
            if (romanNumber != "")
                return romanNumber;
            else
                throw new RomanNumberException("Число не найдено");
        }
    }
}