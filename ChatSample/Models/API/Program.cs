//using System;
//using System.Net;
//using System.IO;
//using System.Threading.Tasks;
//using AngleSharp;
//using AngleSharp.Html.Parser;
//using AngleSharp.Dom;
//using AngleSharp.Html.Dom;

//namespace App2
//{
//    class Program
//    {
//        static async Task Main(string[] args)
//        {


           

//            var client = new WebClient();
//            client.Proxy = new WebProxy("zproxy.lum-superproxy.io:22225");
//            client.Proxy.Credentials = new NetworkCredential("lum-customer-hl_0f0bde82-zone-search", "ovghn91e5f9k");
//            string data = client.DownloadString("http://www.google.com/search?q=bitcoin+casino&uule=w+CAIQICINVW5pdGVkIFN0YXRlcw");
            

//            var config = Configuration.Default;

            
//            var context = BrowsingContext.New(config);

            
//            var source = data;
            
//            var document = await context.OpenAsync(req => req.Content(source));

            
           
//            await File.WriteAllTextAsync("e:/Search.html", document.DocumentElement.OuterHtml);

//            Console.WriteLine("---------------Keywords-------------");
//            var RelKeywords = document.QuerySelectorAll("a.k8XOCe");
            
//            foreach (var item in RelKeywords)
//            {
//                Console.WriteLine(item.Text());
//            }

//            Console.WriteLine("-------------SERP--------------------");

//            var Serp = document.QuerySelectorAll("div.yuRUbf a");

//            int Serpindex = 1;
//            foreach (IHtmlAnchorElement item in Serp)
                
//            {
                
//                Console.WriteLine(Serpindex);
//                Console.WriteLine(item.Href.ToString());
//                Serpindex++;
//            }
//            Console.WriteLine("-----------------ASK-----------------------");
//            var Ask = document.QuerySelectorAll("div.cbphWd");

//            foreach (var item in Ask)
//            {
//                Console.WriteLine(item.Text());
//            }

//        }

        
//    }
//}
    