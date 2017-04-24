using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFiles
{
    class NameCompare : IComparer<Directory>
    {
        public int Compare(Directory first, Directory second)
        {
            if (first.text.Length != second.text.Length)
                return first.text.Length - second.text.Length;

            else
                return first.text.CompareTo(second.text);
        }
    }
}
