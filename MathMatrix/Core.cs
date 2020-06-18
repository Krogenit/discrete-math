using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMatrix
{
    public class Core : IDisposable
    {
        private string mainString;
        public Core()
        {
            /// "^" - пересечение, "u" - объединение, "/" - вычитание
            //mainString = "((S^(T1/T0))/(LuT1))u(T0^((S/T1)u(L/M)))";
            //mainString = "(S/(T0uM))u(L^(T0/S))u(M^(L/S))";
            //mainString = "(L^T0)u(S^(T1/L))u(M/(LuT1))";
            mainString = "((S^(T0/L))/(T1uT0))u((S^L)/(M/(T0uT1)))";
            Console.WriteLine(mainString);
        }
        public void Start()
        {
            int leftSkob = 0;
            int rigthSkob = 0;
            int virazhenieNum = 0;
            for (int i = 0; i < mainString.Length; i++)
            {
                if (mainString.Substring(i, 1).Equals("("))
                {
                    leftSkob++;
                }
                else if (mainString.Substring(i, 1).Equals(")"))
                {
                    rigthSkob++;
                }
                if (leftSkob != 0 && leftSkob == rigthSkob)
                {
                    virazhenieNum++;
                }
            }
            if (virazhenieNum == 2)
            {
                leftSkob = 0;
                rigthSkob = 0;
                string leftVirazh = "", rigthVirazh = "";
                string operation = "";
                for (int i = 0; i < mainString.Length; i++)
                {
                    if (mainString.Substring(i, 1).Equals("("))
                    {
                        leftSkob++;
                    }
                    else if (mainString.Substring(i, 1).Equals(")"))
                    {
                        rigthSkob++;
                    }
                    if (leftSkob != 0 && leftSkob == rigthSkob)
                    {
                        int pos1 = i + 1;
                        int pos2 = i + 2;
                        leftVirazh = mainString.Substring(0, pos1);
                        operation = mainString.Substring(pos1, 1);
                        rigthVirazh = mainString.Substring(pos2, mainString.Length - pos2);
                        break;
                    }
                }
                Console.WriteLine(Calc(leftVirazh, rigthVirazh, operation, 0));
            }
            else
            {
                leftSkob = 0;
                rigthSkob = 0;
                string leftVirazh = "", rigthVirazh = "";
                string operation = "";
                for (int i = 0; i < mainString.Length; i++)
                {
                    if (mainString.Substring(i, 1).Equals("("))
                    {
                        leftSkob++;
                    }
                    else if (mainString.Substring(i, 1).Equals(")"))
                    {
                        rigthSkob++;
                    }
                    if (leftSkob != 0 && leftSkob == rigthSkob)
                    {
                        int pos1 = i + 1;
                        int pos2 = i + 2;
                        leftVirazh = mainString.Substring(0, pos1);
                        operation = mainString.Substring(pos1, 1);
                        rigthVirazh = mainString.Substring(pos2, mainString.Length - pos2);
                        break;
                    }
                }
                Console.WriteLine(Calc(leftVirazh, rigthVirazh, operation, 0));
            }
        }
        private string openModul(string s, int shag)
        {
            Console.WriteLine("Считаем " + s);
            if (s.Equals("[T1]"))
            {
                Console.WriteLine(s + " = 2^(2^n-1)");
                return "+2^(2^n-1)";
            }
            else if (s.Equals("[T0]"))
            {
                Console.WriteLine(s + " = 2^(2^n-1)");
                return "+2^(2^n-1)";
            }
            else if (s.Equals("[S]"))
            {
                Console.WriteLine(s + " = 2^(2^(n-1))");
                return "+2^(2^(n-1))";
            }
            else if (s.Equals("[L]"))
            {
                Console.WriteLine(s + " = 2^(n+1)");
                return "+2^(n+1)";
            }
            else if (s.Equals("[S^T1]") || s.Equals("[T1^S]") || s.Equals("[S^T0]") || s.Equals("[T0^S]"))
            {
                Console.WriteLine(s + " = 2^(n-1)-1");
                return "+2^(n-1)-1";
            }
            else if (s.Equals("[T0^T1]") || s.Equals("[T1^T0]"))
            {
                Console.WriteLine(s + " = 2^n-2");
                return "+2^n-2";
            }
            else if (s.Equals("[L^T1]") || s.Equals("[T1^L]") || s.Equals("[L^T0]") || s.Equals("[T0^L]"))
            {
                Console.WriteLine(s + " = 2^n");
                return "+2^n";
            }
            else if (s.Equals("[L^S]") || s.Equals("[S^L]"))
            {
                Console.WriteLine(s + " = 2^n");
                return "+2^n";
            }
            else if (s.Equals("[M/T0]") || s.Equals("[M/T1]"))
            {
                Console.WriteLine(s + " = 1");
                return "+1";
            }
            else if (s.Equals("[(S^T1)^T0]") || s.Equals("[(S^T0)^T1]") ||
                s.Equals("[(T1^S)^T0]") || s.Equals("[(T1^T0)^S]") ||
                s.Equals("[(T0^S)^T1]") || s.Equals("[(T0^T1)^S]"))
            {
                Console.WriteLine(s + " = 2^(2^(n-1)-1)");
                return "+2^(2^(n-1)-1)";
            }
            else if (s.Equals("[(L^T1)^S]") || s.Equals("[(L^T0)^S]") ||
                s.Equals("[(L^S)^T0]") || s.Equals("[(L^S)^T1]") ||
                s.Equals("[(S^L)^T1]") || s.Equals("[(S^L)^T0]") ||
                s.Equals("[L^(T1^S)]") || s.Equals("[L^(T0^S)]") ||
                s.Equals("[L^(S^T0)]") || s.Equals("[L^(S^T1)]") ||
                s.Equals("[S^(L^T1)]") || s.Equals("[S^(L^T0)]"))
            {
                Console.WriteLine(s + " = 2^(n-1)");
                return "+2^(n-1)";
            }
            else
            {
                string[] s1 = getVirazhenieAndOperation(s);
                return Calc(s1[0], s1[2], s1[1], shag + 1);
            }
        }
        public string Calc(string a, string b, string operation, int shag)
        {
            string result = "";
            //if(a.Equals(""))
            {

            }
            if (operation[0] == 'u')
            {
                Console.WriteLine("Шаг: " + shag + " раскрываем объединение: " + a + operation + b);
                string c = "[" + a + "^" + b + "]";
                a = a.Remove(0, 1).Insert(0, "[");
                a = a.Remove(a.Length-1, 1).Insert(a.Length-1, "]");

                b = b.Remove(0, 1).Insert(0, "[");
                b = b.Remove(b.Length - 1, 1).Insert(b.Length - 1, "]");

                result = a + "+" + b + "-" + c;
                Console.WriteLine("Получаем: " + result);
                return openModul(a, shag) + openModul(b, shag) + "-" + openModul(c, shag);
            }
            else if (operation[0] == '/')
            {
                Console.WriteLine("Шаг: " + shag + " раскрываем вычитание: " + a + operation + b);
                string c = "[" + a + "^" + b + "]";
                if(a[0] == '(')
                a = a.Remove(0, 1).Insert(0, "[");
                else
                    a = '['+a;
                if (a[a.Length - 1] == ')')
                a = a.Remove(a.Length - 1, 1).Insert(a.Length - 1, "]");
                else
                    a = a+']';
                result = a + "-" + c;
                Console.WriteLine("Получаем: " + result);
                return openModul(a, shag) + "-" + openModul(c, shag);
            }
            else if (operation[0] == '^')
            {
                Console.WriteLine("Шаг: " + shag + " раскрываем пересечение: "+a+operation+b);
                if (b[0] == '(' && b[b.Length - 1] == ')')
                    b = b.Substring(1, b.Length - 2);
                else if (b[0] == '(')
                    b = b.Substring(1, b.Length - 1);
                else if (b[b.Length - 1] == ')')
                    b = b.Substring(0, b.Length - 2);
                string[] s = getVirazhenieAndOperation(b);
                if(s[1].Equals("u"))
                {
                        if (isMnozhestvo(a, 0) == 0)
                        {
                            if (a[0] == '[')
                                a = a.Remove(0, 1).Insert(0, "(");
                            else
                                a = '(' +  a;
                        }
                        else
                        {
                            a = '(' + a;
                        }
                        if (isMnozhestvo(s[0], 0) == 0)
                        {
                            if (s[0][s[0].Length - 1] == ']')
                                s[0] = s[0].Remove(s[0].Length - 1, 1).Insert(s[0].Length - 1, ")");
                            else
                                s[0] = s[0].Insert(s[0].Length - 1, ")");
                        }
                        else
                        {
                            s[0] = s[0] + ")";
                        }
                        if (isMnozhestvo(s[2], 0) == 0)
                        {
                            if (s[2][s[2].Length - 1] == ']')
                                s[2] = s[2].Remove(s[2].Length - 1, 1).Insert(s[2].Length - 1, ")");
                            else
                                s[2] = s[2].Insert(s[2].Length - 1, ")");
                        }
                        else
                        {
                            s[2] = s[2] + ")";
                        }
                        result = a + "^" + s[0] + "u" + a + "^" + s[2];
                        Console.WriteLine("Получаем: " + result);
                        return Calc(a + "^" + s[0], a + "^" + s[2], "u", shag + 1);
                }
                else if (s[1].Equals("/"))
                {
                    if(isMnozhestvo(a,0)==0)
                    a = a.Remove(0, 1).Insert(0, "(");
                    if (isMnozhestvo(s[0], 0) == 0)
                    s[0] = s[0].Remove(s[0].Length - 1, 1).Insert(s[0].Length - 1, ")");

                    result = "(" + a + "^" + s[0] + ")" + "/" + s[2];
                    Console.WriteLine("Получаем: " + result);
                    return Calc("(" + a + "^" + s[0]+")", s[2], "/", shag + 1);
                }
                else
                {
                    result = "Нет формул для решения :(";
                    Console.WriteLine(result);
                    return "+0";
                }
            }
            else
            {
                result = "error";
                Console.WriteLine(result);
                return "Шаг "+ shag +" error";
            }
        }

        private string[] getVirazhenieAndOperation(string s)
        {
            if (s[0] == '[' && s[s.Length-1] == ']')
                s = s.Substring(1, s.Length - 2);
            else if (s[0] == '[')
                s = s.Substring(1, s.Length - 1);
            if (s[s.Length - 1] == ']')
                s = s.Substring(0, s.Length - 2);
                //Console.WriteLine("Разбиваю " + s + " на выражения и операцию:");
            string[] result = new string[3];
            int leftSkob = 0;
            int rigthSkob = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s.Substring(i, 1).Equals("("))
                {
                    leftSkob++;
                }
                else if (s.Substring(i, 1).Equals(")"))
                {
                    rigthSkob++;
                }
                if (leftSkob != 0 && leftSkob == rigthSkob)
                {
                    int pos1 = i + 1;
                    int pos2 = i + 2;
                    result[0] = s.Substring(0, pos1);
                    result[1] = s.Substring(pos1, 1);
                    string s1 = getNextVirazhenie(s.Substring(pos2, s.Length - pos2));
                    result[2] = s1;
                    return result;
                }
                else if (leftSkob == 0 && isMnozhestvo(s, i) != 0)
                {
                    int pos1 = i + isMnozhestvo(s, i);
                    int pos2 = i + 1 + isMnozhestvo(s, i);
                    result[0] = s.Substring(i, isMnozhestvo(s, i));
                    if (s.Length > pos1)
                    {
                        result[1] = s.Substring(pos1, 1);
                        string s1 = getNextVirazhenie(s.Substring(pos2, s.Length - pos2));
                        result[2] = s1;
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("error");
                        result[1] = "error";
                        result[2] = "error";
                        return result;
                    }
                }
            }
            Console.WriteLine("error");
            result[0] = "error";
            result[1] = "error";
            result[2] = "error";
            return result;
        }
        private string getNextVirazhenie(string s)
        {
            string otvet = "";
            int leftSkob = 0;
            int rigthSkob = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s.Substring(i, 1).Equals("("))
                {
                    leftSkob++;
                }
                else if (s.Substring(i, 1).Equals(")"))
                {
                    rigthSkob++;
                }
                if (leftSkob != 0 && leftSkob == rigthSkob)
                {
                    int pos1 = i + 1;
                    otvet = s.Substring(0, pos1);
                    return otvet;
                }
                else if (leftSkob == 0 && isMnozhestvo(s, i) != 0)
                {
                    otvet = s.Substring(0, isMnozhestvo(s, i));
                    return otvet;
                }
            }
            otvet = "error";
            return otvet;
        }
        private int isMnozhestvo(string s, int i)
        {
            if((s.Substring(i, 1).Equals("S")) || (s.Substring(i, 1).Equals("M")) || (s.Substring(i, 1).Equals("L")))
            {
                return 1;
            }
            else if ((s.Substring(i, 2).Equals("T0")) || (s.Substring(i, 2).Equals("T1")))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        public void Dispose()
        {

        }
    }
}
