﻿using Quick.OwinMVC.WebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Nowin;
using Owin;
using Microsoft.Owin.Builder;

namespace Quick.OwinMVC.Server.Nowin
{
    public class Wrapper : IWebServer
    {
        private IDisposable webApp;

        public bool IsRuning { get { return webApp != null; } }

        public void Dispose()
        {
            if (webApp != null)
                webApp.Dispose();
            webApp = null;
        }

        public void Start(Action<IAppBuilder> app, IPEndPoint endpoint)
        {
            AppBuilder appBuilder = new AppBuilder();
            app(appBuilder);
            var builder = ServerBuilder.New().SetEndPoint(endpoint).SetOwinApp(appBuilder.Build());
            //builder.SetCertificate(new X509Certificate2("certificate.pfx", "password"));
            webApp = builder.Start();
        }

        public void Stop()
        {
            Dispose();
        }
    }
}
