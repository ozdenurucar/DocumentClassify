﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Nuve.Lang;
using Nuve.Morphologic.Structure;

namespace document_classify
{

    class TurkishStopWords
    {
        static readonly string stopWordsPath = @"C:\Users\alper\Downloads\1150haber\stop_words.txt";

        public string[] StopWords { get; } = File.ReadAllLines(stopWordsPath, Encoding.UTF8);
    }
   class NewsFile
   {
        public string Categori { get; set; }
        public Dictionary<string, int> Frequencies { get; set; }

        private TurkishStopWords stopWords = new TurkishStopWords();

        private string text;
        readonly Language tr = LanguageFactory.Create(LanguageType.Turkish);
        


        public NewsFile(FileInfo file, string categori)
        {
            Categori = categori;
            text = File.ReadAllText(file.FullName, Encoding.GetEncoding("ISO-8859-9"));
            text = Regex.Replace(text, @"\((?'content'[^)]+)\)", match => $", {match.Groups["content"].Value}");
            text = Regex.Replace(text, @"[^\w,]+", " ");
            text = Regex.Replace(text, @"\p{P}", "");
            text = text.ToLower();
            text = ClearTheStopWords(text);
            text = DeleteSuffixes(text);
        }
        public string ClearTheStopWords(string text)
        {
            string[] words = text.Split(' ');
            
            foreach (var stopword in stopWords.StopWords)
            {
                text.Replace(stopword, "");
            }
            return text;
        }
        public string DeleteSuffixes(string text)
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
    }
}
