# EOL
This is a simple file encryption program written C#.
It uses the yubikey's ability to store a secret key to encrypt and decrypt files.

The original idea for this program was to be used to store a text file with instructions on what to do in the event of the yubikey holder's death.
Hence the name EOL (End Of Life). An example file can be found on this repo https://github.com/potatoqualitee/eol-dr


## Usage
1. Setup your yubikey with the secret key you want to use.
   1. Install the yubikey manager from https://www.yubico.com/support/download/yubikey-manager/
   2. Open the yubikey manager and plug in your yubikey
   3. Select Applications -> PIV -> Configure certificates -> Key management -> Generate key
2. Download the latest release from https://github.com/Nathanwoodburn/EOL/releases or build it yourself
3. Run the program and open the file you wish to encrypt.
4. The program will encrypt the file and save it to the same directory with the same name but with a .eol extension.
5. To decrypt the file, open the file with the .eol extension and the program will decrypt it and save it to the same directory with the same name but with the .eol extension removed.


[Example Video](EOL%20Encrypter.mp4)