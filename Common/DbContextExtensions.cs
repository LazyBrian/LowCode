using System;
using System.Data.Common;
using System.Dynamic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static IEnumerable<IDictionary<string, object>> CollectionSql(this DbContext dbContext, string sql, Dictionary<string, object> paramters)
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            if (command.Connection.State != System.Data.ConnectionState.Open)
                command.Connection.Open();

            foreach (var item in paramters)
            {
                DbParameter dbParameter = command.CreateParameter();
                dbParameter.ParameterName = item.Key;
                dbParameter.Value = item.Value;

                command.Parameters.Add(dbParameter);
            }

            var obj = new List<IDictionary<string, object>>();
            using (DbDataReader dataReader = command.ExecuteReader())
            {

                while (dataReader.Read())
                {
                    var expendoObject = new ExpandoObject() as IDictionary<string, object>;

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        expendoObject.Add(dataReader.GetName(i), dataReader[i]);
                    }

                    obj.Add(expendoObject);
                }
            }

            return obj;
        }
    }
}