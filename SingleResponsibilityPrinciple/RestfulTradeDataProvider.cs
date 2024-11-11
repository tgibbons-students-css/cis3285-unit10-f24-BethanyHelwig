using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        string url;
        ILogger logger;
        HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        async Task<List<string>> GetTradeAsync()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");
            List<string> tradesString = null;

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Read the content as a string and deserialize it into a List<string>
                string content = await response.Content.ReadAsStringAsync();
                tradesString = JsonSerializer.Deserialize<List<string>>(content);
                logger.LogInfo("Received trade strings of length = " + tradesString.Count);
            }
            return tradesString;
        }

        public IEnumerable<string> GetTradeData()
        {
            Task<List<string>> task = Task.Run(() => GetTradeAsync());
            task.Wait();

            List<string> tradeList = task.Result;
            return tradeList;
        }
        
        public async IAsyncEnumerable<string> GetTradeDataAsync()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");
            List<string> tradesString = null;

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                // TO DO: CODING TROUBLE
                // Be able to deserialize JSON line by line;
                // Finding info online stating that it is not supported, however
                
                // Read the content as a string and deserialize it into a List<string>
                string content = await response.Content.ReadAsStringAsync();
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        //line = JsonSerializer.DeserializeAsync<string>(line);
                        yield return line;
                    }
                }
            }
        }
    }
}
