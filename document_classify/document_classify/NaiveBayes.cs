using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentClassify
{

    class NaiveBayes
    {
        public List<News> TrainingSet { get; set; }
        public List<News> TestSet { get; set; }
        public Dictionary<string,int> Numbers { get; set; }
        public Dictionary<string, Dictionary<string, int>> TrainingData { get; set; } = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, double>> Probabilities { get; set; } = new Dictionary<string, Dictionary<string, double>>();
        
        public NaiveBayes(List<News> trainingSet,List<News> testSet)
        {
            TrainingSet = trainingSet;
            TestSet = testSet;
            PrepareTrainingSet();
            SetTrainingData();
        }

        void PrepareTrainingSet()
        {
            Dictionary<string, int> Frequencies3 = new Dictionary<string, int>();
            Dictionary<string, int> Frequencies2 = new Dictionary<string, int>();
            foreach (var document in TrainingSet)
            {
                foreach (var gram in document.Gram2)
                {
                    if (!Frequencies2.ContainsKey(gram.Key))
                        Frequencies2.Add(gram.Key, gram.Value);
                    else
                        Frequencies2[gram.Key] += gram.Value;
                }

                foreach (var gram in document.Gram3)
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
                 file.Gram2 = file.Gram2.Keys.Intersect(Frequencies2.Keys).ToDictionary(t => t, t => file.Gram2[t]);
                 file.Gram3 = file.Gram3.Keys.Intersect(Frequencies3.Keys).ToDictionary(t => t, t => file.Gram3[t]);
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
