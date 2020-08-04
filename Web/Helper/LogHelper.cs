using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    public static class LogHelper
    {
        private static DAL.SystemLog systemLog = null;      
  
        static LogHelper() {
            if (systemLog == null) systemLog = new DAL.SystemLog();
        }

        public static void Log(int canboID, String info)
        {
            SystemLogInfo logInfo = new SystemLogInfo();
            logInfo.CanBoID = canboID;
            logInfo.LogInfo = info;
            logInfo.LogTime = DateTime.Now;
            logInfo.LogType = (int)LogType.Other;

            try
            {
                systemLog.Insert(logInfo);
            }
            catch
            {
            }
        }

        public static void Log(int canboID, String logIngo, int logType)
        {
            SystemLogInfo systemLogInfo = new SystemLogInfo();
            systemLogInfo.CanBoID = canboID;
            systemLogInfo.LogInfo = logIngo;
            systemLogInfo.LogTime = DateTime.Now;
            systemLogInfo.LogType = logType;

            try
            {
                systemLog.Insert(systemLogInfo);
            }
            catch
            {
            }
        }


        public static void Log(int canboID, String logInfo, int logType, DateTime logTime)
        {
            SystemLogInfo systemLogInfo = new SystemLogInfo();
            systemLogInfo.CanBoID = canboID;
            systemLogInfo.LogInfo = logInfo;
            systemLogInfo.LogTime = logTime;
            systemLogInfo.LogType = logType;

            try
            {
                systemLog.Insert(systemLogInfo);
            }
            catch
            {
            }
        }        
    }
}