using System.Security.Cryptography;
using System.Text;

namespace Identity.Sdk.Lib.Extensions
{
    public static class Encryption
    {
        private static string Key { get; set; }
        private static string IV { get; set; }
        private static string Salt { get; set; }
        private const string HashAlgorithm = "SHA1";
        private const int PasswordIterations = 2;
        private const int KeySize = 256;
        public static void Initialization(string key, string iv, string salt)
        {
            Key = key;
            IV = iv;
            Salt = salt;
        }

        public static byte[] Encrypt(string PlainText)
        {
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(IV);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);

            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Key, SaltValueBytes, HashAlgorithm, 
                PasswordIterations); byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);

            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;

            byte[] CipherTextBytes = null;

            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            { 
                using (MemoryStream MemStream = new MemoryStream())
                { 
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length); CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray(); MemStream.Close(); CryptoStream.Close();
                    }
                }
            }

            SymmetricKey.Clear();
            
            return CipherTextBytes;
        }

        public static string Decrypt(byte[] CipherText)
        {
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(IV);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);

            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Key, SaltValueBytes, HashAlgorithm,
                PasswordIterations);

            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);

            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;

            byte[] PlainTextBytes = new byte[CipherText.Length];
            int ByteCount = 0;

            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherText))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close(); CryptoStream.Close();
                    }
                }
            }

            SymmetricKey.Clear();

            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
        }
    }
}
