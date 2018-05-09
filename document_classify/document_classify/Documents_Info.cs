using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions; //noktalama işaretlerini temizlemek için kullanıldı

namespace document_classify
{
    class DocumentsInfo
    {
        private FileInfo[] files;
        private string categori;
        private Dictionary<string, int> frequencyOfWords = new Dictionary<string, int>();// kelime-->sıklığı şekilde tutulacak olan veriyapısı
        private List<string> all_texts = new List<string>();
        public FileInfo[] Files { get => files; set => files = value; }
        public string Categori { get => categori; set => categori = value; }
        public Dictionary<string, int> FrequencyOfWords { get => frequencyOfWords; set => frequencyOfWords = value; }
        public List<string> All_texts { get => all_texts; set => all_texts = value; }
        public void InitFiles()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var file = File.ReadAllText(files[i].FullName, Encoding.GetEncoding("ISO-8859-9"));
                var replaceBrackets = Regex.Replace(file,@"\((?'content'[^)]+)\)", match => $", {match.Groups["content"].Value}");// \r\n  gibi karakterleri temizlemek için
                var replacePunctuation = Regex.Replace(replaceBrackets,@"[^\w,]+", " ");// \u karakterini temizlemek için
                file = replacePunctuation;
                file = Regex.Replace(file, @"\p{P}", "");//Noktalama işaretlerini temizleme
                file=file.Replace(" ", "_");
                All_texts.Add(file.ToLower());//TÜm karakterleri küçük harfe çevirme
            }
        }
        public List<string> clear_the_stop_words(List<string> words)
        {


            return words;
        }
    }
}
