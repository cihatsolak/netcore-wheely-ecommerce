using System;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.Protectors
{
    public partial interface IProtectorService
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="input">value to be encrypted</param>
        /// <returns>encrypted value</returns>
        /// <exception cref="ArgumentNullException">when there is no input or no protector is produced</exception>
        IDataResult<string> Encrypt(string input);

        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="input">value to be encrypted</param>
        /// <param name="purpose">guardian name</param>
        /// <returns>encrypted value</returns>
        /// <exception cref="ArgumentNullException">when there is no input or no protector is produced</exception>
        IDataResult<string> Encrypt(string input, string purpose);

        /// <summary>
        /// Decryption 
        /// </summary>
        /// <param name="input">encrypted value</param>
        /// <returns>decrypted value</returns>
        /// <exception cref="ArgumentNullException">when there is no input or no protector is produced</exception>
        IDataResult<string> Decrypt(string input);

        /// <summary>
        /// Decryption 
        /// </summary>
        /// <param name="input">encrypted value</param>
        /// <param name="purpose">guardian name</param>
        /// <returns>decrypted value</returns>
        /// <exception cref="ArgumentNullException">when there is no input or no protector is produced</exception>
        IDataResult<string> Decrypt(string input, string purpose);
    }
}
