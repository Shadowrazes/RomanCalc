using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalc.Models
{
    public class RomanNumberExtend : RomanNumber
    {
        public RomanNumberExtend(string num) : base(1)
        {
            romanNumber = num;
            number = 0;

            Dictionary<char, ushort> rDigits = new Dictionary<char, ushort>()
        {
          {'M', 1000},
          {'D', 500},
          {'C', 100},
          {'L', 50},
          {'X', 10},
          {'V', 5},
          {'I', 1}
        };

            ushort a = rDigits['M'];

            int buff = number;
            if (num.Length > 1)
            {
                for (int i = 0; i < num.Length - 1; i++)
                {
                    ushort current = 0;
                    ushort next = 0;
                    foreach (var rDigit in rDigits)
                    {
                        if (rDigit.Key == num[i])
                        {
                            current = rDigit.Value;
                        }
                        if (rDigit.Key == num[i + 1])
                        {
                            next = rDigit.Value;
                        }
                    }
                    if (current < next)
                        buff -= current;
                    else
                        buff += current;

                    if (i == num.Length - 2)
                        buff += next;
                }
            }
            else
            {
                buff = rDigits[num[0]];
            }

            if (buff > 0 && buff < 4000)
                number = (ushort)buff;
            else
                throw new RomanNumberException("Некорректное число");
        }

        public ushort Num()
        {
            return number;
        }
    }
}