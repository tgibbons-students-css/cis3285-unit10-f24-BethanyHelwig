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
    }
}
