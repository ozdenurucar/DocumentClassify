using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace document_classify
{
    class Program
    {

        static List<DocumentsInfo> documents = new List<DocumentsInfo>(); // 5 kategori için dokumanların tutulacağı dokuman lisatesi
        static string[] allCategories = { "ekonomi", "magazin", "saglik", "siyasi", "spor" };
        static string filePath = @"C:\Users\alper\Downloads\1150haber\raw_texts\";
        public static void ReadDirectory()
        {
            for (int i = 0; i < allCategories.Length; i++)
            {
                string file_path_2 = filePath + allCategories[i];
                DirectoryInfo directory = new DirectoryInfo(file_path_2);
                documents.Add( new DocumentsInfo { files = directory.GetFiles("*.txt"),
                                                   Categori = allCategories[i]
                                                 });
            }
        }
        static void Main(string[] args)
        {
            ReadDirectory();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < documents.Count(); i++)
            {
                documents[i].InitFiles();
            }
            sw.Stop();
            string runningTime = sw.Elapsed.ToString();
            Console.Write("Finished   "+runningTime);
            Console.ReadKey();
        }
    }
}
