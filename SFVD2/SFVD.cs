using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFVD2
{
    internal class SFVD
    {
        static void Main(string[] args)
        {
            string url,user,pass;

            Console.WriteLine("Enter A aep :");
            url = Console.ReadLine();
            Console.WriteLine("Enter your UserName :");
            user= Console.ReadLine(); 
            Console.WriteLine("Enter your Pass");
            pass= Console.ReadLine();   
            CrawlerAndAEP crawler = new CrawlerAndAEP();
            string aep = crawler.crawl(url);
            Attack attack = new Attack(url, user, pass);
            attack.getMethod();
            Console.ReadLine();
            Console.WriteLine(attack.postURlmaker());
            Console.ReadLine();
            Analyzer analyzer = new Analyzer();
            analyzer.analysis(attack.coocie[0],attack.coocie[1]);
            Console.ReadLine();
        }
    }
}
