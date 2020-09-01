using System.Data.SqlClient;
using CommandLine;

namespace SqlServerBulkCopyTool
{
    public class Options
    {
        [Option("source-connectionstring", Required = true, HelpText = "The connection string of the source database")]
        public string SourceConnectionstring { get; set; }
        [Option("source-query", Required = true, HelpText = "The query to execute against the source connection to retrieve the data.")]
        public string SourceQuery { get; set; }
        [Option("destination-connectionstring", Required = true, HelpText = "The connection string of the destination database")]
        public string DestinationConnectionstring { get; set; }
        [Option("destination-tablename", Required = true, HelpText = "The name of the destination table.")]
        public string DestinationTablename { get; set; }

        [Option("bulk-insert-timeout", Default = 60, Required = false, HelpText = "Timeout in seconds to execute the bulk insert operation.")]
        public int BulkInsertTimeout { get; set; }

        [Option("bulk-copy-options", Default = SqlBulkCopyOptions.Default, Required = false, HelpText = "The SqlBulkCopyOptions flags, for more info: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopyoptions#fields")]
        public SqlBulkCopyOptions SqlBulkCopyOptions { get; set; }
    }
}