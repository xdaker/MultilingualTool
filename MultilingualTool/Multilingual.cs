using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multilingual;

namespace MultilingualTool
{
    public class Multilingual
    {
        public static TransApi TransApi = new TransApi("2015063000000001", "1435660288");

        public string ScannPath { get;private set; }

        public LangType Type { get; set; }

        public string  JsonPath { get; private set; }

        /// <summary>
        /// 资源字典，键值是文件路径，值是该文件中的中文字符串（字符串包含双引号）
        /// </summary>
        public Dictionary<string, List<string>> ChineseDictionary { get; private set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// 翻译配置文件用的键值字典，字典的键值是：需要翻译的文本，值是：查找文本所需要的键值
        /// </summary>
        public Dictionary<string, string> KeyDictionary { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 通过扫描路径来初始化信息
        /// </summary>
        /// <param name="scannPath"></param>
        public virtual void InitializationInformationFormScannPath(string scannPath)
        {
            ScannPath = scannPath;
            FileHelper flieHelper = new FileHelper();
            //获取文件夹下所有文件的路径
            var paths = FileHelper.GetAllFliePath(scannPath);
            //筛选路径
            var newlist = flieHelper.ScreenFliePath(paths, ".cs", ExistPosition.Suffix);
            newlist = flieHelper.ExcludeCharacter(newlist, @"\obj\");
            //找出含有中文的文件，并筛选出中文
            foreach (var path in newlist)
            {
                var text = flieHelper.ReadFile(path);
                var chineseList = new List<string>();
                chineseList.AddRange(flieHelper.GetChineseString(text));
                ChineseDictionary.Add(path, chineseList);
            }
        }

        /// <summary>
        /// 通过Json文件初始化信息
        /// </summary>
        /// <param name="jsonPath"></param>
        public virtual void InitializationInformationFormJson(string jsonPath)
        {
            JsonPath = jsonPath;
            ChineseDictionary = JsonSerializer.JsonFileToObject<Dictionary<string, List<string>>>(jsonPath);
        }

        /// <summary>
        /// 删除字符串中的双引号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeleteQuotes(string str)
        {
            if (!str.Contains("\"")) return str;
            var s =str.Split("\"".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return s.Aggregate("", (current, s1) => current + s1);
        }

        /// <summary>
        /// 把资源字典写到Json
        /// </summary>
        /// <returns></returns>
        public bool WriteJson()
        {
            try
            {
                FileHelper flieHelper = new FileHelper();
                return flieHelper.WriteFile(@"./JsonFile/Chinese.json", JsonSerializer.ObjectToJson(ChineseDictionary));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }

        /// <summary>
        /// 获得翻译配置文件用的键值字典，返回的字典的键值是：需要翻译的文本，值是：查找文本所需要的键值
        /// </summary>
        /// <returns></returns>
        public virtual void GenerateKey()
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            foreach (var value in ChineseDictionary.Values)
            {
                
                foreach (var str in value)
                {
                    if (keys.Keys.Contains(str)) continue;
                    //去掉双引号
                    var newstr = DeleteQuotes(str);
                    //选着前面两个文字
                    var  towstr = newstr.Length > 2 ? newstr.Insert(2, "\0") : newstr;
                    //翻译生成键值
                    var key = TransApi.GetTransResult(towstr, LangType.auto.ToString(), LangType.en.ToString());
                    //添加到翻译文本和翻译结果的字典中
                    keys.Add(str,key);
                }
            }
            KeyDictionary= keys;
        }
    }
}
