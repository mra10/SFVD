using System;

public class Class1
{
    public static async void getMethodAsync()
    {
        using (HttpClient httpclient = new HttpClient())
        {
            // var request = new HttpRequestMessage(HttpMethod.Get, url); ;

            var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");
            var commentValue = new ProductInfoHeaderValue("(+http://www.example.com/ScraperBot.html)");

            httpclient.DefaultRequestHeaders.UserAgent.Add(productValue);
            httpclient.DefaultRequestHeaders.UserAgent.Add(commentValue);

            var request = new HttpRequestMessage(HttpMethod.Get, "https://API.com/api");
            using (var resp = await httpclient.SendAsync(request))
            {
                //  HttpResponseMessage responce = await httpclient.SendAsync(request);

                Console.WriteLine("after");

                var header = resp.Headers;
                string s = header.ToString();
                StreamWriter sw = new StreamWriter("1".ToString() + "-header.txt");
                sw.WriteLine(s);
                sw.Close();
            }





        }
    }












    public Class1()
	{










	}
}
