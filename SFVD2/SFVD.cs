using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace SFVD2
{
    internal class SFVD
    {
        static void Main(string[] args)
        {
            string url,user,pass;
            string[] coocies = new string[6];

            Console.WriteLine("Enter A aep :");
            url = Console.ReadLine();
            Console.WriteLine("Enter your UserName :");
            user= Console.ReadLine(); 
            Console.WriteLine("Enter your Pass");
            pass= Console.ReadLine();   
            CrawlerAndAEP crawler = new CrawlerAndAEP();
            string aep = crawler.crawl(url);
            Attack attack = new Attack(url, user, pass);
            Attack attack1 = new Attack(url, user, pass);
            Thread t1 = new Thread(attack.getMethod);
            Thread t2 = new Thread(attack1.getMethod);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join(); 
            Console.ReadLine();
            Console.WriteLine(attack.postURlmaker());
            Console.ReadLine();
            Analyzer analyzer = new Analyzer();
            analyzer.analysis(attack.coocie[0],attack.coocie[2]);
            StreamWriter writ = new StreamWriter("coocies.txt");
            StreamReader reader = new StreamReader("1-beforecoocie.txt");
            coocies[0] = reader.ReadToEnd();
            reader.Close();
            reader = new StreamReader("2-beforecoocie.txt");
            coocies[1] = reader.ReadToEnd();
            reader.Close();
            reader = new StreamReader("1-aftercoocie.txt");
            coocies[2] = reader.ReadToEnd();
            reader.Close();
            reader = new StreamReader("2-aftercoocie.txt");
            coocies[3] = reader.ReadToEnd();
            reader.Close();
            foreach (var item in coocies)
            {
                Console.WriteLine(item);
            }

            writ.Close();
            Console.ReadLine();
            
        }
    }
}
