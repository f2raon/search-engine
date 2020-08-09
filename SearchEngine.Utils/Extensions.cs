using System;
using System.Globalization;
using System.Text;

namespace SearchEngine.Utils
{
    public static class Extensions
    {
        public static string ToStr(this object obj)
        {
            string res = string.Empty;

            if (obj is bool)
                res = ((obj.ToString().ToUpper() == "TRUE") ? "1" : "0");
            else if (obj is decimal)
                res = obj.ToString().Replace('.', ',');
            else if (obj is DateTime)
                res = Convert.ToDateTime(obj, System.Threading.Thread.CurrentThread.CurrentCulture).ToString("dd.MM.yyyy");
            else if (obj is byte[])
                res = Encoding.Default.GetString(obj as byte[]);
            else if (obj is string)
                res = string.IsNullOrEmpty(obj.ToString()) ? string.Empty : obj.ToString();
            else
                res = obj.ToString();

            return res.ToString().Trim();
        }

        public static string GetAllMessages(this Exception ex)
        {
            var res = new StringBuilder();
            
            while (ex != null)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    if (res.Length > 0)
                        res.AppendLine();

                    res.AppendLine(ex.Message);
                }
                ex = ex.InnerException;
            }

            return res.ToStr();
        }

        public static long ToInt64(this object inVal)
        {
            long res = 0;
            if (DBNull.Value != inVal && inVal != null && !string.IsNullOrEmpty(inVal.ToStr()))
                res = Convert.ToInt64(inVal);

            return res;
        }

        public static DateTime ToDateTime(this object source, string format = null)
        {
            DateTime dt = DateTime.MinValue;
            if (DBNull.Value == source || source == null || string.IsNullOrEmpty(source.ToStr()))
                return dt;

            if (string.IsNullOrEmpty(format))
            {
                DateTimeFormatInfo enDtfi = new CultureInfo("en-EN", false).DateTimeFormat;
                dt = Convert.ToDateTime(source.ToStr(), enDtfi);
            }
            else
            {
                DateTime.TryParseExact(source.ToStr(), format, null, DateTimeStyles.None, out dt);
            }

            return dt;
        }
    }
}
