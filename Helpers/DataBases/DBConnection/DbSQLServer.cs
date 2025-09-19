using Alpheus_API.Helpers.DataBases.ConnectionStrings;
using Alpheus_API.Helpers.DataBases.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using IDbConnection = Alpheus_API.Helpers.DataBases.Interfaces.IDbConnection;

namespace Alpheus_API.Helpers.DataBases.DBConnection
{
    public class DbSQLServer : IDbConnection
    {
        private static DbSQLServer? _instance;
        private readonly string _connectionString;

        private DbSQLServer(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        public static DbSQLServer GetInsSQLServer(IConfiguration? config, string option)
        {
            if(_instance != null)
                return _instance;
            
            try
            {
                if(config == null)
                    throw new ArgumentNullException($"Parameter {nameof(config)} was not a validate value.");

                var dbString = DB01.GetInsDB01(config);

                _instance = new DbSQLServer(dbString.GetConnectionString());
                return _instance;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable ExecuteStoredProcedure(string storedProcedureName, Dictionary<string, object>? parameters)
        {
            DataTable resultTable = new DataTable();

            if (storedProcedureName == null)
                throw new ArgumentNullException($"Parameter {nameof(storedProcedureName)} was not a validate value.");

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(storedProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                            foreach (var param in parameters)
                                if (param.Value is string)
                                    cmd.Parameters.AddWithValue(param.Key, param.Value.ToString().Replace("'", "") ?? "");
                                else
                                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                        con.Open();

                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(resultTable);
                            con.Close();
                            return resultTable;
                        }
                    }
                }
            }
            catch(SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private T MapDataRowToModel<T>(DataRow row) where T : new()
        {
            var model = new T();
            var properties = typeof(T).GetProperties();

            try
            {
                foreach (var property in properties)
                    if (row.Table.Columns.Contains(property.Name) && !row.IsNull(property.Name))
                        property.SetValue(model, Convert.ChangeType(row[property.Name], property.PropertyType));

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<T> MapDataList<T>(DataTable table) where T: new()
        {
            try
            {
                var modelList = new List<T>();
                var properties = typeof(T).GetProperties(); 

                foreach(DataRow row in table.Rows)
                {
                    var model = new T();
                    foreach (var property in properties)
                    {
                        if (row.Table.Columns.Contains(property.Name) && !row.IsNull(property.Name))
                            property.SetValue(model, Convert.ChangeType(row[property.Name], property.PropertyType));
                    }

                    modelList.Add(model);
                }

                return modelList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T GetData<T>(string storedName, Dictionary<string, object> parameters) where T : new()
        {
            try
            {
                var result = _instance.ExecuteStoredProcedure(storedName, parameters);

                if(result.Rows.Count > 0)
                    return MapDataRowToModel<T>(result.Rows[0]);

                return default;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> GetDataList<T>(string storedName, Dictionary<string, object>? parameters) where T : new()
        {
            try
            {
                var result = _instance.ExecuteStoredProcedure(storedName, parameters);

                if (result.Rows.Count > 0)
                    return MapDataList<T>(result);

                return default;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
