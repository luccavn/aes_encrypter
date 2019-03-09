using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PartyEncrypter
{
    internal static class Program
    {
        private static void Main(string[] args) 
        {
            foreach (FileInfo fileInfo in args.Where(File.Exists).Select(file => new FileInfo(file)))
            {
                CriptografarArquivo(fileInfo.FullName,
                    $@"{fileInfo.DirectoryName}\{fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length)}_criptografado{fileInfo.Extension}");

                //DescriptografarArquivo(fileInfo.FullName,
                //    $@"{fileInfo.DirectoryName}\{fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length)}_descriptografado{fileInfo.Extension}");
            }
        }

        private static void CriptografarArquivo(string arquivoEntrada, string arquivoSaida)
        {
            const string senha = "12345678";

            UnicodeEncoding codificadorUnicode = new UnicodeEncoding();

            byte[] chave = codificadorUnicode.GetBytes(senha);

            FileStream streamSaida = new FileStream(arquivoSaida, FileMode.Create);

            RijndaelManaged rijndael = new RijndaelManaged();

            CryptoStream encriptadorDeStreams = new CryptoStream(streamSaida, rijndael.CreateEncryptor(chave, chave),
                CryptoStreamMode.Write);

            FileStream streamEntrada = new FileStream(arquivoEntrada, FileMode.Open);

            int dados;

            while ((dados = streamEntrada.ReadByte()) != -1)
            {
                encriptadorDeStreams.WriteByte((byte) dados);
            }

            streamEntrada.Close();
            encriptadorDeStreams.Close();
            streamSaida.Close();
        }

        private static void DescriptografarArquivo(string arquivoEntrada, string arquivoSaida)
        {
            const string senha = "12345678";

            UnicodeEncoding codificadorUnicode = new UnicodeEncoding();

            byte[] chave = codificadorUnicode.GetBytes(senha);

            FileStream streamSaida = new FileStream(arquivoSaida, FileMode.Create);

            RijndaelManaged rijndael = new RijndaelManaged();

            CryptoStream encriptadorDeStreams = new CryptoStream(streamSaida,
                rijndael.CreateDecryptor(chave, chave),
                CryptoStreamMode.Write);

            FileStream streamEntrada = new FileStream(arquivoEntrada, FileMode.Open);

            int dados;

            while ((dados = streamEntrada.ReadByte()) != -1)
            {
                encriptadorDeStreams.WriteByte((byte) dados);
            }

            streamEntrada.Close();
            encriptadorDeStreams.Close();
            streamSaida.Close();
        }
    }
}
