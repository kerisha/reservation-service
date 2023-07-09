public static class SqlFileTrigger
{
    public static string GetRawSql(string sqlFileName)
    {
        var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations");
        var path = Path.Combine(baseDirectory, sqlFileName);
        return File.ReadAllText(path);
    }
}