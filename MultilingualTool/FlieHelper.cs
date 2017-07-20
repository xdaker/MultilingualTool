using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilingualTool
{
    public class FlieHelper
    {

        private static List<string> GetAllFliePath(string path)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            List<string> list = new List<string>();
            //遍历文件夹
            foreach (DirectoryInfo nextFolder in TheFolder.GetDirectories())
            {
                foreach (FileInfo nextFile in nextFolder.GetFiles())
                {
                    list.Add(nextFile.FullName);
                }
                foreach (var directoryInfo in nextFolder.GetDirectories())
                {
                    list.AddRange(GetAllFliePath(directoryInfo.FullName));
                }
            }
            return list;
        }
    }
}
