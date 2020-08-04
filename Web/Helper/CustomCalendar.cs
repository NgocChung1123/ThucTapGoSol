using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;

namespace HSCB.Helper
{
    public class CalendarEventArgs 
    {
        public int Arg1;
        public int Arg2;
        public int Arg3;
    }

    public delegate void BindCellEventHandler(object sender, CalendarEventArgs e);

    public class CalendarNote
    {
        public int Day { get; set; }
        public string Text { get; set; }
    }

    public class CustomCalendar
    {
        public event BindCellEventHandler BindCell;
        public String CssClass { get; set; }

        public String GeneratedText {get;set;}
        public CalendarEventArgs Event { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CurrentDate { get; set; }
        public String Title { get; set; }
        public List<CalendarNote> CalendarNotes { get; set; }

        protected virtual void OnBindCell(CalendarEventArgs e)
        {
            if (BindCell != null) BindCell(this, e);
        }

        public void CreateCalendar(int month, int year)
        {
            this.Month = month;
            this.Year = year;

            if (this.CalendarNotes == null) this.CalendarNotes = new List<CalendarNote>();

            GeneratedText = String.Empty;
            GeneratedText += "<table class='" + this.CssClass + "'>";
            GeneratedText += "<tr><th colspan='7'>" + this.Title + "</th></tr>";
            GeneratedText += "<tr class='days'>";
            GeneratedText += "<td style='width: 14%'>CN</td>";
            GeneratedText += "<td style='width: 14%'>T2</td>";
            GeneratedText += "<td style='width: 14%'>T3</td>";
            GeneratedText += "<td style='width: 14%'>T4</td>";
            GeneratedText += "<td style='width: 14%'>T5</td>";
            GeneratedText += "<td style='width: 14%'>T6</td>";
            GeneratedText += "<td style='width: 14%'>T7</td>";
            GeneratedText += "</tr>";

            List<DateTime> dateList = GetDatesInMonth(year, month);
            List<DateTime> prevMonthDateList = new List<DateTime>();
            List<DateTime> nextMonthDateList = new List<DateTime>();

            if (month == 1)
            {
                prevMonthDateList = GetDatesInMonth(year - 1, 12);
            }
            else
            {
                prevMonthDateList = GetDatesInMonth(year, month - 1);
            }
            if (month == 12)
            {
                nextMonthDateList = GetDatesInMonth(year + 1, 1);
            }
            else
            {
                nextMonthDateList = GetDatesInMonth(year, month + 1);
            }           

            int lastDayInMonthPos = 0;

            for (int weekInMonth = 1; weekInMonth < 7; weekInMonth++)
            {
                GeneratedText += "<tr>";
                for (int dateInWeek = 0; dateInWeek < 7; dateInWeek++)
                {
                    GeneratedText += "<td class='calendar-date'>";
                    bool dateInMonth = false;
                    foreach (DateTime datetime in dateList)
                    {
                        if ((int)datetime.DayOfWeek == dateInWeek && datetime.Day <= ((weekInMonth - 1) * 7 + dateInWeek + 1) && datetime.Day > (weekInMonth - 2) * 7 + dateInWeek + 1)
                        {
                            dateInMonth = true;

                            String today = "";
                            String warning = "";
                            String noteText = "";

                            if (datetime.Date == DateTime.Now.Date)
                            {
                                today = "today";
                            }

                            foreach (var note in this.CalendarNotes)
                            {
                                if (note.Day == datetime.Day)
                                {
                                    warning = "warning-date";
                                    noteText = note.Text;
                                }
                            }                            

                            GeneratedText += "<span class='in-month " + today + " " + warning + "' id='" + Format.FormatDate(datetime) + "'>" + datetime.Day.ToString() + "</span>";

                            if (noteText != "")
                            {
                                GeneratedText += "<div class='calendar-note'>" + noteText + "</div>";
                            }

                            OnBindCell(this.Event);

                            lastDayInMonthPos = (weekInMonth - 1) * 7 + dateInWeek + 1;

                            this.CurrentDate = datetime;
                        }
                    }
                    if (!dateInMonth)
                    {
                        if (weekInMonth < 2)
                        {
                            int minDay = 25 + dateInWeek;
                            if (month == 2)
                            {
                                minDay = 22 + dateInWeek;
                            }
                            foreach (DateTime datetime in prevMonthDateList)
                            {
                                if ((int)datetime.DayOfWeek == dateInWeek && datetime.Day > minDay)
                                {
                                    dateInMonth = true;

                                    GeneratedText += "<span class='other-month' id='" + Format.FormatDate(datetime) + "'>" + datetime.Day.ToString() + "</span>";

                                    OnBindCell(this.Event);

                                    this.CurrentDate = datetime;
                                }
                            }
                        }
                        else if (weekInMonth > 4)
                        {
                            DateTime dt = GetPosNDay(nextMonthDateList, (weekInMonth - 1) * 7 + dateInWeek - lastDayInMonthPos);

                            GeneratedText += "<span class='other-month' id='" + Format.FormatDate(dt) + "'>" + dt.Day.ToString() + "</span>";

                            OnBindCell(this.Event);
                            this.CurrentDate = dt;
                        }
                    }
                    GeneratedText += "</td>";
                }
                GeneratedText += "</tr>";
            }
            GeneratedText += "</table>";
        }

        //Lay ngay thu n trong thang ung voi thu trong tuan
        private DateTime GetPosNDay(List<DateTime> dateInMonth, int n)
        {
            DateTime resultDate = dateInMonth.ElementAt(n);

            return resultDate;
        }

        private List<DateTime> GetDatesInMonth(int year, int month)
        {

            var dates = new List<DateTime>();

            // Loop from the first day of the month until we hit the next month, moving forward a day at a time
            for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;

        }
    }
}