using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;

namespace WebRequest
{
    internal class Program
    {
        public static string url;
        public static int count = 1;
        public static int count2 = 0;
        public static string[] coocie = new string[3];
        public static string[] postInfo = new string[4];
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a URL");
            url = "http://192.168.0.108/ESAPI-Java-SwingSet-Interactive/main?function=InsecureLogin&lab";


          Console.WriteLine(url);
            Thread t = new Thread(getMethod);
            Thread t1 = new Thread(getMethod);
            t1.Start();
            t.Start();
            t.Join();
            t1.Join();


          //  Console.WriteLine("hello");
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
            if (coocie[0] != coocie[1])
            {


                Console.WriteLine("before auth Secure");

            }
            else {
                Console.WriteLine("before seassion matched : unsafe");
            
            }
            Console.ReadLine();
            extractor();

            Console.ReadLine();
            string userName="test";
            string pass="test";
            string[] key = { postInfo[1], postInfo[2], "submit" };
            string[] values = { userName, pass, postInfo[3] };
             
            post2("http://192.168.0.108/ESAPI-Java-SwingSet-Interactive" + "/"+postInfo[0],key, values,1);
        }

        public static void post2(string url, string[] key, string[] value, int count)
        {

            StringBuilder postData = new StringBuilder();
            for (int i = 0; i < key.Length; i++)
            {
                postData.Append(String.Format("{0}={1}&", HttpUtility.HtmlEncode(key[i]), HttpUtility.HtmlEncode(value[i])));
            }

            //  postData.Append(String.Format("{0}={1}", HttpUtility.HtmlEncode("password"), HttpUtility.HtmlEncode("123456789")));
            StringContent myStringContent = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpClient client = new HttpClient();
            HttpResponseMessage message = client.PostAsync(url, myStringContent).GetAwaiter().GetResult();
            string responseContent = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            StreamWriter sw = new StreamWriter(count.ToString() + "-postresponce.txt");
            sw.WriteLine(responseContent);
            Console.WriteLine(responseContent);
            sw.Close();
        }




        public static async void getMethod()
        {
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Add("User-Agent", "C# App");
                Console.WriteLine(httpclient); 

                using (HttpResponseMessage responce = await httpclient.GetAsync(Program.url))
                {
                    
                        var header = responce.Headers;
                        using (HttpContent content = responce.Content)
                        {
                        string text = await content.ReadAsStringAsync();

                        StreamWriter sw = new StreamWriter(count.ToString() + "-content.txt");
                        sw.WriteLine(text);
                        Console.WriteLine(text);
                        sw.Close();

                         }

                    string s = header.ToString();
                        StreamWriter headerWriter = new StreamWriter(count.ToString() + "-header.txt");
                        count++;
                        headerWriter.WriteLine(s);
                       //Console.WriteLine(s);
                        headerWriter.Close();

                    StreamWriter bodyWriter = new StreamWriter(count.ToString() + "-body.txt");
                     
                    bodyWriter.Close(); 
                        string[] pieces = s.Split(' ');
                         int counter = 0;
                         while (pieces.Length>=counter)
                         {
                                

                             if (pieces[counter].Contains("Set-Cookie")) {

                            coocie[count -1]= pieces[counter+1];
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
                        postInfo[count2] = a;
                        count2++;
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


















    }
}
