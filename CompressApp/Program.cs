using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressApp
{
    class Program
    {    
        static void Main(string[] args)
        {
            Console.Write("Type in file path: ");
            string filePath = Console.ReadLine();
            string fileName = @filePath;
            Console.Write("Select compress or decompress: (c/d)");
            string modeSelect = Console.ReadLine();
            Console.Write("Set a name for the new file to create: ");
            string newFileName = Console.ReadLine();
            Stopwatch sw = new Stopwatch();
            
            if (modeSelect.Equals("c"))
            {           
                sw.Start();
                Compress(fileName, newFileName);
            }
            else if (modeSelect.Equals("d"))
            {
                sw.Start();
                Decompress(fileName, newFileName);
            }
            
            sw.Stop();
            long timer = sw.ElapsedMilliseconds;
            Console.WriteLine("Runtime: " + timer);
            Console.WriteLine("Press any key to close the window");
            Console.Read();
        }

        public static void Compress(string fileName, string compressedFileName)
        {
            using (FileStream inputStream = File.OpenRead(fileName))
            {
                FileStream outputStream = File.OpenWrite(compressedFileName);
                using (var compressStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    try
                    {
                        inputStream.CopyTo(compressStream);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }                    
                }
            }
        }

        public static void Decompress(string fileName, string newFileName)
        {
            using (FileStream inFile = File.OpenRead(fileName))
            {
                using (FileStream outFile = File.Create(newFileName))
                {
                    using (GZipStream compress = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        try
                        {
                            compress.CopyTo(outFile);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }                       
                    }
                }
            }
        }       
    }
}
