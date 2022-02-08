using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebRequest
{
    internal class Program
    {
        public static string url;
        public static int count = 1;
        public static string[] coocie = new string[3];
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a URL");
            url = "https://www.amazon.com";


          Console.WriteLine(url);
            Thread t = new Thread(getMethod);
            Thread t1 = new Thread(getMethod);
            t1.Start();
            t.Start();
            t.Join();
            t1.Join();


            Console.WriteLine("hello");
            //count = 1;
            //Thread t2 = new Thread(coocieExtractor);
            //Thread t3 = new Thread(coocieExtractor);
            //t2.Start();
            //t3.Start();
            //t2.Join();
            //t3.Join();
            //Console.WriteLine("\n\n\n"+coocie[1]);
            //Console.WriteLine("\n\n\n" +coocie[2]);
            Console.ReadLine();
            
           
        }

        




        public static async void getMethod()
        {
            using (HttpClient httpclient = new HttpClient())
            {

                using (HttpResponseMessage responce = await httpclient.GetAsync(Program.url))
                {
                    
                        var header = responce.Headers;
                        string s = header.ToString();
                        StreamWriter sw = new StreamWriter(Program.count.ToString() + "-header.txt");
                        count++;
                        sw.WriteLine(s);
                        //Console.WriteLine(s);
                        sw.Close();
                        string[] pieces = s.Split(' ');
                    int counter = 0;
                    while (pieces[counter] != null)
                    {
                        //Console.WriteLine("dhukse");

                        if (pieces[counter].Contains("ookie")) {

                            Program.coocie[count -1]= pieces[counter+1];
                            Console.WriteLine("\n\n\n" + pieces[counter + 1]);
                            break;


                        }

                        counter++;
                    }

                    

                }

            }
        }
        public static void coocieExtractor() {

            StreamReader reader = new StreamReader(Program.count.ToString() + "-header.txt");
            int inCount = count;
            count++;
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


















    }
}
