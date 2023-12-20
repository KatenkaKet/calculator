using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcul_2
{
    class CheckErrors
    {
        public string expression = "";
        public int AvailableCharacters()
        {
            string availablecharacters = "+-*/,()0123456789";
            for(int i = 0; i < expression.Length; i++)
            {
                if (availablecharacters.IndexOf(expression[i]) == -1)
                {
                    return 0; // не успешное
                }
            }
            return 1; // успешное
        }

        public int SyntacticAnalysis()
        {
            if (String.IsNullOrEmpty(expression)) return 0;
            if (AvailableCharacters() == 0) return 0;
            if (SyntacticAnalysisBracket() == 0) return 0;
            if (SyntacticAnalysisnNumbers() == 0) return 0;

            //string sign = "";       // хранит подряд идущие арифметические знаки

            //string operation1 = "+*/";
            //if (operation1.IndexOf(expression[0]) >= 0) return 0; // строка начинается с "+*/"

            //for (int i = 0; i < expression.Length; i++)
            //{

            //    //проверка на знак
            //    if (operation1.IndexOf(expression[i]) >= 0 || expression[i] == '-') sign += expression[i];
            //    else sign = "";
            //    if (sign.Length > 1) return 0; // проверка на несколько подряд идущих знаков


            //    // Дописать сброс данных при переходе из одного состояния в другое
            //}

            return 1; //успешное
        }

        // Проверка на скобки
        public int SyntacticAnalysisBracket()
        {
            int bracketleft = 0;    // количество '('
            int bracketright = 0;   // количество ')'
            if (expression[0] == '(') bracketleft++;
            if (expression[0] == ')') bracketright++;
            if (expression.Length > 1)
            {
                int i = 1;
                do
                {
                    if (expression[i - 1] == '(' && expression[i] == ')' || expression[i - 1] == ')' && expression[i] == '(') return 0;
                    if (expression[i] == '(') bracketleft++;
                    if (expression[i] == ')') bracketright++;
                    if (bracketright > bracketleft) return 0; // ошибка в скобках
                    i++;
                } while (i < expression.Length);
                if (bracketleft != bracketright) return 0; // не одинаковое количество скобок
            }
            else if (expression.Length == 1) { }
            {
                if (bracketleft != bracketright) return 0;
            }
            return 1;
        }

        // Проверка на запятые
        public int SyntacticAnalysisnNumbers()
        {
            string number = "";
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] >= '0' && expression[i] <= '9' || expression[i] == ',')
                {
                    number += expression[i];
                }
                else if (number != "")
                {
                    if (number[0] == '.' || number[number.Length - 1] == '.') return 0;
                    try { double number2 = double.Parse(number); }
                    catch (Exception) { return 0; }
                    number = "";
                }
            }
            if (number != "")
            {
                if (number[0] == ',' || number[number.Length - 1] == '.') return 0;
                try { double number2 = double.Parse(number); }
                catch (Exception) { return 0; }
            }
            return 1;
        }



    }


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
            double x = 0; 
            if (s[i] == '(')
            {
                i++;
                x = ProcE(); 
                if (s[i] != ')')
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
            double j = 0; 
            if (s[i] == ',')
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
    }

}
