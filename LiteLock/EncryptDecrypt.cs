using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.IO.Compression;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace LiteLock
{
    public class EncryptDecrypt
    {
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
        private Locker caller;
        public EncryptDecrypt(Locker callingForm)
        {
            caller = callingForm;
        }
        public bool EncryptFile(string inputFile, string outputFile, string password)
        {
            caller.Invoke((MethodInvoker)delegate
            {
                caller.fileProgressBar.Maximum = 100;
                caller.fileProgressBar.Value = 0;
            });
            long totalBytes = new FileInfo(inputFile).Length;
            byte[] salt = new byte[16];
            rand.GetBytes(salt);
            byte[] IV = new byte[16];
            rand.GetBytes(IV);
            byte[] key = new Rfc2898DeriveBytes(password, salt, 77).GetBytes(32);
            string cryptFile = outputFile;
            try
            {
                using(FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create))
                {
                    fsCrypt.Write(salt, 0, salt.Length);
                    fsCrypt.Write(IV, 0, IV.Length);
                    using (RijndaelManaged RMCrypto = new RijndaelManaged())
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateEncryptor(key, IV), CryptoStreamMode.Write))
                        {
                            using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                            {
                                byte[] buffer = new byte[1024*1024];
                                int data;
                                long bytesRead = 0;
                                while((data = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    bytesRead += data;
                                    if (!caller.isClosing)
                                        caller.Invoke((MethodInvoker)delegate { caller.fileProgressBar.Value = ((int)(((double)bytesRead / totalBytes) * 100)); });
                                    else
                                        return false;
                                    cs.Write(buffer, 0, data);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                try
                {
                    caller.Invoke((MethodInvoker)delegate
                    {
                        if(!caller.isClosing)
                            MessageBox.Show("Error Encrypting File", "Encryption Failure");
                    });
                }
                catch { }
                return false;
            }
        }

        public bool DecryptFile(string inputFile, string outputFile, string password)
        {
            long totalBytes = new FileInfo(inputFile).Length;
            caller.Invoke((MethodInvoker)delegate
            {
                caller.fileProgressBar.Maximum = 100;
                caller.fileProgressBar.Value = 0;
            });
            byte[] salt = new byte[16];
            byte[] IV = new byte[16];
            try
            {
                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open)) {
                    fsCrypt.Read(salt, 0, salt.Length);
                    fsCrypt.Read(IV, 0, IV.Length);
                    byte[] key = new Rfc2898DeriveBytes(password, salt, 77).GetBytes(32);
                    using (RijndaelManaged RMCrypto = new RijndaelManaged())
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, IV), CryptoStreamMode.Read))
                        {
                            using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                            {
                                byte[] buffer = new byte[1024*1024];
                                int data;
                                long bytesRead = 0;
                                while((data = cs.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    bytesRead += data;
                                    if (!caller.isClosing)
                                        caller.Invoke((MethodInvoker)delegate { caller.fileProgressBar.Value = ((int)(((double)bytesRead / totalBytes) * 100)); });
                                    else
                                        return false;
                                    fsOut.Write(buffer, 0, data);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                try
                {
                    caller.Invoke((MethodInvoker)delegate
                    {
                        if (!caller.isClosing)
                            MessageBox.Show("Error Decrypting File(s). Invalid Password", "Decryption Failure");
                    });
                }
                catch { }
                return false;
            }
        }

        //Call this function to remove the key from memory after use for security
        //[System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        //public static extern bool ZeroMemory(IntPtr Destination, int Length);
    }
}