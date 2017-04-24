using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

            DirectoryInfo directoryInfo = new DirectoryInfo(fileLoc);
            Directory root = new Directory("Work");
            directoryInfo.ChildrenAmount(ref root);
            root.text = directoryInfo.Name + " " + root.childrenAmount + " " + directoryInfo.DOSAttributes();

            root.CreateList(directoryInfo, ref root);
            root.SortList(root);
            root.ListFiles(root);

            System.Console.WriteLine("\nNajstarszy plik: " + directoryInfo.TheOldestElement().ToString());

            FileStream fileStream = new FileStream("C:\\Work\\work.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fileStream, root.list);
            fileStream.Flush();
            fileStream.Close();

            root = new Directory("Work");
            directoryInfo.ChildrenAmount(ref root);
            root.text = directoryInfo.Name + " " + root.childrenAmount + " " + directoryInfo.DOSAttributes();

            fileStream = new FileStream("C:\\Work\\work.dat", FileMode.Open);
            root.list = (List<Directory>)formatter.Deserialize(fileStream);

            root.ListFiles(root);

            System.Console.ReadKey();
        }
    }

    static class DirectoryInfoExtensions
    {
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

        public static void ChildrenAmount(this DirectoryInfo directoryInfo, ref Directory directory)
        {
            int amount = 0;
            foreach (FileSystemInfo systemFile in directoryInfo.GetFileSystemInfos())
            {
                if ((systemFile.Attributes & FileAttributes.Directory) == FileAttributes.Directory)   // is directory
                    amount += ((DirectoryInfo)systemFile).ChildrenAmount() + 1;

                else
                    amount++;
            }

            directory.childrenAmount = amount;
        }

        private static int ChildrenAmount(this DirectoryInfo directoryInfo)
        {
            int amount = 0;
            foreach (FileSystemInfo systemFile in directoryInfo.GetFileSystemInfos())
            {
                if ((systemFile.Attributes & FileAttributes.Directory) == FileAttributes.Directory)   // is directory
                    amount += ((DirectoryInfo)systemFile).ChildrenAmount() + 1;

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
