using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace LiteLock
{
    public partial class Locker : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private Thread cryptoThread;
        private EncryptDecrypt cryptor;
        public bool isClosing = false;
        public bool isWorking = false;

        public Locker()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.overwriteOriginal.Checked = Properties.Settings.Default.overwriteBox;
            cryptor = new EncryptDecrypt(this);
            this.encryptionProgress.Visible = false;
            this.currentFileLabel.Visible = false;
            this.Height = 254;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
                foreach (string a in args)
                    MessageBox.Show(a);
        }

        private void EncryptLabel_DragDrop(object sender, DragEventArgs e)
        {
            if (isWorking)
                return;
            SetForegroundWindow(this.Handle);
            Password pass = new Password();
            if (pass.ShowDialog(this) == DialogResult.OK)
            {
                string p = pass.textBox1.Text.ToString();
                pass.Dispose();
                Password pass2 = new Password();
                pass2.Text = "Enter Again";
                if (pass2.ShowDialog(this) == DialogResult.OK)
                {
                    string p2 = pass2.textBox1.Text.ToString();
                    pass2.Dispose();
                    if (p.Equals(p2))
                    {
                        isWorking = true;
                        cryptoThread = new Thread(() => EncryptList(e,p));
                        cryptoThread.Start();
                    }
                    else
                    {
                        MessageBox.Show("Password Mismatch. Please try again.","LiteLock");
                    }
                }
            }
        }

        private void EncryptList(DragEventArgs e, string p)
        {
            List<string> filepaths = new List<string>();
            this.Invoke((MethodInvoker)delegate
            {
                this.currentFileLabel.Visible = true;
                this.Height = 347;
            });
            try
            {
                filepaths = getFilepaths((string[])(e.Data.GetData(DataFormats.FileDrop)));
            }
            catch (Exception) {}
            int filesCrypted = 0;
            this.Invoke((MethodInvoker)delegate
            {
                this.encryptionProgress.Maximum = filepaths.Count;
                this.encryptionProgress.Value = filesCrypted;
                this.encryptionProgress.Visible = true;
                this.fileProgressBar.Visible = true;
            });
            foreach (string fileLoc in filepaths)
            {
                if (isClosing)
                    break;
                this.Invoke((MethodInvoker)delegate
                {
                    this.encryptionProgress.Value = filesCrypted;
                    this.currentFileLabel.Text = "Encrypting: " + fileLoc;
                });
                if (File.Exists(fileLoc))
                {
                    if (cryptor.EncryptFile(fileLoc, fileLoc + ".locking", p)) //Encrypt the file
                    {
                        try
                        {
                            if (overwriteOriginal.Checked)
                            {
                                try
                                {
                                    cryptor.EncryptFile(fileLoc + ".locking", fileLoc, p); //Overwrite the original with an encrypted version of the encrypted file.
                                }
                                catch (Exception exception)
                                {
                                    MessageBox.Show(exception.Message.ToString());
                                }
                            }
                            File.Delete(fileLoc);
                            File.Move(fileLoc + ".locking", fileLoc);
                        }
                        catch { }

                    }
                    else
                    {
                        try
                        {
                            File.Delete(fileLoc + ".locking");
                            break;
                        }
                        catch { }
                    }
                }
                filesCrypted++;
            }
            this.Invoke((MethodInvoker)delegate
            {
                this.encryptionProgress.Value = filesCrypted;
                this.encryptionProgress.Visible = false;
                this.currentFileLabel.Visible = false;
                this.fileProgressBar.Visible = false;
                this.Height = 254;
                if (isClosing)
                {
                    isClosing = false;
                    this.Text = "LiteLock v2.0 - Created by DarkTussin";
                    //this.Close();
                }
                isWorking = false;
            });
        }

        private void DecryptList(DragEventArgs e, string p)
        {
            List<string> filepaths = new List<string>();
            this.Invoke((MethodInvoker)delegate
            {
                this.currentFileLabel.Visible = true;
                this.Height = 347;
            });
            try
            {
                filepaths = getFilepaths((string[])(e.Data.GetData(DataFormats.FileDrop)));
            }
            catch (Exception) {}
            int filesCrypted = 0;
            this.Invoke((MethodInvoker)delegate
            {
                this.encryptionProgress.Maximum = filepaths.Count;
                this.encryptionProgress.Value = filesCrypted;
                this.encryptionProgress.Visible = true;
            });
            foreach (string fileLoc in filepaths)
            {
                if (isClosing)
                    break;
                this.Invoke((MethodInvoker)delegate
                {
                    this.encryptionProgress.Value = filesCrypted;
                    this.fileProgressBar.Visible = true;
                    this.currentFileLabel.Text = "Decrypting: " + fileLoc;
                });
                if (File.Exists(fileLoc))
                {
                    if (cryptor.DecryptFile(fileLoc, fileLoc + ".unlocking", p))
                    {
                        try
                        {
                            File.Delete(fileLoc);
                            File.Move(fileLoc + ".unlocking", fileLoc);
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            File.Delete(fileLoc + ".unlocking");
                            break;
                        }
                        catch { }
                    }

                }
                filesCrypted++;
            }
            this.Invoke((MethodInvoker)delegate
            {
                this.encryptionProgress.Value = filesCrypted;
                this.encryptionProgress.Visible = false;
                this.currentFileLabel.Visible = false;
                this.fileProgressBar.Visible = false;
                this.Height = 254;
                if (isClosing)
                {
                    isClosing = false;
                    this.Text = "LiteLock v2.0 - Created by DarkTussin";
                    //this.Close();
                }
                isWorking = false;
            });
        }

        private void DecryptLabel_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (isWorking)
                return;
            SetForegroundWindow(this.Handle);
            using (Password pass = new Password())
            {
                if (pass.ShowDialog(this) == DialogResult.OK)
                {
                    string p = pass.textBox1.Text.ToString();
                    isWorking = true;
                    cryptoThread = new Thread(() => DecryptList(e, p));
                    cryptoThread.Start();
                }
                pass.Dispose();
            }
        }

        private void elevate()
        {
            Process elevated = new Process();
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;
            proc.Verb = "runas";
            elevated.StartInfo = proc;
            try
            {
                elevated.Start();
                this.Invoke((MethodInvoker)delegate { Environment.Exit(0); });
            }
            catch(Exception e)
            {
                this.Invoke((MethodInvoker)delegate { MessageBox.Show("Elevation Failed","LiteLock"); });
                throw e;
            }
        }

        private List<string> getFilepaths(string[] paths)
        {
            List<string> filepaths = new List<string>();
            foreach (string fileLoc in paths)
            {
                if (File.Exists(fileLoc))
                {
                    filepaths.Add(fileLoc);
                }
                else if (Directory.Exists(fileLoc))
                {
                    this.Invoke((MethodInvoker)delegate { 
                        this.currentFileLabel.Text = "Enumerating Directory: " + fileLoc;
                    });
                    try
                    {
                        filepaths.AddRange(Directory.EnumerateFiles(fileLoc, "*.*", SearchOption.AllDirectories));
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        MessageBox.Show("LiteLock needs Administrative Access to Enumerate this Directory." + Environment.NewLine + Environment.NewLine +
                            "This is not currently supported - Please move the files to a readable location."
                            ,"Elevation Required");
                        //elevate();
                        throw e;
                    }
                }
            }
            return filepaths;
        }

        private void EncryptLabel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void DecryptLabel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Locker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosing)
            {
                isClosing = true;
                try
                {
                    if (cryptoThread.IsAlive)
                    {
                        e.Cancel = true;
                        this.Text = "Stopping Encryption/Decryption...";
                        Thread.Sleep(1000);
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    Properties.Settings.Default.overwriteBox = this.overwriteOriginal.Checked;
                    Properties.Settings.Default.Save();
                }
                catch { }
            }
        }
    }
}
