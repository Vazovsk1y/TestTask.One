using Microsoft.Extensions.Options;
using System.Text;

namespace TestTaskOne.DAL;

public record SqlServerDatabaseOptions : IOptions<SqlServerDatabaseOptions>
{
	public const string Server = "Server = (localdb)\\MSSQLLocalDB;";
	public string? UserName { get; init; }

	public string? Password { get; init; }

	public string? DatabaseName { get; init; }

	public SqlServerDatabaseOptions Value => this;

	public static readonly SqlServerDatabaseOptions Undefined = new() { DatabaseName = null, Password = null, UserName = null };

	private SqlServerDatabaseOptions() { }

	public SqlServerDatabaseOptions(string databaseName, string? password = null, string? userName = null)
	{
		ArgumentException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));
		DatabaseName = databaseName;
		Password = password;
		UserName = userName;
	}

	public string? BuildConnectionString()
	{
		if (this == Undefined)
		{
			return null;
		}

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
