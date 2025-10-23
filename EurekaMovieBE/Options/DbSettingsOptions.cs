namespace EurekaMovieBE.Options
{
    public class DbSettingsOptions
    {
        public const string OptionName = "DbSettings";
        public string MongoDbConnection { get; set; } = default!;
        public string MongoDbName { get; set; } = default!;
        public string PostgresConnection { get; set; } = default!;
    }
}
