using Microsoft.Extensions.Options;
using System.Text;

namespace TestTaskOne.DAL;

public class SqlServerDatabaseOptions : IOptions<SqlServerDatabaseOptions>
{
	public const string Server = "Server = (localdb)\\MSSQLLocalDB;";
	public string? UserName { get; set; }

	public string? Password { get; set; }

	public required string DatabaseName { get; set; }

	public SqlServerDatabaseOptions Value => this;

	public static readonly SqlServerDatabaseOptions Default = new()
	{
		DatabaseName = "TestTaskDb",
	};

	public string BuildConnectionString()
	{
		var builder = new StringBuilder();
		builder.Append(Server);
		builder.Append($"Database = {DatabaseName};");
		builder.Append("Connect Timeout = 30;");

		if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
		{
			builder.Append($"User Id = {UserName};");
			builder.Append($"Password = {Password};");
		}

		return builder.ToString();
	}
}
