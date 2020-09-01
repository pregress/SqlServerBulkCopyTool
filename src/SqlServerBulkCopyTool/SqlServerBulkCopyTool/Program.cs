using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace SqlServerBulkCopyTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var program = new Program();
            var result = Parser.Default.ParseArguments<Options>(args);
            await result.WithParsedAsync(program.Run);
            result.WithNotParsed(x =>
            {
                var helpText = HelpText.AutoBuild(result, h =>
                {
                    h.AutoHelp = false;     // hides --help
                    h.AutoVersion = false;  // hides --version
                    return HelpText.DefaultParsingErrorsHandler(result, h);
                }, e => e);
                Console.WriteLine(helpText);
            });
        }

        public async Task Run(Options options)
        {
            Console.WriteLine("Opening source connection");
            await using var srcConnection = new SqlConnection(options.SourceConnectionstring);
            await srcConnection.OpenAsync();

            Console.WriteLine("Create data reader");
            var sourceCommand = new SqlCommand(options.SourceQuery, srcConnection);
            var reader = await sourceCommand.ExecuteReaderAsync();

            Console.WriteLine("Opening destination connection");
            await using var dstConnection = new SqlConnection(options.DestinationConnectionstring);
            await dstConnection.OpenAsync();
            var transaction = dstConnection.BeginTransaction();

            using var bulkCopy = new SqlBulkCopy(dstConnection, options.SqlBulkCopyOptions, transaction)
            {
                DestinationTableName = options.DestinationTablename, 
                BulkCopyTimeout = options.BulkInsertTimeout
            };

            Console.WriteLine($"Starting bulk copy to table: {options.DestinationTablename}");
            await bulkCopy.WriteToServerAsync(reader);
            Console.WriteLine("Commiting transaction");
            await transaction.CommitAsync();
            await reader.CloseAsync();
            Console.WriteLine("Finished");
        }
    }
}
