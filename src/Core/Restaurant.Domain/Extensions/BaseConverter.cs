using System.Text;

namespace Restaurant.Domain.Extensions
{
    internal static class BaseConverter
    {
        public static string ToBase36( this long number)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (number == 0)
                return "0";

            var result = new StringBuilder();

            while (number > 0)
            {
                result.Insert(0, chars[(int)(number % 36)]);
                number /= 36;
            }

            return result.ToString();
        }
    }
}
