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
        public static List<string> directoriesToIgnore = new List<string> { "Ignore", "Ignore1" };
        public static List<string> filesToIgnore = new List<string> { "ConsoleLogIgnore1.txt" };
        public static List<string> fileExtensionsToIgnore = new List<string> { ".js" };
        public static List<string> directoriesToScan = new List<string>();
        public static List<string> filesToScan = new List<string>();

        public static void ReplaceTextInFile()
        {
            int fileCount = 0;
            int replacementCount = 0;

            PrepareFilesToScan();

            foreach (string f in filesToScan)
            {
                string text = File.ReadAllText(f);
                var matches = Regex.Matches(text.ToLower(), regexPattern);
                foreach (var m in matches)
                {
                    text = text.ToLower().Replace(m.ToString(), "");
                    File.WriteAllText(f, text);
                    Console.WriteLine("Replaced text in " + f);
                    replacementCount += 1;
                }

                fileCount += 1;
            }

            Console.WriteLine("Replaced " + replacementCount + " occurences after scanning " + fileCount + " files");

        }

        public static void PrepareFilesToScan()
        {
            // Get directories to scan
            GetDirectories(folderRoot);

            // Get files to scan
            GetFiles();
        }

        public static void GetDirectories(string rootDirectory)
        {
            var directories = Directory.GetDirectories(rootDirectory);
            foreach (var item in directories)
            {
                var directoryName = new DirectoryInfo(item).Name;

                if (!directoriesToIgnore.Contains(directoryName))
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
