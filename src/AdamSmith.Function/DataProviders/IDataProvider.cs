using System.Threading.Tasks;

namespace AdamSmith.DataProviders
{
    interface IDataProvider
    {
        string GetData(string input);
        Task<string> GetDataAsync(string input);
    }
}
