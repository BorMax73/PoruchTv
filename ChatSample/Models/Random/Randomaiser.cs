using System.Linq;

namespace poruchTv.Models.Random
{
    public class Randomaiser
    {
        private static System.Random random = new System.Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}