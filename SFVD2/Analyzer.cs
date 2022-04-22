using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFVD2
{
    internal class Analyzer
    {
        public void analysis(string before,string after) {

            Console.WriteLine(before +"  after :"+after);
                    if (before == "NOT FOUND" || after == "NOT FOUND")
                    {
                        Console.WriteLine("seassion fixation vulnerable: before login session used for login and did not changed");
                    }
                    else if (before == after)
                    {
                      Console.WriteLine("seassion fixation vulnerable: before login session did not change");
                    }
                    else
                    {

                        Console.WriteLine("This is secure: go to sleep...have a sweet dreams .");

                    }

        }
    }
}
