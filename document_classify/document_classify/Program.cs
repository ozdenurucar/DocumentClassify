using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DocumentClassify
{
    static class Program
    {
        private static readonly string dir = @"C:\Users\alper\Downloads\1150haber\raw_texts\";
        //private static readonly string dir = @"C:\Users\ozden\Desktop\Yazlab  II\Dokuman Siniflandirma\1150haber\raw_texts\";
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

        public static List<List<News>> ShuffleAndSplit(this List<News> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return new List<List<News>> { list.GetRange(0,list.Count* 3/4), list.GetRange(list.Count*3/4, list.Count-list.Count*3/4) };
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
            var lists = ShuffleAndSplit(documents);
            NaiveBayes bayes = new NaiveBayes (lists[0], lists[1]);
            int x = 0;
            foreach(var file in lists[1])
            {
                string xx = bayes.Deduce(file);
                Console.WriteLine(file.Categori+":  "+ xx);
                if (file.Categori == xx)
                    x++;
            }
            double result = (x * 1.0 / lists[1].Count) * 100;
            Console.WriteLine("%"+result);
            
            
            Console.WriteLine("----------------------");
            Console.ReadKey();
        }
    }
}