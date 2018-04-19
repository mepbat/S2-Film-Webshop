using System.Security.Cryptography;
using System.Text;

namespace Film_Webshop.Helpers
{
    public class PasswordManager
    {
        public static string Hash(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }
            return hashString + "AOJSNGFNonZIjoijoifjadj2utensd";
        }
    }
}