namespace DAL;

public static class Config
{
    public static readonly string DbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? throw new InvalidOperationException();
}