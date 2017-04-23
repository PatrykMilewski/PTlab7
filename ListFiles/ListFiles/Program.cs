using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFiles
{
    class Program
    {
        private static bool sort, comparator;
        static void Main(string[] args)
        {
            //if (args[0] == null)
            //{
            //    System.Console.WriteLine("File location is null!");
            //    return;
            //}

            String fileLoc = "C:\\Work";
            sort = true;
            comparator = false;
            List<ListElement> list = new List<ListElement>();

            DirectoryInfo directory = new DirectoryInfo(fileLoc);

            directory.CreateList(ref list);

            list.Sort(new NameCompare());



            System.Console.ReadKey();
        }
    }

    static class DirectoryInfoExtensions
    {
        public static void CreateList(this DirectoryInfo directoryInfo, ref List<ListElement> list, int deep = 0)
        {
            int childrenAmount;
            string consoleOutput;
            foreach (FileSystemInfo systemFile in directoryInfo.GetFileSystemInfos())
            {
                if ((systemFile.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    childrenAmount = ((DirectoryInfo)systemFile).ChildrenAmount();
                    consoleOutput = "";
                    for (int i = 0; i < deep; i++)
                        consoleOutput += '\t';
                    consoleOutput += systemFile.Name + " " + childrenAmount + " " + systemFile.DOSAttributes();
                    list.Add(new ListElement(deep, consoleOutput));
                    ((DirectoryInfo)systemFile).CreateList(ref list, deep + 1);
                }
                else
                {
                    consoleOutput = "";
                    for (int i = 0; i < deep; i++)
                        consoleOutput += '\t';
                    consoleOutput += systemFile.Name + " " + ((FileInfo)systemFile).Length + " " + systemFile.DOSAttributes();
                    list.Add(new ListElement(deep, consoleOutput));
                }
            }
        }

        public static DateTime TheOldestElement(this DirectoryInfo directoryInfo)
        {
            DateTime directoryOldest, theOldestThis = directoryInfo.LastWriteTime;
            foreach (FileSystemInfo systemFile in directoryInfo.GetFileSystemInfos())
            {
                if ((systemFile.Attributes & FileAttributes.Directory) == FileAttributes.Directory)   // is directory
                {
                    directoryOldest = ((DirectoryInfo)systemFile).TheOldestElement();
                    if (directoryOldest < theOldestThis)
                        theOldestThis = directoryOldest;

                }
                else
                {
                    if (systemFile.LastWriteTime < theOldestThis)
                        theOldestThis = systemFile.LastWriteTime;
                }
            }
            return theOldestThis;
        }

        public static int ChildrenAmount(this DirectoryInfo fileSystemInfo)
        {
            int amount = 0;
            foreach (FileSystemInfo systemFile in fileSystemInfo.GetFileSystemInfos())
            {
                if ((systemFile.Attributes & FileAttributes.Directory) == FileAttributes.Directory)   // is directory
                    amount += ((DirectoryInfo)systemFile).ChildrenAmount();
                
                else
                    amount++;
            }
            return amount;
        }

        public static string DOSAttributes(this FileSystemInfo fileSystemInfo)
        {
            FileAttributes fileAttributes = fileSystemInfo.Attributes;
            return ((fileAttributes & FileAttributes.ReadOnly) > 0 ? "r" : "-") +
                ((fileAttributes & FileAttributes.Archive) > 0 ? "a" : "-") +
                ((fileAttributes & FileAttributes.Hidden) > 0 ? "h" : "-") +
                ((fileAttributes & FileAttributes.System) > 0 ? "s" : "-");
        }
    }
}
