using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharpFly_Packet_Library.Security
{
    public static class Rijndael
    {

        private static ICryptoTransform decryptor { get; set; }
        private static ICryptoTransform encryptor { get; set; }
        private static RijndaelManaged rijndael { get; set; }

        public static void Initialize()
        {
            byte[] decryptKey = Encoding.ASCII.GetBytes("dldhsvmflvm").Concat(Enumerable.Repeat((byte)0, 5).ToArray()).ToArray();
            byte[] decryptIV = Enumerable.Repeat((byte)0, 16).ToArray();

            rijndael = new RijndaelManaged() { Padding = PaddingMode.Zeros, Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = decryptKey, IV = decryptIV };
            decryptor = rijndael.CreateDecryptor(decryptKey, decryptIV);
            encryptor = rijndael.CreateEncryptor();
        }


        public static string decrypt(byte[] data)
        {
            string password = null;
            using (MemoryStream ms = new MemoryStream(data))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                        password = sr.ReadToEnd();
            return password;
        }

        public static byte[] encrypt(string data)
        {
            byte[] stringBytes = Encoding.ASCII.GetBytes(data);
            byte[] retVal = encryptor.TransformFinalBlock(stringBytes, 0, stringBytes.Length);

            return retVal;
        }
    }
}
