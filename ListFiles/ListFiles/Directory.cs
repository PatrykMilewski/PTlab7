﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFiles
{
    class Directory
    {
        public static NameCompare nameCompare = new NameCompare();
        public List<Directory> list;

        public bool isDirectory { get; set; }

        public string text { get; set; }

        public int childrenAmount { get; set; }

        public Directory(string text, bool isDirectory)
        {
            if (isDirectory)
                list = new List<Directory>();

            this.text = text;
            this.isDirectory = isDirectory;
            this.childrenAmount = -1;
        }

        public void CreateList(DirectoryInfo directoryInfo, ref Directory parent, int deep = 0)
        {
            int childrenAmount;
            string consoleOutput;
            foreach (FileSystemInfo systemFile in directoryInfo.GetFileSystemInfos())
            {
                if ((systemFile.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    childrenAmount = ((DirectoryInfo)systemFile).ChildrenAmount();
                    consoleOutput = systemFile.Name + " (" + childrenAmount + ") " + systemFile.DOSAttributes();
                    Directory newDirChild = new Directory(consoleOutput, true);
                    parent.list.Add(newDirChild);
                    newDirChild.CreateList((DirectoryInfo)systemFile, ref newDirChild, deep + 1);
                }
                else
                {
                    consoleOutput = systemFile.Name + " " + ((FileInfo)systemFile).Length + " bajtow " + systemFile.DOSAttributes();
                    parent.list.Add(new Directory(consoleOutput, false));
                }
            }
        }

        public void ListFiles(Directory parent, int deep = 0)
        {
            string consoleOutput, tabs = "";
            for (int i = 0; i < deep; i++)
                tabs += '\t';

            foreach (Directory child in parent.list)
            {
                consoleOutput = tabs + child.text;
                System.Console.WriteLine(consoleOutput);

                if (child.isDirectory)
                    child.ListFiles(child, deep + 1);
            }
        }

        public void SortList(Directory parent)
        {
            foreach (Directory child in parent.list)
            {
                if (child.isDirectory)
                {
                    child.list.Sort(nameCompare);
                    child.SortList(child);
                }
            }
        }
    }
}
