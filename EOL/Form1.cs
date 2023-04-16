using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using Yubico.YubiKey;
using Yubico.YubiKey.Piv;
using System.IO;
using System.Collections;

namespace EOL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static bool PinSubmitter(KeyEntryData pin)
        {
            string s = "123456";
            var s_b = Encoding.UTF8.GetBytes(s);
            pin.SubmitValue(s_b);
            return true;
        }

        // Encrypt
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxout.Text == "")
                {
                    MessageBox.Show("Please enter a message to encrypt.");
                    return;
                }

                //Assumes there is exactly one yubikey connected and it has a RSA2048 certificate in slot 9d
                //PIV PIN is assumed to be 123456
                var devices = YubiKeyDevice.FindAll();
                var ykDevice = devices.First();
                PivSession piv = new(ykDevice);

                piv.KeyCollector += PinSubmitter;
                piv.VerifyPin();

                var slot = PivSlot.KeyManagement;

                X509Certificate2 cert = piv.GetCertificate(slot);
                if (cert.SignatureAlgorithm.FriendlyName != "sha256RSA")
                    throw new CryptographicException("Certificate must be RSA with SHA256");

                var publicKey = cert.GetRSAPublicKey() ?? throw new CryptographicException("Couldn't get public key from certificate.");

                Aes aesFirst = Aes.Create();

                var encryptedKey = publicKey.Encrypt(aesFirst.Key, RSAEncryptionPadding.Pkcs1);
                var decryptedKey = piv.Decrypt(slot, encryptedKey);

                //MessageBox.Show($"aesFirst.Key.Length: {aesFirst.Key.Length}");
                //MessageBox.Show($"encryptedKey.Length: {encryptedKey.Length}");
                //MessageBox.Show($"decryptedKey.Length: {decryptedKey.Length}");

                // split the message into blocks of 128 bytes

                string message = textBoxout.Text;
                int blockSize = 128;
                int blockCount = (int)Math.Ceiling((double)message.Length / blockSize);

                string[] strings = new string[blockCount];
                FileStream sw = new FileStream(textBoxpath.Text, FileMode.Create, FileAccess.Write);

                for (int i = 0; i < blockCount; i++)
                {
                    int size = Math.Min(blockSize, message.Length - i * blockSize);
                    strings[i] = message.Substring(i * blockSize, size);

                    byte[] bytes = Encoding.ASCII.GetBytes(strings[i]);
                    var encryptedBytes = publicKey.Encrypt(bytes, RSAEncryptionPadding.Pkcs1);
                    sw.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                sw.Close();
                sw.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                //Assumes there is exactly one yubikey connected and it has a RSA2048 certificate in slot 9d
                //PIV PIN is assumed to be 123456
                var devices = YubiKeyDevice.FindAll();
                var ykDevice = devices.First();
                PivSession piv = new(ykDevice);

                piv.KeyCollector += PinSubmitter;
                piv.VerifyPin();

                var slot = PivSlot.KeyManagement;

                X509Certificate2 cert = piv.GetCertificate(slot);
                if (cert.SignatureAlgorithm.FriendlyName != "sha256RSA")
                    throw new CryptographicException("Certificate must be RSA with SHA256");

                var publicKey = cert.GetRSAPublicKey() ?? throw new CryptographicException("Couldn't get public key from certificate.");

                Aes aesFirst = Aes.Create();

                var encryptedKey = publicKey.Encrypt(aesFirst.Key, RSAEncryptionPadding.Pkcs1);
                var decryptedKey = piv.Decrypt(slot, encryptedKey);

                byte[] input = File.ReadAllBytes(textBoxpath.Text);

                // decrypt the input

                int blockSize = 256;
                int blockCount = (int)Math.Ceiling((double)input.Length / blockSize);

                byte[][] blocks = new byte[blockCount][];
                byte[] decripted = new byte[blockCount * blockSize];

                for (int i = 0; i < blockCount; i++)
                {
                    int size = Math.Min(blockSize, input.Length - i * blockSize);
                    blocks[i] = new byte[size];
                    Array.Copy(input, i * blockSize, blocks[i], 0, size);
                    var paddedDecryptedBytes = piv.Decrypt(slot, blocks[i]);
                    byte[] decryptedBytes;
                    bool couldParse = Yubico.YubiKey.Cryptography.RsaFormat.TryParsePkcs1Decrypt(paddedDecryptedBytes, out decryptedBytes);
                    Array.Copy(decryptedBytes, 0, decripted, i * blockSize, decryptedBytes.Length);

                    textBoxout.Text += Encoding.ASCII.GetString(decryptedBytes);
                }

                StreamWriter sw = new StreamWriter(textBoxpath.Text.Replace(".eol", ""));
                sw.Write(textBoxout.Text);
                sw.Dispose();
                textBoxout.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxpath.Text = openFileDialog.FileName;
                if (textBoxpath.Text.Substring(textBoxpath.Text.Length - 4) == ".eol")
                {

                }
                else
                {
                    textBoxout.Text = File.ReadAllText(textBoxpath.Text);
                    textBoxpath.Text += ".eol";
                }

            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}