// Decompiled with JetBrains decompiler
// Type: SimplerAES
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SimplerAES
{
  private static byte[] key = new byte[32]
  {
    (byte) 123,
    (byte) 217,
    (byte) 19,
    (byte) 11,
    (byte) 24,
    (byte) 26,
    (byte) 85,
    (byte) 45,
    (byte) 114,
    (byte) 184,
    (byte) 1,
    (byte) 2,
    (byte) 6,
    (byte) 15,
    (byte) 32,
    (byte) 45,
    (byte) 51,
    (byte) 24,
    (byte) 175,
    (byte) 144,
    (byte) 173,
    (byte) 53,
    (byte) 196,
    (byte) 29,
    (byte) 24,
    (byte) 26,
    (byte) 17,
    (byte) 218,
    (byte) 131,
    (byte) 236,
    (byte) 53,
    (byte) 209
  };
  private static byte[] vector = new byte[16]
  {
    (byte) 146,
    (byte) 64,
    (byte) 191,
    (byte) 1,
    (byte) 2,
    (byte) 6,
    (byte) 15,
    (byte) 32,
    (byte) 45,
    (byte) 51,
    (byte) 121,
    (byte) 112,
    (byte) 79,
    (byte) 32,
    (byte) 114,
    (byte) 156
  };
  private ICryptoTransform encryptor;
  private ICryptoTransform decryptor;
  private UTF8Encoding encoder;

  public SimplerAES()
  {
    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    this.encryptor = rijndaelManaged.CreateEncryptor(SimplerAES.key, SimplerAES.vector);
    this.decryptor = rijndaelManaged.CreateDecryptor(SimplerAES.key, SimplerAES.vector);
    this.encoder = new UTF8Encoding();
  }

  public string Encrypt(string unencrypted) => Convert.ToBase64String(this.Encrypt(this.encoder.GetBytes(unencrypted)));

  public string Decrypt(string encrypted) => this.encoder.GetString(this.Decrypt(Convert.FromBase64String(encrypted)));

  public byte[] Encrypt(byte[] buffer) => this.Transform(buffer, this.encryptor);

  public byte[] Decrypt(byte[] buffer) => this.Transform(buffer, this.decryptor);

  protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
  {
    MemoryStream memoryStream = new MemoryStream();
    using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, transform, CryptoStreamMode.Write))
      cryptoStream.Write(buffer, 0, buffer.Length);
    return memoryStream.ToArray();
  }
}
