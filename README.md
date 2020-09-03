# SqlServerBulkCopyTool
<img src="https://github.com/pregress/SqlServerBulkCopyTool/raw/master/logo.png" width="200">


:bullettrain_side: Command line tool, to bulk copy data between a source an destination SQL server. 

When you need to move data between 2 SQL server instances you can do this with [SqlBulkCopy](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopy). These tool eliminates the coding and allows for passing in the arguments to copy the data between the 2 servers.

![CI](https://github.com/pregress/SqlServerBulkCopyTool/workflows/CI/badge.svg)
![Release](https://github.com/pregress/SqlServerBulkCopyTool/workflows/Release/badge.svg)

You can download the single exe from the [release page](https://github.com/pregress/SqlServerBulkCopyTool/releases)

# Arguments
| Argument      | Description   | Required  | Default |
| ------------- | ------------- |------------- |------------- |
| source-connectionstring | The connection string of the source database. | yes | |
| source-query | The tSQL query to retrieve data from the source database. | yes |  |
| destination-connectionstring | The connection string of the destination database. | yes |  |
| destination-tablename | The name of the table in the destination database where the data is inserted. | yes |  |
| bulk-insert-timeout | A timeout in seconds to execute the bulk insert. | no | 60 |
| bulk-copy-options | The [SqlBulkCopyOptions](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopyoptions#fields) as provided by Microsoft. | no | Default |


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
