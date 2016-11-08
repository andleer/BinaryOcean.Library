using System;
using System.Security.Cryptography;

namespace BinaryOcean.Library
{
    /// <summary>
    /// AES Encryption Library. Provides both encryption and decryption functionality.
    /// </summary>
    public class AesEncryption : SymmetricEncryption
    {
        public AesEncryption(string password) : base(password) { }

        protected override SymmetricAlgorithm GetSymmetricAlgorithm()
        {
            return new AesManaged();
        }
    }
}