using NLog;
using System;

namespace SearchEngine.Utils
{
    public static class LogManager
    {
        public static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        #region info messages
        public static void Info(Exception ex)
        {
            try
            {
                logger.Info(ex);
            }
            catch (Exception)
            {
            }
        }

        public static void Info(string msg)
        {
            try
            {
                logger.Info(msg);
            }
            catch (Exception)
            {
            }
        }

        public static void Info(Exception ex, string msg)
        {
            try
            {
                logger.Info(ex, msg);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region error messages
        public static void Error(Exception ex)
        {
            try
            {
                logger.Error(ex);
            }
            catch (Exception)
            {
            }
        }

        public static void Error(string msg)
        {
            try
            {
                logger.Error(msg);
            }
            catch (Exception)
            {
            }
        }

        public static void Error(Exception ex, string msg)
        {
            try
            {
                logger.Error(ex, msg);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
