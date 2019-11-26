

using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography;
using System.Security.Permissions;
namespace cry3
{
    class Program
    {
        static void Main(string[] args)
        {
            Certif c = new Certif(1024);
            c.CreateCertificate("main");
            c.EncryptFile("main", "9k.png");
        }
    }
}
