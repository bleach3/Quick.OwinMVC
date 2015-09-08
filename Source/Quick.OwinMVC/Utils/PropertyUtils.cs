﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quick.OwinMVC.Utils
{
    public class PropertyUtils
    {
        public static IDictionary<String, String> Load(String content)
        {
            IDictionary<String, String> dict = new Dictionary<String, String>();

            //参考正则表达式: (?!#)(?'key'[^#][^\s]*)\s*=\s*(?'value'.*)\s*
            content = content.Replace("\\\r\n", "").Replace("\\\r", "").Replace("\\\n", "");

            Regex regex = new Regex(@"^(?!#)\s*(?'key'[^#][^\s]*?)\s*=\s*(?'value'.*?)\s*$", RegexOptions.Multiline);
            foreach (Match match in regex.Matches(content))
            {
                String key = match.Groups["key"].Value;
                String value = match.Groups["value"].Value;
                value = value.Replace(@"\r", "\r").Replace(@"\n", "\n").Replace(@"\t", "\t").Replace(@"\f", "\f").Replace(@"\\", "\\");
                dict[key] = value;
            }
            return dict;
        }

        public static IDictionary<String, String> LoadFile(String fileName)
        {
            return Load(File.ReadAllText(fileName));
        }
    }
}
