using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Text.RegularExpressions; //noktalama işaretlerini temizlemek için kullanıldı
using net.zemberek.erisim;
using net.zemberek.yapi;
using net.zemberek.tr.yapi;
using net.zemberek.bilgi;


namespace document_classify
{
    class DocumentsInfo
    {
        public FileInfo[] files;
        public string Categori { get; set; }
        public Dictionary<string, int> FrequencyOfWords { get; set; } = new Dictionary<string, int>();
        public List<string> AllTexts { get; set; } = new List<string>();
        public List<string> StopWords { get; set; } = new List<string>();

        Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());

        public void InitFiles()
        {
            foreach(var iter in files)
            {
                var file = File.ReadAllText(iter.FullName, Encoding.GetEncoding("ISO-8859-9"));
                var replaceBrackets = Regex.Replace(file, @"\((?'content'[^)]+)\)", match => $", {match.Groups["content"].Value}");
                var replacePunctuation = Regex.Replace(replaceBrackets, @"[^\w,]+", " ");// \u karakterini temizlemek için
                file = replacePunctuation;
                file = Regex.Replace(file, @"\p{P}", "");//Noktalama işaretlerini temizleme
                file = file.ToLower().Replace(" ", "_");
                file = ClearTheStopWords(file);
                file = deleteSuffixes(file);
                AllTexts.Add(file);//Tüm karakterleri küçük harfe çevirme

            }
        }
        public string ClearTheStopWords(string text)
        {
            //Stop wordler temizlenirken stop_words_txt doyası okunarak içerisindeki stop wordler alınır. Daha sonra her ham txt doyası için
            //stop wordleriniçerisinde var mı diye bakılır. Eğer varsa yeni oluşturulan string new_words stringine eklenmez.
            string[] words = text.Split('_');
            string result = "";
            var stopWords = File.ReadAllLines("stop_words.txt", Encoding.GetEncoding("ISO-8859-9"));
            foreach(var word in words)
            {
                if(!stopWords.Contains(word))
                {
                    result += word + "_";
                }
            }
            Console.WriteLine(result);
            return result;
        }

        public string deleteSuffixes(string text)
        {
            
            return "";
        }
    }
}
