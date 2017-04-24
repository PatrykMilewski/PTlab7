using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFiles
{
    class ListElement
    {
        public bool isDirectory { get; set; }

        public string text { get; set; }

        public ListElement(string text, bool isDirectory)
        {
            this.text = text;
            this.isDirectory = isDirectory;
        }

        public string printElement(int deep)
        {
            string output = "";
            for (int i = 0; i < deep; i++)
                output += '\t';
            output += text;
            return output;
        }
    }
}
