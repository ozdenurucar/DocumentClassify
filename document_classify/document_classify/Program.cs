using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DocumentClassify
{
    class Program
    {
       // static string dir = @"C:\Users\alper\Downloads\1150haber\raw_texts\";
        static string dir = @"C:\Users\ozden\Desktop\Yazlab  II\Dokuman Siniflandirma\1150haber\raw_texts\";
        static void Main(string[] args)
        {
            DataSet dataset = new DataSet(dir);
            NaiveBayes.Train bayes = new NaiveBayes.Train (dataset.TrainingData);
            NaiveBayes.Test test = new NaiveBayes.Test(bayes,dataset.Categories);

            test.PredictAll(dataset.TestData);
            var results = test.Results;
            var values = test.PrecisionRecallAndFMeasureForResult();
        }
    }
}