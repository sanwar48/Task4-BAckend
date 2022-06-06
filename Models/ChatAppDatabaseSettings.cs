namespace ChatApp.Models
{
    public class ChatAppDatabaseSettings:IChatAppDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string SignupCollectionName { get; set; } = null!;
    }
}
