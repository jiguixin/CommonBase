namespace Infrastructure.Crosscutting.Security.Cryptography
{
    using System.Security.Cryptography;

    /// <summary>
    /// Cryptography service to encrypt and decrypt strings.
    /// </summary>
    public class CryptoSym : ICrypto
	{
        protected CryptoConfig _encryptionOptions;
        protected SymmetricAlgorithm _algorithm;


        #region Constructors
        /// <summary>
        /// Default options
        /// </summary>
        public CryptoSym()
        {
            this._encryptionOptions = new CryptoConfig();
            this._algorithm = CryptographyUtils.CreateSymmAlgoTripleDes();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public CryptoSym(string key, SymmetricAlgorithm algorithm)
        {
            this._encryptionOptions = new CryptoConfig(true, key);
            this._algorithm = algorithm;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public CryptoSym(CryptoConfig options, SymmetricAlgorithm algorithm)
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
        public void SetAlgorithm(SymmetricAlgorithm algorithm)
        {
            this._algorithm = algorithm;
        }


		/// <summary>
		/// Encrypts the plaintext using an internal private key.
		/// </summary>
		/// <param name="plaintext">The text to encrypt.</param>
		/// <returns>An encrypted string in base64 format.</returns>
		public virtual string Encrypt( string plaintext )
		{
            if(!this._encryptionOptions.Encrypt)
                return plaintext;

            string base64Text = CryptographyUtils.Encrypt(this._algorithm, plaintext, this._encryptionOptions.InternalKey);
			return base64Text;
		}


		/// <summary>
		/// Decrypts the base64key using an internal private key.
		/// </summary>
		/// <param name="base64Text">The encrypted string in base64 format.</param>
		/// <returns>The plaintext string.</returns>
        public virtual string Decrypt( string base64Text )
		{
            if(!this._encryptionOptions.Encrypt)
                return base64Text;

            string plaintext = CryptographyUtils.Decrypt(this._algorithm, base64Text, this._encryptionOptions.InternalKey);
			return plaintext;
		}


        /// <summary>
        /// Determine if encrypted text can be matched to unencrypted text.
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public bool IsMatch(string encrypted, string plainText)
        {
            string decrypted = this.Decrypt(encrypted);
            return string.Compare(decrypted, plainText, false) == 0;
        }
	}
}
