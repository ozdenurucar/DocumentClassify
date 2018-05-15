using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Nuve.Lang;
using Nuve.Morphologic.Structure;

namespace DocumentClassify
{

    class TurkishStopWords
    {
        static readonly string stopWordsPath = @"C:\Users\alper\Downloads\1150haber\stop_words.txt";
        //static readonly string stopWordsPath = @"C:\Users\ozden\Desktop\Yazlab  II\Dokuman Siniflandirma\1150haber\stop_words.txt";
        

        public string[] StopWords { get; } = File.ReadAllLines(stopWordsPath, Encoding.UTF8);
    }
   class News
   {
        public string Categori { get; set; }
        public Dictionary<string, int> Gram2 { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Gram3 { get; set; } = new Dictionary<string, int>();

        private TurkishStopWords stopWords = new TurkishStopWords();

        private string text;
        readonly Language tr = LanguageFactory.Create(LanguageType.Turkish);
        


        public News(string file, string categori)
        {
            Categori = categori;
            text = File.ReadAllText(file, Encoding.GetEncoding("ISO-8859-9"));
        }

        public void Prepare()
        {
            text = Regex.Replace(text, @"\((?'content'[^)]+)\)", match => $", {match.Groups["content"].Value}");
            text = Regex.Replace(text, @"[^\w,]+", " ");
            text = Regex.Replace(text, @"\p{P}", "");
            text = text.ToLower();
            text = ClearTheStopWords(text);
            text = DeleteSuffixes(text);
            SetGram2();
            SetGram3();
        }
        private string ClearTheStopWords(string text)
        {
            string[] words = text.Split(' ');
            
            foreach (var stopword in stopWords.StopWords)
            {
                text.Replace(stopword, "");
            }
            return text;
        }
        private string DeleteSuffixes(string text)
        {
            var words = text.Split(' ');
            string result = "";
            foreach (var word in words)
            {
                IList<Word> solutions = tr.Analyze(word);
                if (solutions.Any())
                    result += solutions[0].GetStem().GetSurface() + "_";
            }
            return result;
        }

        private void SetGram2()
        {
            for(int i = 0; i < text.Length-2; ++i)
            {

                if (!Gram2.ContainsKey(text.Substring(i, 2)))
                    Gram2.Add(text.Substring(i, 2), 1);
                else
                    Gram2[text.Substring(i, 2)]++;
            }
        }
        private void SetGram3()
        {
            for (int i = 0; i < text.Length - 3; ++i)
            {
                if (!Gram3.ContainsKey(text.Substring(i, 3)))
                    Gram3.Add(text.Substring(i, 3), 1);

                else 
                    Gram3[text.Substring(i, 3)]++;
            }
        }

        public Dictionary<string,int> GetGrams()
        {
            Dictionary<string, int> gram = this.Gram2;
            gram.Concat(Gram3.ToList());
            return gram;
        }

        public void print()
        {
            foreach(var key in Gram2)
            {
                if (key.Value >= 50)
                    System.Console.Write(key+" ");
            }
            foreach (var key in Gram3)
            {
                if(key.Value >= 50)
                {
                    System.Console.Write(key + " ");
                }
            }
        }
    }
}
