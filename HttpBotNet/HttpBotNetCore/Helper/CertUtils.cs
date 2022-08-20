
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
namespace BotNetCore.Helper
{
    /// <summary>
    /// The CertificateUtil class can maybe help you create own certificate for building, signing and using encrypted HTTP traffic
    /// </summary>
    public static class CertificateUtil
    {
        /// <summary>
        /// Generates a SSL Certificate
        /// </summary>
        /// <param name="path">Path to your Certificates</param>
        /// 
        /// <param name="filename">Filename for your Certificate</param>
        /// 
        /// <param name="password">Passwort for PC</param>
        public static void MakeCert(string path, string filename, string password)
        {
            var ecdsa = ECDsa.Create(); // generate asymmetric key pair
            var req = new CertificateRequest("cn=HttpBotNetCore", ecdsa, HashAlgorithmName.SHA256);
            var cert = req.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.Now.AddYears(5));

            if(string.IsNullOrEmpty(path)) path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if(string.IsNullOrEmpty(filename)) filename = Path.GetRandomFileName();

            Console.WriteLine($@"Cert Info -> Path:{path} filename:{filename} +password:{password}");

            GeneralHelper.TryCreateDirectoryForThisFile(path.TrimEnd('\\') + '\\' + filename + ".pfx");
            // Password for PK
            // Create PFX (PKCS #12) with private key
            if(string.IsNullOrEmpty( password))
            {
                File.WriteAllBytes(path.TrimEnd('\\') + '\\' + filename + ".pfx", cert.Export(X509ContentType.Pfx));
            }
            else
            {
                File.WriteAllBytes(path.TrimEnd('\\') + '\\' + filename + ".pfx", cert.Export(X509ContentType.Pfx, @password));
            }

            // Create Base 64 encoded CER (public key only)
            File.WriteAllText(path.TrimEnd('\\') + '\\' + filename + ".cert",
                "-----BEGIN CERTIFICATE-----\r\n"
                + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                + "\r\n-----END CERTIFICATE-----");

                //using (RSA parent = RSA.Create(4096))  Not used but a nice implementation maybe later Feature
                //using (RSA rsa = RSA.Create(2048))
                //{
            //    CertificateRequest parentReq = new CertificateRequest(
            //        "CN=Experimental Issuing Authority",
            //        parent,
            //        HashAlgorithmName.SHA256,
            //        RSASignaturePadding.Pkcs1);

            //    parentReq.CertificateExtensions.Add(
            //        new X509BasicConstraintsExtension(true, false, 0, true));

            //    parentReq.CertificateExtensions.Add(
            //        new X509SubjectKeyIdentifierExtension(parentReq.PublicKey, false));

            //    using (X509Certificate2 parentCert = parentReq.CreateSelfSigned(
            //        DateTimeOffset.UtcNow.AddDays(-45),
            //        DateTimeOffset.UtcNow.AddDays(365)))
            //    {
            //        CertificateRequest req = new CertificateRequest(
            //            "CN=Valid-Looking Timestamp Authority",
            //            rsa,
            //            HashAlgorithmName.SHA256,
            //            RSASignaturePadding.Pkcs1);

            //        req.CertificateExtensions.Add(
            //            new X509BasicConstraintsExtension(false, false, 0, false));

            //        req.CertificateExtensions.Add(
            //            new X509KeyUsageExtension(
            //                X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.NonRepudiation,
            //                false));

            //        req.CertificateExtensions.Add(
            //            new X509EnhancedKeyUsageExtension(
            //                new OidCollection
            //                {
            //        new Oid("1.3.6.1.5.5.7.3.8")
            //                },
            //                true));

            //        req.CertificateExtensions.Add(
            //            new X509SubjectKeyIdentifierExtension(req.PublicKey, false));

            //        using (X509Certificate2 cert = req.Create(
            //            parentCert,
            //            DateTimeOffset.UtcNow.AddDays(-1),
            //            DateTimeOffset.UtcNow.AddDays(90),
            //            new byte[] { 1, 2, 3, 4 }))
            //        {

            //            // Do something with these certs, like export them to PFX,
            //            // or add them to an X509Store, or whatever.
            //        }
            //    }
            //}
            ecdsa.Dispose();


        }
        /// <summary>
        /// Helps to Save Certificate to OS User Storeage
        /// </summary>
        /// <param name="path">Path to your Certificates</param>
        /// 
        /// <param name="filename">Filename for your Certificate</param>
        /// 
        /// <param name="password">Passwort for PC</param>
        public static void SaveCertForUser(string path, string filename, string password)
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);

            if (string.IsNullOrEmpty(path)) path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(filename)) filename = Path.GetRandomFileName();

            Console.WriteLine($@"Cert Info -> Path:{path} filename:{filename} +password:{password}");
            using (var cert = new X509Certificate2(@path.TrimEnd('\\') + '\\' + filename + ".pfx", @password, X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet))
            {
                using(var innerCert = new X509Certificate2(cert))
                {
                    store.Add(innerCert);
                }
            }
                
            store.Close();
        }
    }
}
