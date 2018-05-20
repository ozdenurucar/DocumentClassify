using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Document_Classify
{
    class NaiveBayes
    {
        public class Train
        {
            public List<News> TrainingSet { get; set; }
            public Dictionary<string, double> CategoriCount { get; set; } = new Dictionary<string, double>(5);
            public Dictionary<string, Dictionary<string, List<double>>> MeanAndVariance { get; } = new Dictionary<string, Dictionary<string, List<double>>>();
            public Train(List<News> trainingSet)
            {
                TrainingSet = trainingSet;
                foreach (var file in TrainingSet)
                {
                    if (!CategoriCount.ContainsKey(file.Categori))
                        CategoriCount.Add(file.Categori, 1);
                    else
                        CategoriCount[file.Categori]++;
                }
                PrepareTrainingSet();
                SetVarianceAndMean();
            }
            void PrepareTrainingSet()
            {
                Dictionary<string, double> Frequencies = new Dictionary<string, double>();
                foreach (var document in TrainingSet)
                {
                    foreach (var gram in document.Gram)
                    {
                        if (!Frequencies.ContainsKey(gram.Key))
                            Frequencies.Add(gram.Key, gram.Value);
                        else
                            Frequencies[gram.Key] += gram.Value;
                    }
                }
                foreach (var iter in Frequencies.Where(kv => kv.Value < 50).ToList())
                {
<<<<<<< HEAD
                    Frequencies.Remove(iter.Key);
=======
                    Dictionary<string, double> Frequencies = new Dictionary<string, double>();
                    foreach (var document in TrainingSet)
                    {
                        foreach (var gram in document.Gram)
                        {
                            if (!Frequencies.ContainsKey(gram.Key))
                                Frequencies.Add(gram.Key, gram.Value);
                            else
                                Frequencies[gram.Key] += gram.Value;
                        }
                    }
                    foreach (var iter in Frequencies.Where(kv => kv.Value < 50).ToList())
                    {
                        Frequencies.Remove(iter.Key);
                    }
                    Parallel.ForEach(TrainingSet, file =>
                    {
                        file.Gram = file.Gram.Keys.Intersect(Frequencies.Keys).ToDictionary(t => t, t => file.Gram[t]);
                    });
                }
                Parallel.ForEach(TrainingSet, file =>
                {
                    file.Gram = file.Gram.Keys.Intersect(Frequencies.Keys).ToDictionary(t => t, t => file.Gram[t]);
                });


            }
            void SetVarianceAndMean()
            {
                foreach (var file in TrainingSet)
                {
                    if (!MeanAndVariance.ContainsKey(file.Categori))
                    {
                        MeanAndVariance.Add(file.Categori, new Dictionary<string, List<double>>());
                        foreach (var iter in file.Gram)
                        {
                            MeanAndVariance[file.Categori].Add(iter.Key, new List<double>(2) { iter.Value });
                        }
                    }
                    else
                    {
                        foreach (var iter in file.Gram)
                        {
                            if (!MeanAndVariance[file.Categori].ContainsKey(iter.Key))
                                MeanAndVariance[file.Categori].Add(iter.Key, new List<double>(2) { iter.Value });

                            else
                                MeanAndVariance[file.Categori][iter.Key][0] += iter.Value;
                        }
                    }
                }
                foreach (var cat in MeanAndVariance.ToList())
                {
                    foreach (var gram in cat.Value.ToList())
                    {
                        MeanAndVariance[cat.Key][gram.Key][0] = (gram.Value[0] * 1.0) / CategoriCount[cat.Key];
                    }
                }
                foreach (var cat in MeanAndVariance)
                {
                    foreach (var gram in cat.Value)
                    {
                        double x = 0.0;
                        foreach (var file in TrainingSet)
                        {
                            if (file.Categori == cat.Key)
                                if (file.Gram.ContainsKey(gram.Key))
                                    x += Math.Pow(file.Gram[gram.Key] - MeanAndVariance[cat.Key][gram.Key][0], 2);
                        }
                        x /= (TrainingSet.Count - 1);
                        MeanAndVariance[cat.Key][gram.Key].Add(x);

                    }
                }

            }
        }
        public class Test
        {
            private Train Train;
            private readonly string[] Categories;
            public List<Tuple<string, string>> Results { get; } = new List<Tuple<string, string>>();

            public Test(Train train, string[] categories)
            {
                Train = train;
                Categories = categories;
            }

            public string Predict(News file)
            {
                SortedList<double, string> probs = new SortedList<double, string>(5);
                foreach (var cat in Categories)
                {
                    double result = 0.0;
                    foreach (var gram in Train.MeanAndVariance[cat].ToList())
                    {
                        double x;
                        if (file.Gram.ContainsKey(gram.Key))
                            x = Math.Log((1 / (Math.Sqrt(2 * Math.PI * Train.MeanAndVariance[cat][gram.Key][1]))) * Math.Pow(Math.E, -1 * (Math.Pow(file.Gram[gram.Key] - Train.MeanAndVariance[cat][gram.Key][0], 2) / (2 * Train.MeanAndVariance[cat][gram.Key][1])))); //18. sayfa denklemi
                        else
                            x = Math.Log((1 / (Math.Sqrt(2 * Math.PI * Train.MeanAndVariance[cat][gram.Key][1]))) * Math.Pow(Math.E, -1 * (Math.Pow(0 - Train.MeanAndVariance[cat][gram.Key][0], 2) / (2 * Train.MeanAndVariance[cat][gram.Key][1]))));
                        if (!Double.IsNaN(x) && !Double.IsInfinity(x))
                            result += x;
                    }
                    probs.Add(result, cat);
                }

                var xx = probs.Last();
                return xx.Value;
            }
            public void PredictAll(List<News> files)
            {
                foreach (var file in files)
                {
                    string res = Predict(file);
                    Results.Add(new Tuple<string, string>(file.Categori, res));
                }
            }
            public Dictionary<string, Tuple<double, double, double>> PrecisionRecallAndFMeasure(List<Tuple<string, string>> predictions)
            {
                var result = new Dictionary<string, Tuple<double, double, double>>();
                foreach (var cat in Categories)
                {
                    double TP = 0, FP = 0, FN = 0, TN = 0;
                    foreach (var prediction in predictions)
                    {
                        if (prediction.Item1 == cat && prediction.Item2 == cat)
                            TP++;
                        else if (prediction.Item1 == cat && prediction.Item2 != cat)
                            FN++;
                        else if (prediction.Item1 != cat && prediction.Item2 == cat)
                            FP++;
                        else
                            TN++;
                    }
                    double precision = TP / (TP + FN);
                    double recall = TP / (TP + FP);
                    double fMeasure = (2 * precision * recall) / (precision + recall);
                    result.Add(cat, new Tuple<double, double, double>(precision, recall, fMeasure));
                }
                return result;
            }
            public Dictionary<string, Tuple<double, double, double>> PrecisionRecallAndFMeasureForResult()
            {
                return PrecisionRecallAndFMeasure(Results);
            }
        }

    }
}
