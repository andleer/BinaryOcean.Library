using System;

namespace BinaryOcean.Library
{
    public interface ISymmetricEncryption
    {
        string Encrypt(string plainText);
        string Encrypt(byte[] data);
        string DecryptToString(string encryptedText);
        byte[] DecryptToByteArray(string encryptedText);
    }
}
