using System;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;



public static class CertificateRepository
    {

        public static X509Certificate2 GetOAuthSigningCertificate([Optional]string optionalVar)
        {
            string oauthCertificateFindType = "FindByThumbprint"; //ConfigurationManager.AppSettings["XeroApiOAuthCertificateFindType"];                 
           string oauthCertificateFindValue = optionalVar;
            if (string.IsNullOrEmpty(oauthCertificateFindType) || string.IsNullOrEmpty(oauthCertificateFindValue))
            {
                return null;
            }

            X509FindType x509FindType = (X509FindType)Enum.Parse(typeof(X509FindType), oauthCertificateFindType);

            X509Store certStore = new X509Store("My", StoreLocation.LocalMachine);
            // Search the LocalMachine certificate store for matching X509 certificates.
           // X509Store certStore = new X509Store("My", StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certificateCollection = certStore.Certificates.Find(x509FindType, oauthCertificateFindValue, false);
            certStore.Close();

            if (certificateCollection.Count == 0)
            {
                throw new ApplicationException(string.Format("An OAuth certificate matching the X509FindType '{0}' and Value '{1}' cannot be found in the local certificate store.", oauthCertificateFindType, oauthCertificateFindValue));
            }
            return certificateCollection[0];
        }

        /// <summary>
        /// Gets the OAuth signing certificate from the local certificate store, if specfified in app.config.
        /// </summary>
        /// <returns></returns>
      
        /// <summary>
        /// Return a CertificateFactory that can read the Client SSL certificate from the local machine certificate store
        /// </summary>
        /// <returns></returns>

    }

