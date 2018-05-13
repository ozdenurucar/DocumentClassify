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
        private static List<News> documents = new List<News>();
        private static readonly string[] allCategories = { "ekonomi", "magazin", "saglik", "siyasi", "spor" };
        public static void ReadDirectory()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Openning Files...");
            sw.Start();
            foreach (var categori in allCategories)
            {
                
                string[] files = Directory.GetFiles(dir + categori);
                foreach(var file in files)
                {
                    documents.Add(new News(file, categori));
                }
            }
            sw.Stop();
            Console.WriteLine("elapsed time: " + sw.Elapsed.ToString());
        }
        static void Main(string[] args)
        {
            ReadDirectory();
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Preparing Files...");
            sw.Start();
            Parallel.ForEach(documents, document =>
            {
                document.Prepare();
            });
            sw.Stop();
            Console.WriteLine("elapsed time: " + sw.Elapsed.ToString());
            Console.ReadKey();
        }
    }
}