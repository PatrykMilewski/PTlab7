using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFiles
{
    class ListElement
    {
        public int deep { get; set; }
        public string text { get; set; }

        public ListElement(int deep, string text)
        {
            this.deep = deep;
            this.text = text;
        }

        public string printElement()
        {
            string output = "";
            for (int i = 0; i < deep; i++)
                output += '\t';
            output += text;
            return output;
        }
    }
}
