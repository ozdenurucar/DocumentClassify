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
        public Dictionary<string, Dictionary<string, double>> Mean { get;  } = new Dictionary<string, Dictionary<string, double>>();
        public Dictionary<string, Dictionary<string, double>> Variance { get; } = new Dictionary<string, Dictionary<string, double>>();
        public Dictionary<string,double> CategoriProb { get; set; } = new Dictionary<string,double>();
        public Dictionary<string, double> CategoriCount { get; set; } = new Dictionary<string, double>(5);

        public NaiveBayes(List<News> trainingSet,List<News> testSet)
        {
            TrainingSet = trainingSet;
            TestSet = testSet;
            PrepareTrainingSet();
            SetCategoriProbability();
            SetTrainingData();
        }

        void PrepareTrainingSet()
        {
            Dictionary<string, double> Frequencies3 = new Dictionary<string, double>();
            Dictionary<string, double> Frequencies2 = new Dictionary<string, double>();
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
                if (!Mean.ContainsKey(file.Categori))
                    Mean.Add(file.Categori, file.Gram);
                else
                    foreach(var iter in file.Gram)
                    {
                        if (!Mean[file.Categori].ContainsKey(iter.Key))
                            Mean[file.Categori].Add(iter.Key, iter.Value);
                        else
                            Mean[file.Categori][iter.Key] += iter.Value;
                    }
            }
            foreach(var cat in Mean.ToList())
            {
                foreach(var gram in cat.Value.ToList())
                {
                    Mean[cat.Key][gram.Key] = (gram.Value * 1.0) / CategoriCount[cat.Key];
                }
            }
            foreach (var cat in Mean)
            {
                if (!Variance.ContainsKey(cat.Key))
                    Variance.Add(cat.Key, new Dictionary<string, double>());
                foreach (var gram in cat.Value)
                {
                    double x = 0.0;
                    foreach (var file in TrainingSet)
                    {
                        if (file.Categori == cat.Key)
                            if (file.Gram.ContainsKey(gram.Key))
                                x += Math.Pow(file.Gram[gram.Key] - Mean[cat.Key][gram.Key], 2);
                    }
                    x /= (TrainingSet.Count - 1);
                    if (!Variance[cat.Key].ContainsKey(gram.Key))
                        Variance[cat.Key].Add(gram.Key, x);

                }
            }

        }

        void SetCategoriProbability()
        {

            CategoriCount.Add("ekonomi", 0);
            CategoriCount.Add("magazin", 0);
            CategoriCount.Add("saglik", 0);
            CategoriCount.Add("siyasi", 0);
            CategoriCount.Add("spor", 0);
            foreach (var file in TrainingSet)
            {
                CategoriCount[file.Categori]++;
            }
            CategoriProb.Add("ekonomi", 0.0);
            CategoriProb.Add("magazin", 0.0);
            CategoriProb.Add("saglik", 0.0);
            CategoriProb.Add("siyasi", 0.0);
            CategoriProb.Add("spor", 0.0);
            foreach(var iter in CategoriCount.ToList())
            {
                CategoriProb[iter.Key] = (CategoriCount[iter.Key] * 1.0) / (TrainingSet.Count * 1.0);
            }

        }
        public string Deduce(News file) //18 sayfada
        {
            string[] categories = { "ekonomi", "magazin", "saglik", "siyasi", "spor" };
            SortedList<double, string> probs = new SortedList<double, string>(5);
            foreach (var cat in categories)
            {
                double result = 0.0;
                foreach (var gram in Mean[cat].ToList())
                {
                    double x;
                    if(file.Gram.ContainsKey(gram.Key))
                        x = Math.Log((1 / (Math.Sqrt(2 * Math.PI * Variance[cat][gram.Key]))) * Math.Pow(Math.E, -1 * (Math.Pow(file.Gram[gram.Key] - Mean[cat][gram.Key], 2) / (2 * Variance[cat][gram.Key])))); //18. sayfa denklemi
                    else
                        x = Math.Log((1 / (Math.Sqrt(2 * Math.PI * Variance[cat][gram.Key]))) * Math.Pow(Math.E, -1 * (Math.Pow(0 - Mean[cat][gram.Key], 2) / (2 * Variance[cat][gram.Key]))));
                    if (!Double.IsNaN(x)&&!Double.IsInfinity(x))
                        result += x;
                }
                probs.Add(result,cat);
            }

            var xx = probs.Last();
            return xx.Value;
        }

    }

}
