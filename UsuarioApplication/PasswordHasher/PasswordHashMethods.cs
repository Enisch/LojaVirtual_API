using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UsuarioApplication.PasswordHasher
{
    public class PasswordHashMethods
    {
        const int KeySize = 64;
        const int Iterations = 10000;
        const int SaltSize = 128 / 8;
        HashAlgorithmName hashAl = HashAlgorithmName.SHA512;
        char Delimiter = ';';
        public byte[] PasswordHash(string password)
        {
            var Salt = RandomNumberGenerator.GetBytes(SaltSize);
            var Hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),Salt,Iterations,hashAl,KeySize); 
            var senha = String.Join(Delimiter, Convert.ToBase64String(Hash), Convert.ToBase64String(Salt));
            return Encoding.UTF8.GetBytes(senha);
        }
        
        public bool PasswordCheck(string passwordInput, byte[] Password) 
        {
           var Senha= Encoding.UTF8.GetString(Password);
            var elements = Senha.Split(Delimiter);
            var Salt = Convert.FromBase64String(elements[1]);
            var Hash = Convert.FromBase64String(elements[0]);

            var InputToHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(passwordInput), Salt, Iterations, hashAl, KeySize);

            return CryptographicOperations.FixedTimeEquals(Hash, InputToHash);
        }
    }
}
