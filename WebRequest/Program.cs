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
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a URL");
            url = Console.ReadLine();
          
          Console.WriteLine(url);
            Thread t = new Thread(getMethod);
            Thread t1 = new Thread(getMethod);
            t1.Start();
            t.Start();
            t.Join();
            
            t1.Join();
            Console.WriteLine("hello");
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
                        Console.WriteLine(s);
                        sw.Close();


                    

                }

            }
        }


















    }
}
