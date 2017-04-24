using System.Collections.Generic;

namespace ListFiles
{
    class NameCompare : IComparer<Directory>
    {
        public int Compare(Directory first, Directory second)
        {
            if (first.name.Length != second.name.Length)
                return first.name.Length - second.name.Length;

            else
                return first.name.CompareTo(second.name);
        }
    }
}
