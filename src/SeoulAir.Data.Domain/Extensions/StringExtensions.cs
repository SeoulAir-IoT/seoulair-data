using System.Text;

namespace SeoulAir.Data.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string FormatAsExceptionMessage(this string exceptionMessage)
        {
            StringBuilder stringBuilder = new StringBuilder(string.Empty);
            stringBuilder.Append("────────────────────────────\n");
            stringBuilder.Append("Error occured:\n");
            stringBuilder.Append(exceptionMessage + '\n');
            stringBuilder.Append("────────────────────────────\n");
            return stringBuilder.ToString();
        }
    }
}
