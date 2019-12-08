

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
            //c.CreateCertificate("main");
            //c.SignFile("main.pem", "selfi1.jpg");
            Console.WriteLine($"{c.Validate("test.cer", "selfi1.jpg.sgn", "selfi1.jpg.sha256")}");

        }
    }
}
