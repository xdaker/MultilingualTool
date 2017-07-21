using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multilingual;

namespace MultilingualTool
{
    public class FinBaseMultilingual : Multilingual
    {
        public static string Folder = "AutoTranslate";
        public static string Node = "AllText";
        public string ReplaceText =
            $"PMSServerConnService.Activator.getResource(\"{Folder}.{Node}\", \"{0}\", PMSServerConnService.Activator.LanguageFlag)";

        public string HeadResourcePath = @"./Resource/Head.txt";

        public string NodeResourcePath = @"./Resource/Node.txt";

        public string GetLangFile(string nodename,string itemText)
        {
            FileHelper fileHelper = new FileHelper();
            string text = fileHelper.ReadFile(HeadResourcePath);
            return string.Format(text, nodename, itemText);
        }

        public string Itemtext = null;
        public string GetSingleItem(string key,string value)
        {
            if (Itemtext != null) return string.Format(Itemtext, key, value);
            var fileHelper = new FileHelper();
            Itemtext = fileHelper.ReadFile(NodeResourcePath);
            return string.Format(Itemtext, key, value);
        }

        public string SetReplaceText(string key)
        {
            return string.Format(ReplaceText, key);
        }
        public void ReplaceCodeText()
        {
            if(ChineseDictionary.Keys.Count<1 && KeyDictionary.Keys.Count<1) return;
            FileHelper fileHelper = new FileHelper();
            foreach (var filePath in ChineseDictionary.Keys)
            {
                var text = fileHelper.ReadFile(filePath);
                foreach (var chinese in ChineseDictionary[filePath])
                {
                    if(!KeyDictionary.Keys.Contains(chinese)) continue;
                    var key = KeyDictionary[chinese];
                    var replace = SetReplaceText(key);
                    text= text.Replace(chinese, replace);
                }
                fileHelper.WriteFile(filePath, text);
            }
        }

        public void GenerateLangConfig()
        {
            if (KeyDictionary.Keys.Count < 1) return;
            List<string> itmes = new List<string>();
            foreach (var chinese in KeyDictionary.Keys)
            {
                var text=GetSingleItem(KeyDictionary[chinese],
                    TransApi.GetTransResult(chinese, LangType.auto.ToString(), Type.ToString()));
                itmes.Add(text);
                itmes.Add("\r\n");
            }
            string itemtext = "";
            foreach (var itme in itmes)
            {
                itemtext += itme;
            }
            var langFile = GetLangFile(Node, itemtext);
            FileHelper fileHelper = new FileHelper();
            fileHelper.WriteFile("./LangFile/Resource.xml", langFile);
        }
    }

}
