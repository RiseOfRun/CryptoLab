using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;

namespace cry3
{
    class Certif
    {

        private RSACryptoServiceProvider key;
        public Certif(int len)
        {
            key = new RSACryptoServiceProvider(len);
        }

        public void CreateCertificate(string name)
        {
            CertificateRequest req = new CertificateRequest("CN=Стас и Антон", key, 
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            req.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, false));
            req.CertificateExtensions.Add(new X509KeyUsageExtension(
                X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.NonRepudiation, false));
            req.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(
                new OidCollection
                {
                    new Oid("2.6.1.1.2.0.1.9")
                },
                true));
            req.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(req.PublicKey, false));
            X509Certificate2 certif =  req.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddYears(50));

            File.WriteAllBytes(name + ".cer", certif.Export(X509ContentType.Cert));
            certif.PrivateKey = key;
            File.WriteAllBytes(name + ".pem", certif.Export(X509ContentType.Pkcs12, "Fynjy123"));

            //key.ExportParameters(true).D;
        }










    }
}
