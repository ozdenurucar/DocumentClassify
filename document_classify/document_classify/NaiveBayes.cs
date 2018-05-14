using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentClassify
{

    class NaiveBayes
    {
        public List<News> TrainingSet { get; set; }
        public Dictionary<string,int> Numbers { get; set; }
        public Dictionary<string, Dictionary<string, int>> TrainingData { get; set; } = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, double>> Probabilities { get; set; } = new Dictionary<string, Dictionary<string, double>>();

        void PrepareTrainingSet()
        {
            Dictionary<string, int> Frequencies3 = new Dictionary<string, int>();
            Dictionary<string, int> Frequencies2 = new Dictionary<string, int>();
            foreach (var document in TrainingSet)
            {
                foreach (var gram in document.gram2)
                {
                    if (!Frequencies2.ContainsKey(gram.Key))
                        Frequencies2.Add(gram.Key, gram.Value);
                    else
                        Frequencies2[gram.Key] += gram.Value;
                }

                foreach (var gram in document.gram3)
                {
                    if (!Frequencies3.ContainsKey(gram.Key))
                        Frequencies3.Add(gram.Key, gram.Value);
                    else
                        Frequencies3[gram.Key] += gram.Value;
                }
            }
            Thread x = new Thread(() =>
            {
                foreach (var iter in Frequencies2.Where(kv => kv.Value < 50).ToList())
                {
                    Frequencies2.Remove(iter.Key);
                }
            });
            x.Start();
            foreach (var iter in Frequencies3.Where(kv => kv.Value < 50).ToList())
            {
                Frequencies3.Remove(iter.Key);
            }
            x.Join();

            Parallel.ForEach(TrainingSet, file => 
            {
                file.gram2 = file.gram2.Intersect(Frequencies2) as Dictionary<string, int>;
                file.gram3 = file.gram3.Intersect(Frequencies3) as Dictionary<string, int>;
            });

        }     
        void SetTrainingData()
        {
            foreach(var file in TrainingSet)
            {
                if (!TrainingData.ContainsKey(file.Categori))
                    TrainingData.Add(file.Categori, file.GetGrams());
                else
                    foreach(var iter in file.GetGrams())
                    {
                        if (!TrainingData[file.Categori].ContainsKey(iter.Key))
                            TrainingData[file.Categori].Add(iter.Key, iter.Value);
                        else
                            TrainingData[file.Categori][iter.Key] += iter.Value;
                    }
            }
        }
        void Train()
        {
            
        }

    }

}
