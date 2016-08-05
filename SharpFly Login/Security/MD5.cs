using System.Security.Cryptography;
using System.Text;

namespace SharpFly_Login.Security
{
    class MD5
    {
        public static string ComputeString(string data)
        {
            
            return str(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(data)));
        }

        private static string str(byte[] data)
        {
            string retVal = "";
            for (int i = 0; i < data.Length; i++)
                retVal += data[i].ToString("X2");
            return retVal;
        }
    }
}
