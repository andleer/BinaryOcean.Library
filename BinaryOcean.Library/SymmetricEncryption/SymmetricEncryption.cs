using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BinaryOcean.Library
{
    public abstract class SymmetricEncryption : ISymmetricEncryption
    {
        public SymmetricEncryption(string password)
        {
            SymmetricAlgorithm = GetSymmetricAlgorithm();
            GenerateKey(password);
        }

        private SymmetricAlgorithm SymmetricAlgorithm { get; set; }
        protected abstract SymmetricAlgorithm GetSymmetricAlgorithm();

        protected virtual void GenerateKey(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new InvalidOperationException("Password cannot be null or empty.");

            var hashAlgorithm = GetHashAlgorithm(SymmetricAlgorithm.KeySize);
            var hash = hashAlgorithm.ComputeHash(new ASCIIEncoding().GetBytes(password));

            SymmetricAlgorithm.Key = SubArray(hash, 0, SymmetricAlgorithm.KeySize / 8);
        }

        protected HashAlgorithm GetHashAlgorithm(int sizeInBits)
        {
            if (sizeInBits <= 160)
                return new SHA1Managed();

            if (sizeInBits <= 256)
                return new SHA256Managed();

            if (sizeInBits <= 384)
                return new SHA384Managed();

            if (sizeInBits <= 512)
                return new SHA512Managed();

            throw new InvalidOperationException("Unable to find a SHA Hashing Algorithm that will return the required number of bits.");
        }

        public string Encrypt(string plainText)
        {
            return Encrypt(new ASCIIEncoding().GetBytes(plainText));
        }

        public string Encrypt(byte[] data)
        {
            SymmetricAlgorithm.GenerateIV();

            using (var encryptor = SymmetricAlgorithm.CreateEncryptor())
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(Concat(SymmetricAlgorithm.IV, memoryStream.ToArray()));
            }
        }

        public string DecryptToString(string encryptedText)
        {
            return new ASCIIEncoding().GetString(DecryptToByteArray(encryptedText));
        }

        public byte[] DecryptToByteArray(string encryptedText)
        {
            try
            {
                var data = Convert.FromBase64String(encryptedText);
                SymmetricAlgorithm.IV = SubArray(data, 0, SymmetricAlgorithm.IV.Length);

                var cipher = SubArray(data, SymmetricAlgorithm.IV.Length, data.Length - SymmetricAlgorithm.IV.Length);
                byte[] result = new byte[cipher.Length];

                using (var encryptor = SymmetricAlgorithm.CreateDecryptor())
                using (var memoryStream = new MemoryStream(cipher))
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read))
                {
                    var length = cryptoStream.Read(result, 0, result.Length);
                    return SubArray(result, 0, length);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Symmetric Decryption Failure.", ex);
            }
        }

        private T[] SubArray<T>(T[] byteArray, int start, int length)
        {
            T[] result = new T[length];

            Array.Copy(byteArray, start, result, 0, length);

            return result;
        }

        private T[] Concat<T>(T[] array1, T[] array2)
        {
            var result = new T[array1.Length + array2.Length];

            Array.Copy(array1, 0, result, 0, array1.Length);
            Array.Copy(array2, 0, result, array1.Length, array2.Length);

            return result;
        }
    }
}