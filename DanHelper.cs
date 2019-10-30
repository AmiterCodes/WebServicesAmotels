using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

using System.Threading.Tasks;

/// <summary>
/// A bad name Amit made. ("bad name? more like badass name" - Amit) He also decided I should put a summary here.
/// </summary>
namespace Amotels
{
    /// <summary>
    /// The DanHelper(tm) Blockchain cryptocurrency next-gen Windows 11 level API  is a revolutionary
    /// </summary>
    public static class DanHelper
    {       
        /// <summary>
        /// Says something using the Dan API
        /// </summary>
        /// <param name="sentence">what to say (has to be formatted correctly)</param>
        public static void Say(string sentence)
        {
            
            string s = sentence
                    .Replace("ch", "2")
                    .Replace("ts", "3")
                    .Replace("sh", "4");
            for (int i = 0; i < s.Length; i += 2)
            {
                string substring;
                if (i == s.Length - 1) substring = s[i] + "1";
                else
                    substring = s.Substring(i, 2);

                try
                {
                    SoundPlayer p = new SoundPlayer(@".\sounds\" + substring + ".wav");
                    p.PlaySync();
                } catch(FileNotFoundException e)
                {
                    throw new DanException(e.FileName);
                }
            }
        }
    }

    /// <summary>
    /// This class represents an exception that will occur if the dan API is unable to say a certain sentence or phrasing.
    /// </summary>
    public class DanException : Exception
    {
        /// <summary>
        /// The constructor for the DanException class.
        /// </summary>
        /// <param name="fileName">The sentence or phrase that the dan API cannot say</param>
        public DanException(string fileName) : base("Dan could not say the following line: " + fileName)
        {

        }
    }
}
