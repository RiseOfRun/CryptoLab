using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace cry3
{
    class Certif
    {
        private const string PASS = "Fynjy123";
        private const string AUTH = "Стас и Антон";
        private RSACryptoServiceProvider key;
        public Certif(int len)
        {
            key = new RSACryptoServiceProvider(len);
        }

        public void CreateCertificate(string name)
        {
            CertificateRequest req = new CertificateRequest("CN=" + AUTH, key, 
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);
            req.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(req.PublicKey, false));
            X509Certificate2 certif =  req.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddYears(50));
            File.WriteAllBytes(name + ".cer", certif.Export(X509ContentType.Cert));
            certif.PrivateKey = key;
            File.WriteAllBytes(name + ".pem", certif.Export(X509ContentType.Pkcs12, PASS));
        }

        public void EncryptFile(string nameCert, string nameFile)
        {
            X509Certificate2 cert = new X509Certificate2(nameCert + ".pem", PASS);
            byte[] file = File.ReadAllBytes(nameFile);
            RSACryptoServiceProvider csp = cert.PrivateKey as RSACryptoServiceProvider;
            byte[] computeHash = csp.SignData(file, new SHA256CryptoServiceProvider());
            byte[] result = file.Concat(computeHash).ToArray();
            File.WriteAllBytes("cer_" + nameFile, result);
        }

        private static string SignMyData()
        {
            string part1 = "5BC44FA0945347E590EB002F519F1D73";
            X509Certificate2 x509 = new X509Certificate2("mycert.pfx", "mypassword");

            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] originalData = encoding.GetBytes(part1);

            // Create an instance of the RSA encryption algorithm
            // and perform the actual encryption
            using (RSACryptoServiceProvider csp = (RSACryptoServiceProvider)x509.PrivateKey)
            {
                byte[] computedHash = csp.SignData(originalData, new MD5CryptoServiceProvider());

                string part2 = ByteArrayToHexString(computedHash);

                return part1 + ";0x" + part2;
            }
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }











    }
}
