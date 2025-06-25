namespace MI.Service.TestEngine.Infrastructure.Persistence.Extensions;

/// <summary>DatabaseSettings</summary>
public class DatabaseSettings
{
    /// <summary>
    /// Gets or sets the database connection string.
    /// </summary>
    public string SqlServerTestEngineDatabase { get; set; }

    /// <summary>
    /// Gets or sets the database connection string for postgreSQL.
    /// </summary>
    public string PostgreSQLTestEngineDatabase { get; set; }

    /// <summary>
    /// Gets or sets the command timeout for connection string.
    /// </summary>
    public int CommandTimeout { get; set; } = 3600;
}
