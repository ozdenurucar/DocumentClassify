using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace document_classify
{
    class DocumentsInfo
    {
        private FileInfo[] files;
        private string categori;
        private  Dictionary<string, int> frequencyOfWords = new Dictionary<string, int>();// kelime-->sıklığı şekilde tutulacak olan veriyapısı

        public FileInfo[] Files { get => files; set => files = value; }
        public string Categori { get => categori; set => categori = value; }
        public Dictionary<string, int> FrequencyOfWords { get => frequencyOfWords; set => frequencyOfWords = value; }


        public void InitFiles()
        {
            var file = File.ReadAllText(files[0].FullName);
            Console.WriteLine(file);
        }
    }
}
