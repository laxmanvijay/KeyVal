namespace KeyValApi.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string KeyValCollectionName { get; set; } = string.Empty;
}