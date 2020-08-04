using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gosol.CMS.Utility
{
    public class Log
    {
        public static void Debug(String msg)
        {
            String filename = "Log - " + DateTime.Now.ToString("d-M-y") + ".txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Logs\"+filename, true))
            {
                file.WriteLine(msg);
            }
        }
    }
}
