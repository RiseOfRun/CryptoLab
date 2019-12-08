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
            X509Certificate2 certif = req.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddYears(50));
            File.WriteAllBytes(name + ".cer", certif.Export(X509ContentType.Cert));
            certif.PrivateKey = key;
            File.WriteAllBytes(name + ".pem", certif.Export(X509ContentType.Pkcs12, PASS));
        }

        public void SignFile(string nameCert, string nameFile)
        {
            X509Certificate2 cert = new X509Certificate2(nameCert, PASS);
            SHA256CryptoServiceProvider hasher = new SHA256CryptoServiceProvider();
            byte[] file = File.ReadAllBytes(nameFile);
            byte[] hash = hasher.ComputeHash(file);
            RSACryptoServiceProvider csp = cert.PrivateKey as RSACryptoServiceProvider;
            byte[] certHash = csp.SignData(file, new SHA256CryptoServiceProvider());
            File.WriteAllBytes(nameFile + ".sgn", certHash);
            File.WriteAllBytes(nameFile + ".sha256", hash);
        }



        public bool ValidateHash(string nameCert, string sgnFilePath, string hashpath)
        {
            X509Certificate2 cert = new X509Certificate2(nameCert);
            byte[] sgnfile = File.ReadAllBytes(sgnFilePath);

            byte[] hashfile = File.ReadAllBytes(hashpath);
            bool flag = false;
            RSACryptoServiceProvider csp = cert.PublicKey.Key as RSACryptoServiceProvider;
            try { flag = flag || csp.VerifyHash(hashfile, sgnfile, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyHash(hashfile, sgnfile, HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyHash(hashfile, sgnfile, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyHash(hashfile, sgnfile, HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyHash(hashfile, sgnfile, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1); } catch { };
            return flag;
        }


        public bool ValidateFile(string nameCert, string sgnFilePath, string path)
        {
            X509Certificate2 cert = new X509Certificate2(nameCert);
            byte[] sgnfile = File.ReadAllBytes(sgnFilePath);

            byte[] file = File.ReadAllBytes(path);
            bool flag = false;
            RSACryptoServiceProvider csp = cert.PublicKey.Key as RSACryptoServiceProvider; 
            try { flag = flag || csp.VerifyData(file, sgnfile, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyData(file, sgnfile, HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyData(file, sgnfile, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyData(file, sgnfile, HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1); } catch { };
            try { flag = flag || csp.VerifyData(file, sgnfile, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1); } catch { };
            return flag;
        }











    }
}
