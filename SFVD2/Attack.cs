﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
namespace SFVD2
{
    internal class Attack
    {
        public string[] coocie = new string[5];
        public int count = 1;
        public int count2 = 0;
        public string user;
        public string aep;
        public string pass;
        public static string[] postInfo = new string[15];


        public Attack(string url, string userName, string passWord) { 
        
            user = userName;
            pass = passWord;

            aep = url;
        
        
        }
        
        public async void getMethod()
        {
            int cc = count;
            count++;

            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Add("User-Agent", "C# App");
                Console.WriteLine(httpclient);

                using (HttpResponseMessage responce = await httpclient.GetAsync(aep))
                {
                    var header = responce.Headers;
                    using (HttpContent content = responce.Content)
                    {
                        string text = await content.ReadAsStringAsync();

                        StreamWriter sw = new StreamWriter(cc.ToString() + "-content.txt");
                        sw.WriteLine(text);
                        Console.WriteLine(text);
                        sw.Close();

                    }
                    string s = header.ToString();
                    StreamWriter headerWriter = new StreamWriter(cc.ToString() + "-header.txt");
                    count++;
                    headerWriter.WriteLine(s);
                    
                    headerWriter.Close();

                    coocie[cc - 1] = coocieExtractor(cc.ToString() + "-header.txt");
                    Console.WriteLine("\n\n\n" + coocie[cc - 1]);
                }

                extractor();
                Console.ReadLine();
                
                string[] key = { postInfo[1], postInfo[2], "Login" };
                string[] values = { user, pass, postInfo[3] };
                string loginURL = postURlmaker();
                //if (cc == 1)
                //{
                //    post("http://192.168.0.108/ESAPI-Java-SwingSet-Interactive/LoginServletLab", key, values, 1, httpclient);
                //}
                //else
                //{

                //    post("http://192.168.0.108/ESAPI-Java-SwingSet-Interactive/LoginServletLab", key, values, 2, httpclient);
                //    Console.ReadLine();
                //}
                    string postheader = post(loginURL, key, values, 1, httpclient);
                    StreamWriter writ = new StreamWriter("1-postheader.txt");
                    writ.WriteLine(postheader);
                    writ.Close();
                    string aftercoocie =  coocieExtractor("1-postheader.txt");
                    coocie[1] = aftercoocie;

            }
        }

        public static string post(string url, string[] key, string[] value, int count,HttpClient client)
        {

            StringBuilder postData = new StringBuilder();
            for (int i = 0; i < key.Length; i++)
            {
                postData.Append(String.Format("{0}={1}&", HttpUtility.HtmlEncode(key[i]), HttpUtility.HtmlEncode(value[i])));
            }

            //  postData.Append(String.Format("{0}={1}", HttpUtility.HtmlEncode("password"), HttpUtility.HtmlEncode("123456789")));
            StringContent myStringContent = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpResponseMessage message = client.PostAsync(url, myStringContent).GetAwaiter().GetResult();
            string header = message.Headers.ToString();
            Console.WriteLine("post" + header);
          
            string responseContent = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            StreamWriter sw = new StreamWriter(count.ToString() + "-postresponce.txt");
            sw.WriteLine(responseContent);
            sw.Close();
            return header;

        }

        //public void post2(string aep, string[] key, string[] value, int cnt, HttpClient client)
        //{

        //    StringBuilder postData = new StringBuilder();
        //    for (int i = 0; i < key.Length; i++)
        //    {
        //        postData.Append(String.Format("{0}={1}&", HttpUtility.HtmlEncode(key[i]), HttpUtility.HtmlEncode(value[i])));
        //    }

            
        //    StringContent myStringContent = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-aepencoded");
        //    HttpResponseMessage message = client.PostAsync(aep, myStringContent).GetAwaiter().GetResult();
        //    string responseContent = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //    string header = message.Headers.ToString();
        //    Console.WriteLine("post" + header);
        //    StreamWriter sw = new StreamWriter(cnt.ToString() + "-postresponce.txt");
        //    sw.WriteLine(responseContent);
        //    sw.Close();
        //    sw = new StreamWriter(cnt.ToString() + "-postheader.txt");
        //    sw.WriteLine(header);
        //    sw.Close();
        //    Console.WriteLine(responseContent);
        //    coocie[cnt + 1] = coocieExtractor(cnt + "-postheader.txt");

        //    if (cnt == 2)
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            Console.WriteLine("coocie " + i.ToString() + " " + coocie[i]);
        //        }
        //        if (coocie[3] == "NOT FOUND")
        //        {
        //            Console.WriteLine("seassion fixation vulnerable: before login session used for login and did not changed");
        //        }
        //        else if (coocie[0] == coocie[2])
        //        {
        //            Console.WriteLine("seassion fixation vulnerable: before login session did not change");
        //        }
        //        else if (coocie[2] == coocie[3])
        //        {
        //            Console.WriteLine("seassion fixation vulnerable: after login all session samee ");
        //        }
        //        else
        //        {

        //            Console.WriteLine("This is secure: go to sleep...have a sweet dreams .");

        //        }


        //    }



        //}

        public string coocieExtractor(string fileName)
        {

            StreamReader reader = new StreamReader(fileName);
            int inCount = count;
            count++;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                if (line.Contains("Set-Cookie"))
                {
                    string[] parts = line.Split(' ');
                    string coocie = parts[1];
                    reader.Close();
                    return coocie;

                }

            }
            return "NOT FOUND";

        }
        public string extractor()
        {

            StreamReader reader = new StreamReader("1-content.txt");
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

                            
                            Console.WriteLine(purifier(prs));
                            count++;



                        }
                        if (prs.Contains("value") && count == 2)
                        {
                            
                            Console.WriteLine(purifier(prs));
                            break;
                        }


                    }


                }
            }


            return "not implimented";
        }
        public string purifier(string value)
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
        public string postURlmaker()
        {
            string[] parts = aep.Split('/');
           
           

            string man = parts[0] + "//" + parts[2] + "/" + parts[3] + "/" + postInfo[0];

            return man;
        }




    }
}