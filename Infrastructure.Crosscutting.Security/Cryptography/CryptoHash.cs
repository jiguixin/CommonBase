namespace Infrastructure.Crosscutting.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Cryptography service to encrypt and decrypt strings.
    /// </summary>
    public class CryptoHash : ICrypto
	{
        protected CryptoConfig _encryptionOptions;
        protected HashAlgorithm _algorithm;


        #region Constructors
        /// <summary>
        /// Default options
        /// </summary>
        public CryptoHash()
        {
            this._encryptionOptions = new CryptoConfig();
            this._algorithm = CryptographyUtils.CreateHashAlgoMd5();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public CryptoHash(string key, HashAlgorithm algorithm)
        {
            this._encryptionOptions = new CryptoConfig(true, key);
            this._algorithm = algorithm;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public CryptoHash(CryptoConfig options, HashAlgorithm algorithm)
        {
            this._encryptionOptions = options;
            this._algorithm = algorithm;
        }
        #endregion


        /// <summary>
        /// Options for encryption.
        /// </summary>
        /// <value></value>
        public CryptoConfig Settings
        {
            get { return this._encryptionOptions; }
        }


        /// <summary>
        /// Set the creator for the algorithm.
        /// </summary>
        /// <param name="algorithmCreator"></param>
        public void SetAlgorithm(HashAlgorithm algorithm)
        {
            this._algorithm = algorithm;
        }


		/// <summary>
		/// Encrypts the plaintext using an internal private key.
		/// </summary>
		/// <param name="plaintext">The text to encrypt.</param>
		/// <returns>An encrypted string in base64 format.</returns>
		public string Encrypt( string plaintext )
		{
            if(!this._encryptionOptions.Encrypt)
                return plaintext;

            string base64Text = CryptographyUtils.Encrypt(this._algorithm, plaintext);
			return base64Text;
		}


		/// <summary>
		/// Decrypts the base64key using an internal private key.
		/// </summary>
		/// <param name="base64Text">The encrypted string in base64 format.</param>
		/// <returns>The plaintext string.</returns>
        public string Decrypt( string base64Text )
		{
            throw new NotSupportedException("Can not decrypt hash algorithm.");            
		}


        /// <summary>
        /// Determine if encrypted text can be matched to unencrypted text.
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public bool IsMatch(string encrypted, string plainText)
        {
            string encrypted2 = this.Encrypt(plainText);
            return string.Compare(encrypted, encrypted2, false) == 0;
        }
	}
}
