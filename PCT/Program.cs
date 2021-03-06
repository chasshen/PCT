﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCT.Common;
using PCT.UI;

namespace PCT
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainform = new MainForm();
            ComController controller = new ComController(mainform);
            //ZeroForm form = new ZeroForm();
            Application.Run(mainform);
        }
    }
}
