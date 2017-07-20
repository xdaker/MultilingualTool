using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Multilingual
{
    public class FlieHelper
    {

        /// <summary>
        /// 一行一行的度文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public List<string> ReadFileLine(string path)
        {
            if (!File.Exists(path)) return null;
            List<string> list = new List<string>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;
                    list.Add(line);
                }
            }
            return list;
        }

        /// <summary>
        /// 读取整个文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadFile(string path)
        {
            if (!File.Exists(path)) return null;
            string str;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                str = sr.ReadToEnd();
            }
            return str;
        }

        /// <summary>
        /// 筛选出不包含指定字符的字符串
        /// </summary>
        /// <param name="stringList">字符串列表</param>
        /// <param name="exclude">指定字符</param>
        /// <returns></returns>
        public List<string> ExcludeCharacter(List<string> stringList ,string exclude)
        {
            return stringList.Where(v => !v.Contains(exclude)).ToList();
        }

        /// <summary>
        /// 筛选出文件路径中包含的特定文字的文件路径
        /// </summary>
        /// <param name="flies">文件路径</param>
        /// <param name="exist">路径中包含的文字</param>
        /// <param name="position">特定文字在字符串的位置</param>
        /// <returns></returns>
        public List<string> ScreenFliePath(List<string> flies , string exist , ExistPosition position = ExistPosition.Suffix )
        {
            var results = flies.Where(v => v.Contains(exist)).ToList();
            switch (position)
            {
                case ExistPosition.Not:
                    break;
                case ExistPosition.Suffix:
                    results =
                        results.Where(v => (v.IndexOf(exist, StringComparison.Ordinal) + exist.Length) == v.Length)
                            .ToList();
                    break;
            }
           return  results;
        }

        /// <summary>
        /// 获取一个文件路径中的所有文件
        /// </summary>
        /// <param name="path">一个文件路径</param>
        /// <returns>返回完成的文件路径</returns>
        public static List<string> GetAllFliePath(string path)
        {
            if (!Directory.Exists(path)) return null;
            DirectoryInfo theFolder = new DirectoryInfo(path);
            List<string> list = new List<string>();
            //遍历文件夹
            foreach (var nextFolder in theFolder.GetDirectories())
            {
                list.AddRange(nextFolder.GetFiles().Select(nextFile => nextFile.FullName));
                foreach (var directoryInfo in nextFolder.GetDirectories())
                {
                    list.AddRange(GetAllFliePath(directoryInfo.FullName));
                }
            }
            return list;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">写入的内容</param>
        public bool WriteFile(string path , string content)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //开始写入
                    sw.Write(content);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                }
                fs.Close();
                return true;
            }
        }

        /// <summary>
        /// 获取包含中文的字符串，中文字符串两边必须包含在双引号中。如（"第三方"）
        /// </summary>
        /// <param name="text">原文</param>
        /// <returns>含中文的字符串列表</returns>
        public List<string> GetChineseString(string text)
        {
            var reg = new Regex("\"[\\S]*[\u4e00-\u9fa5]+[\\S]*\"");
            return (from Match v in reg.Matches(text) select v.Value).ToList();
        }
    }

    public enum ExistPosition
    {
        Not =0,
        Suffix,

    }
}
