namespace WaspProject
{
    public static class DataBaseChecker
    {
        public static bool ContainsSameId(string filePath, int id)
        {
            string idPattern = Patterns.GetJsonIdPattern(id);
            return File.ReadAllText(filePath).Contains(idPattern);
        }
    }
}
