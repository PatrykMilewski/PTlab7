using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFiles
{
    class NameCompare : IComparer<ListElement>
    {
        public int Compare(ListElement first, ListElement second)
        {
            if (first.deep == second.deep)
            {
                if (first.text.Length != second.text.Length)
                return first.text.Length - second.text.Length;

            else
                return first.text.CompareTo(second);
            }
            else
            {
                return first.deep - second.deep;
            }
        }
    }
}
