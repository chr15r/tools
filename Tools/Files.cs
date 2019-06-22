using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools
{
    public class Files
    {
        public static string folderRoot = @"C:\Users\chris\Documents\Test";
        public static string regexPattern = @"console\.log\(([^)]+)\);";
        public static List<string> foldersToIgnore = new List<string> { "Ignore", "Ignore1" };
        public static List<string> filesToIgnore = new List<string> { "ConsoleLogIgnore1.txt" };
        public static List<string> fileExtensionsToIgnore = new List<string> { ".js" };
        public static List<string> directoriesToScan = new List<string>();
        public static List<string> filesToScan = new List<string>();

        public static void ReplaceTextInFile()
        {
            // Get directories to scan
            GetDirectories(folderRoot);

            // Get files to scan
            GetFiles();

            foreach (string f in filesToScan)
            {
                string text = File.ReadAllText(f);
                var match = Regex.Match(text, regexPattern);

                if (match.Length > 0)
                {
                    text = text.Replace(match.ToString(), "");
                    File.WriteAllText(f, text);
                    Console.WriteLine("Replaced text in " + f);
                }                
            }

            Console.WriteLine("Finished");

        }

        public static void GetDirectories(string rootDirectory)
        {
            var filterDirectory = Directory.GetDirectories(rootDirectory);
            foreach (var item in filterDirectory)
            {
                var folderName = new DirectoryInfo(item).Name;

                if (!foldersToIgnore.Contains(folderName))
                {
                    directoriesToScan.Add(item.ToString());
                }
                GetDirectories(item.ToString());
            }
        }

        public static void GetFiles()
        {
            foreach (var d in directoriesToScan)
            {
                var files = Directory.GetFiles(d);

                foreach (var file in files)
                {
                    FileInfo fi = new FileInfo(file);

                    if (!filesToIgnore.Contains(fi.Name) && !fileExtensionsToIgnore.Contains(fi.Extension))
                    {
                        filesToScan.Add(file);
                    }
                }

            }
        }


    }
}
