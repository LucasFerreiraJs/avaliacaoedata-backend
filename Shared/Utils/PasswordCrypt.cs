using System.Security.Cryptography;
using System.Text;

namespace api.Shared.Utils
{
    public class PasswordCrypt
    {
        public string toSHA512(string strData)
        {
            var message = Encoding.UTF8.GetBytes(strData);
            using (var alg = SHA512.Create())
            {
                string hex = "";

                var hashValue = alg.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hex += String.Format("{0:x2}", x);
                }

                //Console.WriteLine(hex);           
                return hex;
            }
        }
    }
}
