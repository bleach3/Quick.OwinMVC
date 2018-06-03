using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Quick.OwinMVC.Controller;
using Quick.OwinMVC.Resource;
using System.Net;
using Quick.OwinMVC.Utils;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using Quick.OwinMVC.Hunter;

namespace Quick.OwinMVC.Middleware
{
    public class ResourceMiddleware : AbstractPluginPathMiddleware, IPropertyHunter, IAssemblyHunter
    {
        //(资源的缓存过期时间，单位：秒)默认一天
        private double Expires = 86400;
        //资源的ETag是否使用MD5值
        private Boolean UseMd5ETag = false;

        private ResourceWebRequestFactory resourceWebRequestFactory;

        /// <summary>
        /// 静态文件目录
        /// </summary>
        public String StaticFileFolder
        {
            get { return resourceWebRequestFactory.StaticFileFolder; }
            private set { resourceWebRequestFactory.StaticFileFolder = value; }
        }

        public ResourceMiddleware(OwinMiddleware next) : base(next)
        {
            resourceWebRequestFactory = new ResourceWebRequestFactory();
            resourceWebRequestFactory.AssemblyMap = new Dictionary<String, Assembly>();
            //注册resource:前缀URI处理程序
            WebRequest.RegisterPrefix("resource:", resourceWebRequestFactory);
        }

        private Stream getUrlStream(String url, out ResourceWebResponse response)
        {
            Uri uri = new Uri(url);
            response = null;
            try { response = WebRequest.Create(uri).GetResponse() as ResourceWebResponse; }
            catch(Exception ex) {
                App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"getUrlStream error={ex.Message}");
            }
            var stream = response?.GetResponseStream();
            return stream;
        }


        public override Task InvokeNotMatch(IOwinContext context)
        {
            ResourceWebResponse resourceResponse;
            var stream = getUrlStream($"resource://0{context.Request.Path}", out resourceResponse);
            if (stream == null)
                return base.InvokeNotMatch(context);
            return handleResource(context, stream, resourceResponse, Expires, AddonHttpHeaders);
        }

        public override Task Invoke(IOwinContext context, string plugin, string path)
        {
            //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"ResourceMiddleware Path={context.Request.Uri.OriginalString} ");

            return InvokeFinal(context, Route, null, plugin, path, t => InvokeNotMatch(context), Expires, AddonHttpHeaders);
        }

        public Task InvokeFinal(IOwinContext context, string prefix, string suffix, string plugin, string path,
            Func<IOwinContext, Task> noMatchHandler, double expires, IDictionary<string, string> addonHttpHeaders,
            bool IsCCEXResource=false)
        {
            //加前缀
            if (!String.IsNullOrEmpty(prefix))
                path = $"{prefix}/{path}";

            List<String> pathList = new List<string>();
            pathList.Add(path);
            //加后缀
            if (!String.IsNullOrEmpty(suffix))
                path = $"{path}{suffix}";
            pathList.Add(path);

            Stream stream;
            ResourceWebResponse resourceResponse;

            if(IsCCEXResource)//组态资源
            {
                //Config\Template\00ab4f6fa57242599b2caa134e4ee434
                string strfilepath = Path.Combine(path);
                var fileInfo = new FileInfo(strfilepath);
                stream = fileInfo.OpenRead();
                if (stream != null)
                    return handleResource(context, stream, fileInfo, expires, addonHttpHeaders, strfilepath);
            }
            else
            {
                foreach (var currentPath in pathList)
                {
                    stream = getUrlStream($"resource://{plugin}/{currentPath}", out resourceResponse);
                    if (stream != null)
                        return handleResource(context, stream, resourceResponse, expires, addonHttpHeaders);
                }
            }

            return noMatchHandler(context);
        }

        private Task handleResource(IOwinContext context, Stream stream, ResourceWebResponse resourceResponse, double expires, IDictionary<string, string> addonHttpHeaders)
        {
            //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"ResourceMiddlewar url ");
            var req = context.Request;
            var rep = context.Response;
            //验证缓存有效
            {
                //有的资源 ,客户端请求Cache-Control: max-age=0 ,必须返回一个明确的时间.
                //max-age<=0 时 向server 发送http 请求确认 ,该资源是否有修改 
                //有修改的话 返回200, 无的话 返回304.
                var retClientConfirm =req.Headers.Keys.Where(x => x.ToLower().CompareTo("cache-control") == 0 ).FirstOrDefault();
                if (retClientConfirm != null)
                {
                    //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"ResourceMiddlewar222e Path={context.Request.Uri.OriginalString} ret!=null ");
                    rep.Headers["Cache-Control"] = $"public,max-age={expires}";//1天失效.
                }
                else
                {
                    //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"ResourceMiddleware Path={context.Request.Uri.OriginalString} ret==null ");
                }
                    
                //===================
                //先验证最后修改时间
                //===================
                var resourceLastModified = resourceResponse.LastModified;
                //最后修改时间判断部分
                var clientLastModified = req.Headers.Get("If-Modified-Since");
                if (clientLastModified != null)
                {
                    string resourceLastModifiedR = resourceLastModified.ToString("R");
                    if (clientLastModified == resourceLastModifiedR)
                    {
                        //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"*** ResourceMiddleware 验证缓存有效 Path={context.Request.Uri.OriginalString} ,Expires={expires} ,clientLastModified == resourceLastModified ");
                        rep.StatusCode = 304;
                        return Task.Run(() => {
                            stream.Close();
                            stream.Dispose();
                        }
                        );
                    }
                }
                //===================
                //然后验证ETag
                //===================
                //ETag设置判断部分
                String serverETag = null;
                if (UseMd5ETag)
                    serverETag = HashUtils.ComputeETagByMd5(stream);
                else
                    serverETag = resourceResponse.LastModified.Ticks.ToString();
                var clientETag = req.Headers.Get("If-None-Match");
                //如果客户端的ETag值与服务端相同，则返回304，表示资源未修改
                if (serverETag == clientETag)
                {
                    //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"*** ResourceMiddleware 表示资源未修改 Path={context.Request.Uri.OriginalString},Expires={expires} 2");
                    rep.StatusCode = 304;
                    return Task.Run(() => {
                        stream.Close();
                        stream.Dispose(); });
                }
                rep.ETag = serverETag;
                stream.Position = 0;
            }

            //===================
            //设置缓存 等头信息.
            //===================
            rep.Expires = new DateTimeOffset(DateTime.Now.AddSeconds(expires));
            rep.Headers["Cache-Control"] = $"public,max-age={expires}";//1天失效.
            rep.Headers["Last-Modified"] = resourceResponse.LastModified.ToUniversalTime().ToString("R");
            rep.Headers["Access-Control-Allow-Origin"] = "*";//如果设置 Access-Control-Allow-Origin:*，则允许所有域名的脚本访问该资源

            return context.Output(stream, true, EnableCompress, resourceResponse.Uri.LocalPath, addonHttpHeaders);
        }
        private Task handleResource(IOwinContext context, Stream stream, FileInfo fileinfo, double expires, IDictionary<string, string> addonHttpHeaders,string filepath)
        {
            //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"ResourceMiddlewar fileinfo ");
            var req = context.Request;
            var rep = context.Response;
            //验证缓存有效
            {
                //有的资源 ,客户端请求Cache-Control: max-age=0 ,必须返回一个明确的时间.
                //max-age<=0 时 向server 发送http 请求确认 ,该资源是否有修改 
                //有修改的话 返回200, 无的话 返回304.
                var retClientConfirm = req.Headers.Keys.Where(x => x.ToLower().CompareTo("cache-control") == 0).FirstOrDefault();
                if (retClientConfirm != null)
                {
                    //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"ResourceMiddlewar fileinfo Path={context.Request.Uri.OriginalString} ret!=null ");
                    rep.Headers["Cache-Control"] = $"public,max-age={expires}";//1天失效.
                }
                else
                {
                    //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"ResourceMiddleware fileinfo Path={context.Request.Uri.OriginalString} ret==null ");
                }

                //===================
                //先验证最后修改时间
                //===================
                var resourceLastModified = fileinfo.LastWriteTime;
                //最后修改时间判断部分
                var clientLastModified = req.Headers.Get("If-Modified-Since");
                if (clientLastModified != null)
                {
                    string resourceLastModifiedR = resourceLastModified.ToString("R");
                    if (clientLastModified == resourceLastModifiedR)
                    {
                        //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"*** ResourceMiddleware fileinfo 验证缓存有效 Path={context.Request.Uri.OriginalString} ,Expires={expires} ,clientLastModified == resourceLastModified ");
                        rep.StatusCode = 304;
                        return Task.Run(() =>
                        {
                            stream.Close();
                            stream.Dispose();
                        }
                        );
                    }
                }
                //===================
                //然后验证ETag
                //===================
                //ETag设置判断部分
                String serverETag = null;
                if (UseMd5ETag)
                    serverETag = HashUtils.ComputeETagByMd5(stream);
                else
                    serverETag = fileinfo.LastWriteTime.Ticks.ToString();
                var clientETag = req.Headers.Get("If-None-Match");
                //如果客户端的ETag值与服务端相同，则返回304，表示资源未修改
                if (serverETag == clientETag)
                {
                    //App.Core.Utils.Helper.Kernel32OutputDebugString2.COutputDebugString($"*** ResourceMiddleware  fileinfo 表示资源未修改 Path={context.Request.Uri.OriginalString},Expires={expires} 2");
                    rep.StatusCode = 304;
                    return Task.Run(() =>
                    {
                        stream.Close();
                        stream.Dispose();
                    });
                }
                rep.ETag = serverETag;
                stream.Position = 0;
            }

            //===================
            //设置缓存 等头信息.
            //===================
            rep.Expires = new DateTimeOffset(DateTime.Now.AddSeconds(expires));
            rep.Headers["Cache-Control"] = $"public,max-age={expires}";//1天失效.
            rep.Headers["Last-Modified"] = fileinfo.LastWriteTime.ToUniversalTime().ToString("R");
            rep.Headers["Access-Control-Allow-Origin"] = "*";//如果设置 Access-Control-Allow-Origin:*，则允许所有域名的脚本访问该资源

            return context.Output(stream, true, EnableCompress, filepath, addonHttpHeaders);
        }

        public override void Hunt(string key, string value)
        {
            switch (key)
            {
                case nameof(StaticFileFolder):
                    resourceWebRequestFactory.StaticFileFolder = value;
                    break;
                case nameof(Expires):
                    Expires = double.Parse(value);
                    break;
                case nameof(UseMd5ETag):
                    UseMd5ETag = Boolean.Parse(value);
                    break;
            }
            base.Hunt(key, value);
        }

        public void Hunt(Assembly assembly)
        {
            resourceWebRequestFactory.AssemblyMap[assembly.GetName().Name.ToLower()] = assembly;
        }
    }
}
