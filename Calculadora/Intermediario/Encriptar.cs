using System.Security.Cryptography;
using System.Text;

namespace Calculadora.Intermediario
{
    public class Encriptar
    {
        public static string EncriptarContraseña(string contraseña,string pin)
        {
            byte[] llave = Encoding.UTF8.GetBytes(pin);
            byte[] vectorInicializacion = Encoding.UTF8.GetBytes("Un vector de inicialización seguro");

            byte[] bytesContraseña = Encoding.UTF8.GetBytes(contraseña);

            using (var cifrado = new RijndaelManaged())
            {
                cifrado.Mode = CipherMode.CBC;
                cifrado.Padding = PaddingMode.PKCS7;

                using (var encriptador = cifrado.CreateEncryptor(llave, vectorInicializacion))
                {
                    byte[] bytesEncriptados = encriptador.TransformFinalBlock(bytesContraseña, 0, bytesContraseña.Length);
                    return Convert.ToBase64String(bytesEncriptados);
                }
            }
        }
        public static string DesencriptarContraseña(string contraseñaEncriptada,string pin)
        {
            byte[] llave = Encoding.UTF8.GetBytes(pin);
            byte[] vectorInicializacion = Encoding.UTF8.GetBytes("Un vector de inicialización seguro");

            byte[] bytesContraseñaEncriptada = Convert.FromBase64String(contraseñaEncriptada);

            using (var cifrado = new RijndaelManaged())
            {
                cifrado.Mode = CipherMode.CBC;
                cifrado.Padding = PaddingMode.PKCS7;

                using (var desencriptador = cifrado.CreateDecryptor(llave, vectorInicializacion))
                {
                    byte[] bytesDesencriptados = desencriptador.TransformFinalBlock(bytesContraseñaEncriptada, 0, bytesContraseñaEncriptada.Length);
                    return Encoding.UTF8.GetString(bytesDesencriptados);
                }
            }
        }


    }
}
