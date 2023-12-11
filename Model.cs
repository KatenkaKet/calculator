using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcul_2
{
    class Calculator
    {
        public string s;
        public int i = 0; // индекс элемента
        public double ProcE() //результат
        {
            s += '\0';
            double x = ProcT(); 
            while (s[i] == '+' || s[i] == '-')
            {
                char p = s[i];
                i++; if (p == '+')
                {
                    x += ProcT();
                }
                else
                {
                    x -= ProcT();
                }
            }
            return x;
        }
        public double ProcT()
        {
            double x = ProcM();
            while (s[i] == '*' || s[i] == '/')
            {
                char p = s[i]; i++;
                if (p == '*')
                {
                    x *= ProcM();
                }
                else
                {
                    x /= ProcM();
                }
            }
            return x;
        }
        public double ProcM()
        {
            double x = 0; if (s[i] == '(')
            {
                i++;
                x = ProcE(); if (s[i] != ')')
                {
                    Console.WriteLine("missing ')'");
                }
                i++;
            }
            else
            {
                if (s[i] == '-')
                {
                    i++;
                    x -= ProcM();
                }
                else
                {
                    if (s[i] >= '0' && s[i] <= '9')
                    {
                        x = ProcC();
                    }
                    else
                    {
                        Console.WriteLine("Syntex error.");
                    }
                }
            }
            return x;
        }
        public double ProcC()
        {
            double x = 0;
            while (s[i] >= '0' && s[i] <= '9')
            {
                x *= 10; x += s[i] - '0';
                i++;
            }
            double j = 0; if (s[i] == ',')
            {
                i++;
                while (s[i] >= '0' && s[i] <= '9')
                {
                    x *= 10; x += s[i] - '0';
                    i++; j++;
                }
                if (j != 0)
                    x /= Math.Pow(10, j);
            }
            return x;
        }
        public int CheckSyntex(string s)
        {
            int bracket1 = 0;
            int bracket2 = 0;
            return 1;
        }
    }

}
