using System;
using System.Security.Cryptography;
using System.Text;

namespace SavingsCalculator.Api.Common
{
    public class GuidHelper
    {
        public GuidHelper()
        {
        }

        public static Guid GetDeterministicGuid(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(input));
                Guid result = new Guid(hash);

                return result;
            }
        }
    }
}
