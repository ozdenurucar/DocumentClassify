using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace document_classify
{
    class Program
    {

        static List<DocumentsInfo> documents = new List<DocumentsInfo>(); // 5 kategori için dokumanların tutulacağı dokuman lisatesi
        static string[] allCategories = { "ekonomi", "magazin", "saglik", "siyasi", "spor" };
        static string filePath = @"C:\Users\ozden\Desktop\Yazlab  II\Dokuman Siniflandirma\1150haber\raw_texts\";
        public static void ReadDirectory()
        {
            for (int i = 0; i < allCategories.Length; i++)
            {
                string file_path_2 = filePath + allCategories[i];
                DirectoryInfo directory = new DirectoryInfo(file_path_2);
                documents.Add( new DocumentsInfo { Files = directory.GetFiles("*.txt"),
                                                   Categori = allCategories[i]
                                                  });
            }

        }


        static void Main(string[] args)
        {
            ReadDirectory();
            documents[0].InitFiles();
            Console.ReadKey();
        }
    }
}
