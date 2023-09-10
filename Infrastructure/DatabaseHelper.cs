using System;
using System.Linq.Dynamic.Core;
using System.Security.Permissions;
using LowCode.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Index = Microsoft.SqlServer.Management.Smo.Index;

public class DatabaseHelper
{
    private static readonly string DATABASE_NAME = "LowCodeDb";

    private static Server Server = new Server("Lzzy");
    public static Server Instance
    {
        get
        {
            return Server;
        }
    }

    public static Database database = Instance.Databases[DATABASE_NAME];


    public static void CreateTable(InternalEntity entity)
    {
        //Database db = Server.Databases["LowCodeDb"];

        if (database == null)
        {
            Console.WriteLine("Null");
        }

        if (database.Tables.Cast<Table>().Any(c => c.Name == entity.LogicalName))
        {
            return;
        }
        Table table = new Table(database, entity.LogicalName);

        Column column = new Column(table, entity.LogicalName + "Id", DataType.Int);
        column.Collation = "Latin1_General_CI_AS";
        column.Nullable = false;

        table.Columns.Add(column);

        table.Create();

        //table.Alter();

        // Define Index object on the table by supplying the Table1 as the parent table and the primary key name in the constructor.  
        Index pk = new Index(table, entity.LogicalName + "_PK");
        pk.IndexKeyType = IndexKeyType.DriPrimaryKey;

        // Add Col1 as the Index Column  
        IndexedColumn idxCol1 = new IndexedColumn(pk, entity.LogicalName + "Id");
        pk.IndexedColumns.Add(idxCol1);

        // Create the Primary Key  
        pk.Create();
    }

    public static void AddAttribute(string logicalName, InternalAttribute attribute)
    {
        Table currentEntity = database.Tables[logicalName];

        Column column = new Column(currentEntity, attribute.LogicalName, DataType.NVarChar(attribute.MaxLength.Value));
        column.Nullable = true;
        //column.Default = attribute.DefaultValue;
        
        currentEntity.Columns.Add(column);

        currentEntity.Alter();
    }

    public static void AddRelationship(string logicalName)
    {
        // Table currentEntity = database.Tables[logicalName];

        // ForeignKey foreignKey=new ForeignKey(currentEntity,"Test");
        // new ForeignKeyColumn(foreignKey,"","")
        // ForeignKeyAction

    }
}