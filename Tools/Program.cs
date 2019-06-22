using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Tools
    {
        public static string folderRoot = @"C:\Users\chris\Documents\Test";
        public static List<string> foldersToIgnore = new List<string> { "Ignore", "Ignore1" };
        public static List<string> filesToIgnore = new List<string> { "ConsoleLogIgnore1.txt" };
        public static List<string> fileExtensionsToIgnore = new List<string> { ".js" };
        public static List<string> directoriesToScan = new List<string>();
        public static List<string> filesToScan = new List<string>();

        public static void Main(string[] args)
        {
            ClearConsoleLogs();
        }

        public static void ClearConsoleLogs()
        {
            // Get directories to scan
            GetDirectories(folderRoot);

            // Get files to scan
            GetFiles();


            foreach (string f in filesToScan)
            {
                var x = "";
            }
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
            // Get all files
            foreach (var d in directoriesToScan)
            {
                var files = Directory.GetFiles(d);

                foreach (var file in files)
                {
                    FileInfo fi  = new FileInfo(file);

                    if(!filesToIgnore.Contains(fi.Name) && !fileExtensionsToIgnore.Contains(fi.Extension))
                    {
                        filesToScan.Add(file);
                    }
                }
               
            }
        }


    }

}

