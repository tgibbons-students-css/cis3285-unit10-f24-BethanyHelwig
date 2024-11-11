using SingleResponsibilityPrinciple.Contracts;


namespace SingleResponsibilityPrinciple
{
    internal class AdjustTradeDataProvider : ITradeDataProvider
    {
        ITradeDataProvider dataProvider;

        public AdjustTradeDataProvider(ITradeDataProvider dataProvider) { 
            this.dataProvider = dataProvider;
        }
        public IEnumerable<string> GetTradeData()
        {
            IEnumerable<string> tradeData = dataProvider.GetTradeData();

            List<string> lines = new List<string>();
            foreach (string data in tradeData)
            {
                lines.Add(data.Replace("GBP", "EUR"));
            }

            return lines;
        }

        public IAsyncEnumerable<string> GetTradeDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
