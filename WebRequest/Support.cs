using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequest
{
    internal class Support
    {

        public static void coocieExtractor()
        {

            StreamReader reader = new StreamReader(Program.count.ToString() + "-header.txt");
            int inCount = Program.count;
            Program.count++;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                if (line.Contains("oocie"))
                {

                    Program.coocie[inCount] = reader.ReadLine();
                    break;
                }

            }

        }
        public static string extractor()
        {

            StreamReader reader = new StreamReader("1-content.txt");
            string action;
            string[] user = new string[4];
            int count = 0;
            while (!reader.EndOfStream)
            {
                string tester = reader.ReadLine();
                if (tester.Contains("form"))
                {
                    string[] pieces = tester.Split(' ');
                    foreach (string prs in pieces)
                    {
                        if (prs.Contains("action"))
                        {

                            //int end = prs.Length;
                            //end -= 2;
                            //string ac = prs.Substring(8);
                            //action = ac.Remove(ac.Length - 1);
                            //Console.WriteLine(action);
                            Console.WriteLine(purifier(prs));
                            break;
                        }

                    }


                }
                if (tester.Contains("input"))
                {
                    string[] pieces = tester.Split(' ');
                    foreach (string prs in pieces)
                    {
                        if (prs.Contains("name="))
                        {

                            //int end = prs.Length;
                            //end -= 2;
                            //string ac = prs.Substring(6);
                            //user[count]= ac.Remove(ac.Length - 1);
                            //Console.WriteLine(user[count]);
                            Console.WriteLine(purifier(prs));
                            count++;



                        }
                        if (prs.Contains("value") && count == 2)
                        {
                            //Console.WriteLine(prs);
                            //int end = prs.Length;
                            //end -= 2;
                            //string ac = prs.Substring(7);
                            //user[count] = ac.Remove(ac.Length - 1);

                            //if (user[count].Contains("\"")){
                            //    user[count] = user[count].Remove(user[count].Length - 1);
                            //}




                            //Console.WriteLine(user[count]);
                            Console.WriteLine(purifier(prs));
                            break;
                        }


                    }


                }
            }


            return "not implimented";
        }
        public static string purifier(string value)
        {


            int start = 0;
            string a = "";
            char[] b = value.ToCharArray();
            foreach (char c in b)
            {

                if (c == '"')
                {
                    if (start == 0)
                    {
                        start++;
                    }
                    else
                    {
                        //Console.WriteLine(co);
                        Program.postInfo[Program.count2] = a;
                        Program.count2++;
                        return a;

                    }


                }
                else
                {

                    if (start > 0)
                    {
                        a = a + c.ToString();

                    }

                }

            }


            return "dsad";
        }



    }
}
