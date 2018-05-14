using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace document_classify
{
    class Program
    {
        private static readonly string dir = @"C:\Users\alper\Downloads\1150haber\raw_texts\";
        private static List<News> documents = new List<News>();
        private static Dictionary<string, int> frequencies2 = new Dictionary<string, int>();
        private static Dictionary<string, int> frequencies3 = new Dictionary<string, int>();
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
            foreach (var document in documents)
            {
                foreach (var gram in document.gram2)
                {
                    if (!frequencies2.ContainsKey(gram.Key))
                        frequencies2.Add(gram.Key, gram.Value);
                    else
                        frequencies2[gram.Key] += gram.Value;
                }

                foreach (var gram in document.gram3)
                {
                    if (!frequencies3.ContainsKey(gram.Key))
                        frequencies3.Add(gram.Key, gram.Value);
                    else
                        frequencies3[gram.Key] += gram.Value;
                }
            }
            Thread x = new Thread(() =>
            {
                foreach (var iter in frequencies2.Where(kv => kv.Value < 50).ToList())
                {
                    frequencies2.Remove(iter.Key);
                }
            });
            x.Start();
            Thread y = new Thread(() =>
            {
                foreach (var iter in frequencies3.Where(kv => kv.Value < 50).ToList())
                {
                    frequencies2.Remove(iter.Key);
                }
            });
            y.Start();
            x.Join();
            y.Join();
            Console.WriteLine("----------------------");
            Console.ReadKey();
        }
    }
}