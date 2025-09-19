namespace Alpheus_API.Helpers.DataBases.Interfaces
{
    public interface IDbConnection
    {
        T GetData<T>(string storedName, Dictionary<string, object> parameters) where T : new();
        List<T> GetDataList<T>(string storedName, Dictionary<string, object> parameters) where T : new();
    }
}
