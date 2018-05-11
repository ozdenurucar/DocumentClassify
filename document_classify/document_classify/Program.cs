using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace document_classify
{
    class Program
    {
        private static readonly string dir = @"C:\Users\alper\Downloads\1150haber\raw_texts\";
        private static List<DocumentsInfo> documents = new List<DocumentsInfo>(); // 5 kategori için dokumanların tutulacağı dokuman lisatesi
        private static readonly string[] allCategories = { "ekonomi", "magazin", "saglik", "siyasi", "spor" };
        public static void ReadDirectory()
        {
            foreach (var categori in allCategories)
            {
                DirectoryInfo directory = new DirectoryInfo(dir + categori);
                documents.Add( new DocumentsInfo { files = directory.GetFiles("*.txt"),
                                                   Categori = categori
                                                 });
            }
        }
        static void Main(string[] args)
        {
            ReadDirectory();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach(var document in documents)
            {
                document.InitFiles();
            }
            sw.Stop();
            string runningTime = sw.Elapsed.ToString();
            Console.Write("Finished   "+runningTime);
            Console.ReadKey();
        }
    }
}