using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace document_classify
{
    class Program
    {
        private static readonly string dir = @"C:\Users\alper\Downloads\1150haber\raw_texts\";
        private static List<NewsFile> documents = new List<NewsFile>();
        private static readonly string[] allCategories = { "ekonomi", "magazin", "saglik", "siyasi", "spor" };
        public static void ReadDirectory()
        {
            foreach (var categori in allCategories)
            {
                DirectoryInfo directory = new DirectoryInfo(dir + categori);
                FileInfo[] files = directory.GetFiles();
                foreach(var file in files)
                {
                    documents.Add(new NewsFile(file, categori));
                }
            }
            Parallel.ForEach(documents, document =>
            {
                document.Prepare();
            });
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ReadDirectory();
            sw.Stop();
            string runningTime = sw.Elapsed.ToString();
            Console.Write("Finished   "+runningTime);
            Console.ReadKey();
        }
    }
}