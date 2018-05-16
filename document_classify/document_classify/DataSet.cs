using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DocumentClassify
{
    class DataSet
    {
        public string[] Categories { get; set; } = { "ekonomi", "magazin", "saglik", "siyasi", "spor" };
        public string Path { get; set; }
        internal List<News> TrainingData { get; set; } = new List<News>();
        internal List<News> TestData { get; set; } = new List<News>();

        public DataSet(string path)
        {
            Dictionary<string, List<News>> News = new Dictionary<string, List<News>>();
            Path = path;
            foreach (var categori in Categories)
            {
                string[] files = Directory.GetFiles(path + categori);
                News.Add(categori, new List<News>());
                foreach (var file in files)
                {
                    News[categori].Add(new News(file, categori));
                }
            }
            ShuffleAndSplit(News);
            PrepareFiles();
        }


        public void ShuffleAndSplit(Dictionary<string, List<News>> dict)
        {

            Random rng = new Random();
            foreach (var categori in dict.Keys)
            {
                int n = dict[categori].Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    var value = dict[categori][k];
                    dict[categori][k] = dict[categori][n];
                    dict[categori][n] = value;
                }
                TrainingData.AddRange(dict[categori].GetRange(0, dict[categori].Count * 3 / 4));
                TestData.AddRange(dict[categori].GetRange(dict[categori].Count * 3 / 4, dict[categori].Count - dict[categori].Count * 3 / 4));
            }
        }

        void PrepareFiles()
        {
            Parallel.ForEach(TrainingData, document =>
            {
                document.Prepare();
            });
            Parallel.ForEach(TestData, document =>
            {
                document.Prepare();
            });
        }


    }
}
