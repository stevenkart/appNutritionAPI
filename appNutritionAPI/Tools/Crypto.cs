using System.Security.Cryptography;
using System.Text;

namespace appNutritionAPI.Tools
{
    public class Crypto
    {
        //Esta clase contiene los métodos necesarios para encriptar
        //no solo contraseñas sino cualquier dato importante
        //TODO: utilizar estas funciona para encriptar la cadena de conexión 

        const string key = "Pr0gr@m@ci0n6?_(S3curityK3y)_APP";
        const string iv = "_%S3c0nd~K3y=#$_";

        //encriptacion AES256 source
        //https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-6.0 


        public string DesEncriptarPassword(string Pass)
        {
            StringBuilder sb = new StringBuilder();

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] ciphertextBytes = Convert.FromBase64String(Pass);
                byte[] plaintextBytes = decryptor.TransformFinalBlock(ciphertextBytes, 0, ciphertextBytes.Length);

                sb.Append(Encoding.UTF8.GetString(plaintextBytes));
            }

            return sb.ToString();
        }

        public string EncriptarPassword(string Pass)
        {
            StringBuilder sb = new StringBuilder();

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] plaintextBytes = Encoding.UTF8.GetBytes(Pass);
                byte[] ciphertextBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

                sb.Append(Convert.ToBase64String(ciphertextBytes));
            }

            return sb.ToString();
        }

        public string EncriptarEnUnSentido(string Entrada)
        {
            string PorEncriptar = EncriptarPassword(Entrada);

            PorEncriptar += "PalabraClave";

            SHA256CryptoServiceProvider ProveedorCrypto = new SHA256CryptoServiceProvider();

            //Descompone la cadenaDeEntrada en Bytes
            byte[] BytesDeEntrada = Encoding.UTF8.GetBytes(PorEncriptar);

            //Usando los bytes de la cadena de entrada crea el Hash
            byte[] BytesConHash = ProveedorCrypto.ComputeHash(BytesDeEntrada);

            StringBuilder Resultado = new StringBuilder();

            //el for recorre cada byte del Hash y lo agrega a una cadena (stringbuilder)
            for (int i = 0; i < BytesConHash.Length; i++)
                Resultado.Append(BytesConHash[i].ToString("x2").ToLower());
            // el x2 lo que hace es poner los caracteres hexadecimales con cierto formato.

            return Resultado.ToString();

        }
    }
}
