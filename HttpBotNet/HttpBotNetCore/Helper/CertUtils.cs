
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace BotNetCore.Helper
{
    public class CertificateUtil
    {
        /// <summary>
        /// Generates a SSL Certificate
        /// </summary>
        /// <param name="path"></param>
        /// Path for PK and Certfile
        /// <param name="filename"></param>
        /// Filename for Cert and PK
        /// <param name="password"></param>
        /// Password for PK
        public static void MakeCert(string path, string filename, string password)
        {
            var ecdsa = ECDsa.Create(); // generate asymmetric key pair
            var req = new CertificateRequest("cn=netIcqbot", ecdsa, HashAlgorithmName.SHA256);
            var cert = req.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.Now.AddYears(5));

            // Create PFX (PKCS #12) with private key
            File.WriteAllBytes(path.TrimEnd('\\') + '\\' + filename + ".pfx", cert.Export(X509ContentType.Pfx, @password));

            // Create Base 64 encoded CER (public key only)
            File.WriteAllText(path.TrimEnd('\\') + '\\' + filename + ".cert",
                "-----BEGIN CERTIFICATE-----\r\n"
                + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                + "\r\n-----END CERTIFICATE-----");

            //using (RSA parent = RSA.Create(4096))  Schönes Codesnippet aber derzeit nicht benötigt
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

        }

        public static void SaveCertForUser(string path, string filename, string password)
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            store.Add(new X509Certificate2(new X509Certificate2(@path.TrimEnd('\\') + '\\' + filename + ".pfx", @password, X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet)));
            store.Close();
        }
    }
}
