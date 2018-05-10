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
        private FileInfo[] files;
        private string categori;
        private Dictionary<string, int> frequencyOfWords = new Dictionary<string, int>();// kelime-->sıklığı şekilde tutulacak olan veriyapısı
        private List<string> all_texts = new List<string>();
        private List<string> stop_words = new List<string>();
        public FileInfo[] Files { get => files; set => files = value; }
        public string Categori { get => categori; set => categori = value; }
        public Dictionary<string, int> FrequencyOfWords { get => frequencyOfWords; set => frequencyOfWords = value; }
        public List<string> All_texts { get => all_texts; set => all_texts = value; }
        public List<string> Stop_words { get => stop_words; set => stop_words = value; }

        public void InitFiles()
        {
            for (int i = 0; i < files.Length; i++)
            {
                var file = File.ReadAllText(files[i].FullName, Encoding.GetEncoding("ISO-8859-9"));
                var replaceBrackets = Regex.Replace(file,@"\((?'content'[^)]+)\)", match => $", {match.Groups["content"].Value}");// \r\n  gibi karakterleri temizlemek için
                var replacePunctuation = Regex.Replace(replaceBrackets,@"[^\w,]+", " ");// \u karakterini temizlemek için
                file = replacePunctuation;
                file = Regex.Replace(file, @"\p{P}", "");//Noktalama işaretlerini temizleme
                All_texts.Add(file.ToLower());//TÜm karakterleri küçük harfe çevirme
                All_texts[i]=clear_the_stop_words(All_texts[i]);//stop wrodleri temizle
                All_texts[i] = All_texts[i].Replace(" ", "_");// boşluğu "_" yap
            }
        }
        public string clear_the_stop_words(string words)
        {
            //Stop wordler temizlenirken stop_words_txt doyası okunarak içerisindeki stop wordler alınır. Daha sonra her ham txt doyası için
            //stop wordleriniçerisinde var mı diye bakılır. Eğer varsa yeni oluşturulan string new_words stringine eklenmez.
            string[] words_array = words.Split(' ');
            List<string> list = new List<string>();//Kelime sayısındaki değişimi görmek için ekledim
            string new_words="";
            List<string> kokler = new List<string>();
            var stop_words = File.ReadAllLines("stop_words.txt", Encoding.GetEncoding("ISO-8859-9"));
            for (int i = 0; i < words_array.Length; i++)
            {
                if(!stop_words.Contains(words_array[i]))
                {
                    new_words+=words_array[i]+" ";
                    list.Add(words_array[i]);
                }
            }
            kokler = find_words_root(list);
            return new_words;
        }

        public List<string> find_words_root(List<string> words)
        {
            List<string> word_roots = new List<string>();
            Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
            for (int i = 0; i < words.Count(); i++)
            {
                if(zemberek.kelimeDenetle(words[i]))//Kelimeyi denetliyor türkçe olup olmamasına göre
                {
                    word_roots.Add(zemberek.kelimeCozumle(words[i])[0].kok().icerik());// kelimenin kök bilgisini veriyor
                }
            }
            return word_roots;
        }
    }
}
