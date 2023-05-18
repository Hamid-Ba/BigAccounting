using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace BigAccounting
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetYear(date).ToString() + "/" + calendar.GetMonth(date).ToString("00") + "/" + calendar.GetDayOfMonth(date).ToString("00");
        }

        public static string ToShamsi(this DateTime? date)
        {
            PersianCalendar calendar = new PersianCalendar();
            if (date != null)
                return calendar.GetYear((DateTime)date).ToString() + "/" + calendar.GetMonth((DateTime)date).ToString("00") + "/" + calendar.GetDayOfMonth((DateTime)date).ToString("00");
            else return "-";
        }
    }
}
