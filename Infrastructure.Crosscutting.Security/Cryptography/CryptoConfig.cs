namespace Infrastructure.Crosscutting.Security.Cryptography
{
    /// <summary>
    /// Settings for the encryption.
    /// </summary>
    public class CryptoConfig
    {
        private bool _encrypt = true;
        private string _internalKey = "keyphrase";

        
        /// <summary>
        /// encryption option
        /// </summary>
        /// <param name="encrypt"></param>
        public CryptoConfig()
        {
        }


        /// <summary>
        /// encryption options
        /// </summary>
        /// <param name="encrypt"></param>
        /// <param name="key"></param>
        public CryptoConfig(bool encrypt, string key)
        {
            this._encrypt = encrypt;
            this._internalKey = key;
        }


        /// <summary>
        /// Whether or not to encrypt;
        /// Primarily used for unit testing.
        /// Default is to encrypt.
        /// </summary>
        public bool Encrypt
        {
            get { return this._encrypt; }
        }


        /// <summary>
        /// Key used to encrypt a word.
        /// </summary>
        public string InternalKey
        {
            get { return this._internalKey; }
        }
    }
}
