namespace ChatApp.Models
{
    public interface IChatAppDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string SignupCollectionName { get; set; }


    }
}
