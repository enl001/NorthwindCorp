using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindCorp.Core.Helpers
{
  public static class ImageHelper
  {
    public static byte[] AddOleHeader(byte[] data)
    {
      string oleString = "000101010001110000101111000000000000001000000000000000000000000000001101000000000000111000000000000101000000000000100001000000001111111111111111111111111111111101000010011010010111010001101101011000010111000000100000010010010110110101100001011001110110010100000000010100000110000101101001011011100111010000101110010100000110100101100011011101000111010101110010011001010000000000000001000001010000000000000000000000100000000000000000000000000000011100000000000000000000000001010000010000100111001001110101011100110110100000000000000000000000000000000000000000000000000000000000000000000000000010100000001010010000000000000000";
      var ole = ImageHelper.GetOle(oleString);
      byte[] newData = new byte[data.Length + 78];
      System.Buffer.BlockCopy(ole, 0, newData, 0, ole.Length);
      System.Buffer.BlockCopy(data, 0, newData, ole.Length, data.Length);
      return newData;
    }

    private static byte[] GetOle(string input)
    {
      int numOfBytes = input.Length / 8;
      byte[] bytes = new byte[numOfBytes];
      for (int i = 0; i < numOfBytes; ++i)
      {
        bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
      }
      return bytes;
    }

  }
}
