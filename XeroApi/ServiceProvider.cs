using System;
using System.Configuration;

using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Storage.Basic;

using XeroApi;
using XeroApi.OAuth;


    public class ServiceProvider
    {
        [ThreadStatic]
        private static IOAuthSession _oauthSession;

        [ThreadStatic]
        private static ITokenRepository _tokenRepository;

        /// <summary>
        /// Gets the current instance of Repository (used for getting data from the Xero API)
        /// </summary>
        /// <returns></returns>
        public static Repository GetCurrentRepository()
        {
            var session = GetCurrentSession();

            return (session != null)
                ? new Repository(session) 
                : null;
        }

        /// <summary>
        /// Gets the current OAuthSession (used for getting request tokens, access tokens, etc.)
        /// </summary>
        /// <returns></returns>
        public static IOAuthSession GetCurrentSession()
        {
            return _oauthSession ?? (_oauthSession = CreateOAuthSession());
        }


        /// <summary>
        /// Creates the OAuth session.
        /// </summary>
        /// <returns></returns>
        private static IOAuthSession CreateOAuthSession()
        {
            const string userAgent = "Xero.API.ScreenCastWeb v2.0";
             
                    // Private App
                    return new XeroApiPrivateSession(
                        userAgent,
                        "0KSXUK7KYBFKUCOFUCBZ1CWFVI8FME",         // Consumer Key
                        CertificateRepository.GetOAuthSigningCertificate());            // OAuth Signing Certificate             
        }


        /// <summary>
        /// Gets or sets the current token repository.
        /// </summary>
        /// <value>The current token repository.</value>
        public static ITokenRepository CurrentTokenRepository
        {
            get { return _tokenRepository; }
            set { _tokenRepository = value; }
        }

    }
