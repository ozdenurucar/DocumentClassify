using System.Collections.Generic;
using System.IO;

namespace document_classify
{
    class DocumentsInfo
    {
        public FileInfo[] files;
        public string Categori { get; set; }
        public List<NewsFile> AllTexts { get; set; } = new List<NewsFile>();


        public void InitFiles()
        {
            foreach(var iter in files)
            {
                AllTexts.Add(new NewsFile(iter, Categori));
            }
        }
    }
}