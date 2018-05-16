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
        private static Dictionary<string, List<News>> News = new Dictionary<string, List<News>>();
        public static void ReadDirectory()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Openning Files...");
            sw.Start();
            foreach (var categori in allCategories)
            {
                
                string[] files = Directory.GetFiles(dir + categori);
                News.Add(categori, new List<News>());
                foreach(var file in files)
                {
                    documents.Add(new News(file, categori));
                    News[categori].Add(documents[documents.Count - 1]);
                }
            }
            sw.Stop();
            Console.WriteLine("elapsed time: " + sw.Elapsed.ToString());
        }

        public static List<List<News>> ShuffleAndSplit(this Dictionary<string,List<News>> dict)
        {
            List<News> trainingData = new List<News>();
            List<News> testData = new List<News>();
            Random rng = new Random();
            foreach(var categori in dict.Keys)
            {
                int n = dict[categori].Count;
                while(n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    var value = dict[categori][k];
                    dict[categori][k] = dict[categori][n];
                    dict[categori][n] = value;
                }
                trainingData.AddRange(dict[categori].GetRange(0,dict[categori].Count * 3/4));
                testData.AddRange(dict[categori].GetRange(dict[categori].Count * 3 / 4, dict[categori].Count - dict[categori].Count * 3 / 4));
            }

            return new List<List<News>> { trainingData,testData };
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
            var lists = ShuffleAndSplit(News);
            NaiveBayes bayes = new NaiveBayes (lists[0]);
            int x = 0;
            List<Tuple<string, string>> predictions = new List<Tuple<string, string>>();
            foreach(var file in lists[1])
            {
                string xx = bayes.Deduce(file);
                Console.WriteLine(file.Categori+":  "+ xx);
                if (file.Categori == xx)
                    x++;
                predictions.Add(new Tuple<string,string>(file.Categori, xx));
            }
            double result = (x * 1.0 / lists[1].Count) * 100;
            Console.WriteLine("%"+result);
            bayes.PrecisionRecallAndFMeasure(predictions);
            
            Console.WriteLine("----------------------");
            Console.ReadKey();
        }
    }
}