namespace SummitLog.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrWhitespace(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}