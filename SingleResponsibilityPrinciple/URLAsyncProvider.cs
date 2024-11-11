using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    internal class URLAsyncProvider : ITradeDataProvider
    {
        ITradeDataProvider origProvider;

        public URLAsyncProvider(ITradeDataProvider origProvider)
        {
            this.origProvider = origProvider;
        }

        public Task<IEnumerable<string>> GetTradeAsync()
        {
            Task<IEnumerable<string>> task = Task.Run(() => origProvider.GetTradeData());

            return task;
        }

        public IEnumerable<string> GetTradeData()
        {
            Task<IEnumerable<string>> task = Task.Run(() => GetTradeAsync());
            task.Wait();

            IEnumerable<string> tradeList = task.Result;
            return tradeList;
        }

        // TO DO : CODING TROUBLE
        // Still unsure how to read the code from before to make this work,
        // Much less how to change it so it's reading in line by line
        public async IAsyncEnumerable<string> GetTradeDataAsync()
        {
            throw new NotImplementedException();
        }

    }
}
