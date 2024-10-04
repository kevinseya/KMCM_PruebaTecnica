using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace KMCM_PruebaTecnica.kmcm_util
{
	/// <summary>
	/// Proporciona métodos para encriptar y desencriptar texto utilizando el algoritmo AES (Advanced Encryption Standard).
	/// Esta clase utiliza una clave y un vector de inicialización (IV) configurados a través de la inyección de dependencias.
	/// </summary>
	public class kmcm_encript
	{
		private readonly byte[] _key; // Clave de encriptación
		private readonly byte[] _iv; // Vector de inicialización
		private readonly ILogger<kmcm_encript> _logger; // Logger para registrar información

		/// <summary>
		/// Inicializa una nueva instancia de la clase <see cref="kmcm_encript"/> con la configuración y el logger proporcionados.
		/// </summary>
		/// <param name="configuration">Configuración que contiene la clave y el IV para la encriptación.</param>
		/// <param name="logger">Logger para registrar información durante la encriptación y desencriptación.</param>
		public kmcm_encript(IConfiguration configuration, ILogger<kmcm_encript> logger)
		{
			// Convierte la clave y el IV de la configuración a un arreglo de bytes
			_key = Encoding.UTF8.GetBytes(configuration["EncryptionSettings:Key"]);
			_iv = Encoding.UTF8.GetBytes(configuration["EncryptionSettings:IV"]);
			_logger = logger; // Asigna el logger
		}

		/// <summary>
		/// Encripta el texto plano proporcionado utilizando AES.
		/// </summary>
		/// <param name="plainText">Texto plano a encriptar.</param>
		/// <returns>Texto encriptado en formato Base64.</returns>
		/// <exception cref="ArgumentNullException">Lanzado si el texto a encriptar es nulo o vacío.</exception>
		/// <exception cref="InvalidOperationException">Lanzado si ocurre un error durante la encriptación.</exception>
		public string Encrypt(string plainText)
		{
			if (string.IsNullOrEmpty(plainText)) // Verifica si el texto es nulo o vacío
			{
				throw new ArgumentNullException(nameof(plainText), "El texto a encriptar no puede ser nulo o vacío.");
			}

			try
			{
				using (Aes aesAlg = Aes.Create()) // Crea una instancia de Aes
				{
					aesAlg.Key = _key; // Establece la clave
					aesAlg.IV = _iv; // Establece el IV

					ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV); // Crea un transformador de encriptación

					using (MemoryStream msEncrypt = new MemoryStream()) // Flujo de memoria para almacenar datos encriptados
					{
						using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) // Flujo de criptografía para encriptar
						{
							using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) // Escritor de flujo para escribir el texto encriptado
							{
								Console.WriteLine($"Encriptando texto: {plainText}"); // Registra el texto a encriptar
								swEncrypt.Write(plainText); // Escribe el texto plano en el flujo
							}
							string encryptedText = Convert.ToBase64String(msEncrypt.ToArray()); // Convierte el flujo a Base64

							_logger.LogInformation($"Texto encriptado: {encryptedText}"); // Registra el texto encriptado
							return encryptedText; // Devuelve el texto encriptado
						}
					}
				}
			}
			catch (CryptographicException ex) // Captura excepciones de encriptación
			{
				throw new InvalidOperationException("Error al encriptar el texto.", ex);
			}
			catch (Exception ex) // Captura excepciones inesperadas
			{
				throw new InvalidOperationException("Error inesperado durante la encriptación.", ex);
			}
		}

		/// <summary>
		/// Desencripta el texto cifrado proporcionado utilizando AES.
		/// </summary>
		/// <param name="cipherText">Texto cifrado a desencriptar en formato Base64.</param>
		/// <returns>Texto desencriptado.</returns>
		/// <exception cref="ArgumentNullException">Lanzado si el texto cifrado es nulo o vacío.</exception>
		/// <exception cref="FormatException">Lanzado si el texto cifrado tiene un formato inválido.</exception>
		/// <exception cref="InvalidOperationException">Lanzado si ocurre un error durante la desencriptación.</exception>
		public string Decrypt(string cipherText)
		{
			if (string.IsNullOrEmpty(cipherText)) // Verifica si el texto es nulo o vacío
			{
				throw new ArgumentNullException(nameof(cipherText), "El texto cifrado no puede ser nulo o vacío.");
			}

			try
			{
				using (Aes aesAlg = Aes.Create()) // Crea una instancia de Aes
				{
					aesAlg.Key = _key; // Establece la clave
					aesAlg.IV = _iv; // Establece el IV

					ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV); // Crea un transformador de desencriptación

					using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText))) // Flujo de memoria para datos cifrados
					{
						using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) // Flujo de criptografía para desencriptar
						{
							using (StreamReader srDecrypt = new StreamReader(csDecrypt)) // Lector de flujo para leer el texto desencriptado
							{
								return srDecrypt.ReadToEnd(); // Lee y devuelve el texto desencriptado
							}
						}
					}
				}
			}
			catch (FormatException ex) // Captura excepciones de formato
			{
				throw new FormatException("El texto cifrado tiene un formato inválido.", ex);
			}
			catch (CryptographicException ex) // Captura excepciones de desencriptación
			{
				throw new InvalidOperationException("Error al desencriptar el texto.", ex);
			}
			catch (Exception ex) // Captura excepciones inesperadas
			{
				throw new InvalidOperationException("Error inesperado durante la desencriptación.", ex);
			}
		}
	}
}
