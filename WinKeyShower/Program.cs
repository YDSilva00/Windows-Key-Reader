using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace WinKeyShower
{
    internal class Program
    {
        static void Main()
        {
            string key = GetKey();
            Console.WriteLine("Windows Product Key: " + key);
            Console.ReadLine();
        }

        static string GetKey()
        {
            RegistryKey localKey = RegistryKey.OpenBaseKey(
                RegistryHive.LocalMachine, RegistryView.Registry64);

            RegistryKey regKey = localKey.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            byte[] digitalProductId = (byte[])regKey.GetValue("DigitalProductId");

            return DecodeProductKey(digitalProductId);
        }

        static string DecodeProductKey(byte[] digitalProductId)
        {
            const string chars = "BCDFGHJKMPQRTVWXY2346789";
            int start = 52;
            char[] result = new char[29];

            // Check if it's Windows 8/10/11 style
            bool isWin8Plus = ((digitalProductId[66] / 6) & 1) != 0;
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xF7)
                                   | ((isWin8Plus ? 1 : 0) & 2) * 4);

            char[] key = new char[25];
            for (int i = 24; i >= 0; i--)
            {
                int cur = 0;
                for (int j = 14; j >= 0; j--)
                {
                    cur = cur * 256 ^ digitalProductId[j + start];
                    digitalProductId[j + start] = (byte)(cur / 24);
                    cur %= 24;
                }
                key[i] = chars[cur];
            }

            if (isWin8Plus)
            {
                int insertPos = 24 - (int)(key[0] - 'A'); // normalize char to 0-based index
                if (insertPos < 0) insertPos = 0;
                if (insertPos > 24) insertPos = 24;

                char[] newKey = new char[25];
                Array.Copy(key, 1, newKey, 0, insertPos);
                newKey[insertPos] = 'N';
                Array.Copy(key, insertPos + 1, newKey, insertPos + 1,
                           key.Length - insertPos - 1);
                key = newKey;
            }

            // Format: XXXXX-XXXXX-XXXXX-XXXXX-XXXXX
            int k = 0;
            for (int i = 0; i < 25; i++)
            {
                if (i > 0 && i % 5 == 0) result[k++] = '-';
                result[k++] = key[i];
            }

            return new string(result);
        }
    }
}
