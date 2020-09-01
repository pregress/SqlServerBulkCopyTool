# SqlServerBulkCopyTool
![](./logo.png =250x)
![Release](https://github.com/pregress/SqlServerBulkCopyTool/workflows/Release/badge.svg)

:bullettrain_side: Command line tool, to bulk copy data between a source an destination SQL server. 

When you need to move data between 2 SQL server instances you can do this with [SqlBulkCopy](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopy). These tool eliminates the coding and allows for passing in the arguments to copy the data between the 2 servers.

## Required arguments
The following arguments are required.

### source-connectionstring
The source connection string. Examples can be found [here](https://www.connectionstrings.com/sql-server/)

### source-query
A TSQL query to retrieve the data from the source database. 
Example
```
SELECT *
FROM MyTable
WHERE MyColumn = 'MyFilter'
```

### destination-connectionstring
The destination connection string. Examples can be found [here](https://www.connectionstrings.com/sql-server/)

### destination-tablename
The name of the table in the destination database. 

## Optional arguments
The following arguments are optional.

### bulk-insert-timeout
A timeout in seconds to execute the bulk insert. 
Default value = 60 seconds.

### bulk-copy-options
The [SqlBulkCopyOptions](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopyoptions#fields) as provided by Microsoft.

## Examples

### Bulk copy from localhost to a remote sql server with integrated security

```
SqlServerBulkCopyTool.exe --source-connectionstring "Server=localhost;Database=TestDb;Integrated Security=SSPI;" --source-query "SELECT * FROM MySourceTable" --destination-connectionstring "Server=tcp:remote.sample-server.com,1433;Database=TestDb;Integrated Security=SSPI;" --destination-tablename "MyDestinationTable"
```

### Bulk copy from localhost to a remote sql server with bulk copy options: TableLock and CheckConstrains

Executes the bulk copy with table lock and check constraints.

```
SqlServerBulkCopyTool.exe --source-connectionstring "Server=localhost;Database=TestDb;Integrated Security=SSPI;" --source-query "SELECT * FROM MySourceTable" --destination-connectionstring "Server=tcp:remote.sample-server.com,1433;Database=TestDb;Integrated Security=SSPI;" --destination-tablename "MyDestinationTable" --bulk-copy-options TableLock,CheckConstraints
```

### :x: Known limitations
- Table definition between the 2 instances/databases should be the same.
- There is no error handling, assuming the user can understand the exceptions.
