﻿using Quick.OwinMVC.Startup.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Quick.OwinMVC.Startup.Static
{
    
    public static class DebugLauncher
    {
        public static void Launch()
        {
            WinService service = new WinService();
            
            //如果是运行的Windows操作系统，则设置控制台标题
            if (ProgramUtils.IsRuningOnWindows())
                Console.Title = ProgramUtils.GetProgramVersion();

            service.Start(null);
            //while (true)
            //{
            //    Console.Write(">");
            //    var line = Console.ReadLine();
            //    switch (line)
            //    {
            //        case "":
            //            break;
            //        case "help":
            //            Console.WriteLine("help\t显示帮助");
            //            Console.WriteLine("cls\t清空屏幕");
            //            Console.WriteLine("exit\t退出程序");
            //            break;
            //        case "cls":
            //            Console.Clear();
            //            break;
            //        case "exit":
            //            if (ProgramUtils.IsRuningOnWindows())
            //                Environment.Exit(0);
            //            else
            //                Process.GetCurrentProcess().Kill();
            //            return;
            //        default:
            //            Console.WriteLine("未知命令，键入help查看帮助");
            //            break;
            //    }                
            //    if (!ProgramUtils.IsRuningOnWindows())
            //    {
            //        Console.WriteLine("Unknown command.you have to kill this process to terminal program.");
            //        Thread.CurrentThread.Join();
            //    }
            //}
        }
    }
}
