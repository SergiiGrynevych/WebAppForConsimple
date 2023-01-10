using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Data;

namespace WebApp.Helper
{
    public class SqlProcedure
    {
        private string _connectionString { get; set; }
        public int sql_CommandTimeout = 240;

        public SqlProcedure(string connectionString, int sql_CommandTimeout)
        {
            _connectionString = connectionString;
        }
        public SqlProcedure() { }

        public async Task<List<T>> pr_GetListObject<T>(string procedureName, string cn = "", params SqlParameter[] parameters)
        {

            using var connection = string.IsNullOrEmpty(cn) ? new SqlConnection(_connectionString)
                : new SqlConnection(cn);

            var command = new SqlCommand(procedureName)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
                CommandTimeout = sql_CommandTimeout
            };
            command.Parameters.AddRange(parameters);
            connection.Open();
            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var resultRows = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dataTable.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                resultRows.Add(row);
            }

            var jsonResult = JsonConvert.SerializeObject(resultRows);
            connection.CloseAsync();

            List<T> listObjects = JsonConvert.DeserializeObject<List<T>>(jsonResult);

            return listObjects;

        }
    }
}
