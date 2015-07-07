using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace binGo
{
    class bingSearch
    {
        public static List<KeyValuePair<string, string>> Search(string key)
        {
            List<KeyValuePair<string, string>> op = new List<KeyValuePair<string, string>>();

            // Create a Bing container
            string rootUri = "https://api.datamarket.azure.com/Bing/Search";
            var bingContainer = new Bing.BingSearchContainer(new Uri(rootUri));


            // Replace this value with your account key. 
            var accountKey = "mzyAdEQ7iPEwvJ0XWUHhYt5ypb9wmCpenCfvE7sfDsI";

            // Configure bingContainer to use your credentials. 
            bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);


            // Build the query. 
            var imageQuery = bingContainer.Image(key, null, null, null, null, null, null);
            var imageResults = imageQuery.Execute();

            foreach (var result in imageResults)
            {
                op.Add(new KeyValuePair<string, string>(result.Title, result.SourceUrl));
            }

            return op;
        }
    }
}
