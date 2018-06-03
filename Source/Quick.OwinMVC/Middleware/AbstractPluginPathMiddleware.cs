﻿using Microsoft.Owin;
using Quick.OwinMVC.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Quick.OwinMVC.Controller;
using System.IO;
using System.IO.Compression;
using Quick.OwinMVC.Hunter;

namespace Quick.OwinMVC.Middleware
{
    public abstract class AbstractPluginPathMiddleware : OwinMiddleware, IOwinContextCleaner, IPropertyHunter
    {
        public const String QOMVC_PLUGIN_KEY = "QOMVC_PLUGIN_KEY";
        public const String QOMVC_PATH_KEY = "QOMVC_PATH_KEY";

        /// <summary>
        /// 路由
        /// </summary>
        public String Route { get; private set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string ImageType { get; set; }
        /// <summary>
        /// 是否启用压缩
        /// </summary>
        protected bool EnableCompress { get; set; } = false;
        /// <summary>
        /// 额外的HTTP头
        /// </summary>
        public IDictionary<string, string> AddonHttpHeaders { get; private set; }

        private Regex route;

        public AbstractPluginPathMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public virtual Task InvokeNotMatch(IOwinContext context)
        {
            return Next.Invoke(context);
        }

        public override Task Invoke(IOwinContext context)
        {
            //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"@@@ AbstractPluginPathMiddleware ,OriginalString={context.Request.Uri.OriginalString},LocalPath={context.Request.Uri.LocalPath}  ,Route={Route}");
            bool bIsMatch = false;
            String path = context.Get<String>("owin.RequestPath");
            string strfix = Path.GetExtension(path);
            if ( ( context.Request.Uri.LocalPath.Contains("Plugin.CCEX/cc")  ) &&
                context.Request.Uri.LocalPath.Contains("/UpLoadFile_")
                )
            {
                string[] fixList = this.ImageType.Split('#');
                var retClientConfirm = fixList.Where(x => x.ToLower().CompareTo(strfix.ToLower()) == 0).FirstOrDefault();
                if (retClientConfirm != null)
                //if (strfix.ToLower().Contains(".png") ||
                //            strfix.ToLower().Contains(".jpg") ||
                //            strfix.ToLower().Contains(".jpge") ||
                //            strfix.ToLower().Contains(".bmp") ||
                //            strfix.ToLower().Contains(".ico") )
                {
                    string[] strRetTemp = context.Request.Uri.LocalPath.Split('/');
                    if(strRetTemp.Length>=5 && strRetTemp[1]== "Plugin.CCEX" && strRetTemp[4].Contains("UpLoadFile_"))
                    {
                        bIsMatch = true;
                        var myImgViewMiddleware = Server.Instance.GetMiddleware<ImgViewMiddleware>();
                        string strUrlpath = $"Config/Template/{strRetTemp[3]}/{strRetTemp[4]}";
                        //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"*** myImgViewMiddleware  Path={context.Request.Uri.OriginalString} ,strUrlpath={strUrlpath}");
                        
                        return myImgViewMiddleware.Invoke(context, context.Get<String>(QOMVC_PLUGIN_KEY), strUrlpath);
                    }
                    
                }

                return InvokeNotMatch(context);
            }
           
            if ((false==route.IsMatch(path)   ))
                return InvokeNotMatch(context);

            //如果还没有解析插件名称和路径
            if (context.Get<String>(QOMVC_PLUGIN_KEY) == null)
            {
                var groups = route.Match(path).Groups;
                var dic = route.GetGroupNames().ToDictionary(name => name, name => groups[name].Value);
                foreach (var key in dic.Keys.Where(t => t != "0"))
                    context.Environment.Add(key, dic[key]);
            }

            //添加额外的HTTP头
            if (AddonHttpHeaders != null && AddonHttpHeaders.Count > 0)
                foreach (var header in AddonHttpHeaders)
                    context.Response.Headers[header.Key] = header.Value;

            //交给派生的Middleware
            return Invoke(context, context.Get<String>(QOMVC_PLUGIN_KEY), context.Get<String>(QOMVC_PATH_KEY));
        }

        public abstract Task Invoke(IOwinContext context, String plugin, String path);
        
        void IOwinContextCleaner.Clean(IOwinContext context)
        {
            if (context.Environment.ContainsKey(QOMVC_PLUGIN_KEY))
                context.Environment.Remove(QOMVC_PLUGIN_KEY);
            if (context.Environment.ContainsKey(QOMVC_PATH_KEY))
                context.Environment.Remove(QOMVC_PATH_KEY);
        }

        public virtual void Hunt(string key, string value)
        {
            switch (key)
            {
                case nameof(EnableCompress):
                    EnableCompress = bool.Parse(value);
                    break;
                case nameof(Route):
                    Route = value;
                    String fullRoute = $"/:{QOMVC_PLUGIN_KEY}/{Route}/:{QOMVC_PATH_KEY}";
                    route = RouteBuilder.RouteToRegex(fullRoute);
                    break;
                case nameof(ImageType):
                    ImageType = value;
                    break;
                case nameof(AddonHttpHeaders):
                    AddonHttpHeaders = new Dictionary<string, string>();
                    foreach (var headerKeyValue in value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var tmpStrs = headerKeyValue.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmpStrs.Length < 2)
                            continue;
                        var headerKey = tmpStrs[0].Trim();
                        var headerValue = string.Join(":", tmpStrs.Skip(1));
                        AddonHttpHeaders[headerKey] = headerValue;
                    }
                    break;
            }
        }
    }
}
