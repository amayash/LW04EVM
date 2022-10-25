using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static string alphabet = "ABCDEF";
        static bool secondLoop = false;
        static void Main(string[] args)
        {
            while (true)
            {
                secondLoop = false;
                Console.WriteLine("Исходная система счисления? (2-16)");
                int start;
                while (!int.TryParse(Console.ReadLine(), out start))
                {
                    Console.WriteLine("Ошибка ввода. Исходная система счисления? (2-16)");
                }
                if (start < 2 || start > 16)
                {
                    Console.WriteLine("Ошибка ввода. Исходная система счисления принята за 10.");
                    start = 10;
                }

                int end = 2; //конечная система счисления

                Console.WriteLine("Введите первое число.");
                string number = Console.ReadLine();
                bool checkNumber = CheckNumber(number, start);

                Console.WriteLine("Введите второе число.");
                string number2 = Console.ReadLine();
                bool checkNumber2 = CheckNumber(number2, start);

                Console.WriteLine("Выберите операцию: (1) сложение (умеет складывать положительные" +
                    " и отрицательные числа), (2) умножение, (3) деление");
                int choose;
                while (!int.TryParse(Console.ReadLine(), out choose))
                {
                    Console.WriteLine("Ошибка ввода. Выберите операцию: (1) сложение (умеет складывать положительные" +
                    " и отрицательные числа), (2) умножение, (3) деление");
                }
                if (choose > 3 || choose < 1)
                {
                    Console.WriteLine("Ошибка ввода. Выбор принят за сложение.");
                    choose = 1;
                }

                if (checkNumber && checkNumber2)
                {
                    if (choose == 1)
                    {
                        Console.WriteLine("1 число: " + Obrabotka(start, end, number));
                        Console.WriteLine("2 число: " + Obrabotka(start, end, number2));
                        Console.WriteLine("Ответ: " + AddBinary(Obrabotka(start, end, number),
                            Obrabotka(start, end, number2)));
                    }
                    else if (choose > 1)
                    {
                        bool otr = false;
                        string s1 = "";
                        string s2 = "";
                        Console.WriteLine("1 число: " + Obrabotka(start, end, number));
                        Console.WriteLine("2 число: " + Obrabotka(start, end, number2));
                        if (Obrabotka(start, end, number).StartsWith('1'))
                        {
                            s1 += '0' + Obrabotka(start, end, number).Substring(1, Obrabotka(start, end, number).Length - 1);
                            otr = true;
                        }
                        else
                        {
                            s1 = Obrabotka(start, end, number);
                        }
                        if (Obrabotka(start, end, number2).StartsWith('1'))
                        {
                            s2 += '0' + Obrabotka(start, end, number2).Substring(1, Obrabotka(start, end, number2).Length - 1);
                            if (otr)
                                otr = false;
                            else
                                otr = true;
                        }
                        else
                        {
                            s2 = Obrabotka(start, end, number2);
                        }
                        if (choose == 2)
                        {
                            string temp2 = "00000000";
                            do
                            {
                                secondLoop = false;
                                string temp = AddBinary(s1, "10000001");
                                s1 = temp;
                                temp2 = AddBinary(temp2, s2);
                            } while (!s1.Contains("00000000") & !s1.Contains("10000000"));
                            if (otr)
                            {
                                Console.WriteLine("Ответ: 1" + temp2.Substring(1, temp2.Length - 1));
                            }
                            else Console.WriteLine("Ответ: " + temp2);
                        } else if (choose==3)
                        {
                            string count = "00000000";
                            do
                            {
                                secondLoop = false;
                                s1 = AddBinary(s1, '1' + s2.Substring(1, s2.Length - 1));
                                count = AddBinary(count, "00000001");
                            } while (!s1.Equals("00000000") & !s1.Equals("10000000") || !s1.StartsWith('1'));
                            if (!s1.Equals("10000000"))
                                Console.WriteLine("Нацело не делится.");
                            else if (otr)
                                Console.WriteLine("Ответ: 1" + count.Substring(1, count.Length - 1));
                            else Console.WriteLine("Ответ: " + count);
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Ошибка ввода. Неподходящее число в исходной системе счисления.");
                }

            }
        }
        static string Obrabotka(int start, int end, string number)
        {
            if (start == 10 && number[0] == '-')
            {
                return PryamoyKod(DesVDvoich(number, end));
            }
            else if (start == 10)
            { return DesVDvoich(number, end); }
            else if (start != 10 && number[0] == '-')
            {
                return PryamoyKod(FromTo(number, start, end));
            }
            else { return FromTo(number, start, end); }
        }
        static string AddBinary(string a, string b)
        {
            string result = "";
            if (a.StartsWith('0') && b.StartsWith('0'))
            {
                int s = 0;
                int i = a.Length - 1, j = b.Length - 1;
                while (i >= 0 || j >= 0 || s == 1)
                {
                    s += ((i >= 0) ? a[i] - '0' : 0);
                    s += ((j >= 0) ? b[j] - '0' : 0);
                    result = (char)(s % 2 + '0') + result;

                    s /= 2;
                    i--; j--;
                }
            }
            else if (a.StartsWith('1') || b.StartsWith('1'))
            {
                if (!secondLoop)
                {
                    if (a.StartsWith('1'))
                        a = ObrKod(a);
                    if (b.StartsWith('1'))
                        b = ObrKod(b);
                }
                int s = 0;
                int i = a.Length - 1, j = b.Length - 1;
                while (i >= 0 || j >= 0)
                {
                    s += ((i >= 0) ? a[i] - '0' : 0);
                    s += ((j >= 0) ? b[j] - '0' : 0);
                    result = (char)(s % 2 + '0') + result;

                    s /= 2;
                    i--; j--;
                }
                if (s == 1)
                {
                    secondLoop = true;
                    return AddBinary(result, "00000001");
                }
                if (result.StartsWith('1'))
                    return ObrKod(result);
            }
            if (result.Length > 8)
                return result.Substring(result.Length-8, result.Length - 1);
            if (result.Equals("10000000"))
                return "00000000";
            return result;
        }
        static string ObrKod(string s)
        {
            bool otr = false;
            if (s.StartsWith('1'))
                otr = true;
            string result = "";
            foreach (char c in s)
                if (c == '0')
                    result += '1';
                else if (c == '1')
                    result += '0';
            if (otr)
                result = '1' + result.Substring(1, result.Length - 1);
            return result;
        }
        static bool CheckNumber(string number, int start)
        {
            bool checkNumber = true;
            int? temp = null;
            for (int i = 0; i < number.Length; i++)
            {
                string s = number[i].ToString().ToUpper();
                if (s == "-")
                {
                    continue;
                }
                if (alphabet.Contains(s))
                {
                    if (s == "A")
                    {
                        s = "10";
                    }
                    if (s == "B")
                    {
                        s = "11";
                    }
                    if (s == "C")
                    {
                        s = "12";
                    }
                    if (s == "D")
                    {
                        s = "13";
                    }
                    if (s == "E")
                    {
                        s = "14";
                    }
                    if (s == "F")
                    {
                        s = "15";
                    }
                }

                try
                {
                    temp = int.Parse(s);
                }
                catch
                {
                    return false;
                }
                if (temp >= start)
                {
                    checkNumber = false;
                    break;
                }
            }
            return checkNumber;
        }
        static string DesVDvoich(string number, int end)
        {
            string newNum = "";
            int num = Convert.ToInt32(number);
            int ostat = Convert.ToInt32(number);
            bool otr = false;
            if (Convert.ToInt32(number) < 0)
            {
                otr = true;
                num = num * (-1);
                ostat = ostat * (-1);
            }
            ArrayList numTemp = new ArrayList();
            while (ostat > 0)
            {
                ostat = ostat / end;
                numTemp.Add(num - ostat * end);
                num = ostat;
            }
            for (int temp = numTemp.Count - 1; temp >= 0; temp--)
                newNum += Character(numTemp[temp].ToString(), "to");
            string result = "";
            if (otr)
                result += '-';
            for (int i = 0; i < 8 - newNum.Length; i++)
                result += '0';
            result += newNum;
            return result;
        }
        static string PryamoyKod(string s)
        {
            string result = "";
            bool otr = false;
            if (s.StartsWith("-"))
            {
                otr = true;
                s = s.Replace("-0", "1");
                s = s.Replace("-1", "11");
            }

            for (int i = 0; i < 8 - s.Length - 1; i++)
            {
                result += "0";
            }
            result += s;
            return result;
        }
        static string Character(string tempString, string otk)
        {
            string s = "";
            if (otk == "to")
            {
                if (Convert.ToInt32(tempString) > 10)
                    s += alphabet.Substring(Convert.ToInt32(tempString) - 10, 1);
                else
                    s += tempString;
            }
            else if (otk == "from")
            {
                if (alphabet.IndexOf(tempString) == -1)
                    s += tempString;
                else
                    s += (alphabet.IndexOf(tempString) + 10).ToString();
            }
            return s;
        }
        static string FromNtoDes(string number, int ss)
        {
            int newNum = 0;
            string temp;
            string numCopy;
            int calc;

            if (number[0].ToString() == "-")
            {
                numCopy = number.Replace("-", string.Empty);
            }
            else
            {
                numCopy = number;
            }
            for (int i = 0; i < numCopy.Length; i++)
            {
                temp = "";
                temp += Character(numCopy.Substring(i, 1).ToUpper(), "from");
                calc = (int)Math.Pow(Convert.ToDouble(ss), Convert.ToDouble(numCopy.Length - (i + 1)));
                newNum += Convert.ToInt32(temp) * calc;
            }
            if (number[0].ToString() == "-")
            {
                newNum = -newNum;
            }
            return newNum.ToString();
        }
        static string FromTo(string number, int ssN, int ssK)
        {
            string temp = FromNtoDes(number, ssN);
            temp = DesVDvoich(temp, ssK);
            return
            temp;
        }

    }
}