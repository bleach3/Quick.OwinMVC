using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Utils.Helper
{


    public  class Kernel32OutputDebugString2
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        /// <summary>
        /// 输出到 degubview  有界面无界面 时,都可以在degubview.exe 上查看.方便调试.
        /// </summary>
        /// <param name="message"></param>
        public static void COutputDebugString(string message)
        {
            OutputDebugString(message);
        }
    }
}
