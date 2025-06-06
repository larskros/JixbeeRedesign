using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Hours
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private List<string> SegmentItems = new()
        {
            "Week", "Maand", "Jaar"
        };

        private List<(string Label, double Amount)> WeekDays => new()
        {
            ("Ma", 200),
            ("Di", 200),
            ("Wo", 50),
            ("Do", 0),
            ("Vr", 0),
            ("Za", 0),
            ("Zo", 0)
        };

        private double weeklyTotal { get; set; }
        private double highestWeekEarning { get; set; }

        public List<(int Day, double Amount)> CreateMonthOverview(int daysInMonth)
        {
            var monthOverview = new List<(int Day, double Amount)>();

            for (int day = 1; day <= daysInMonth; day++)
            {
                monthOverview.Add((day, 0)); // Standaard 0 of ander bedrag
            }

            return monthOverview;
        }

        private List<(string Label, double Amount)> Months => new()
        {
            ("Jan", 2600),
            ("Feb", 2740),
            ("Mrt", 2600),
            ("Apr", 2830),
            ("Mei", 3100),
            ("Jun", 2650),
            ("Jul", 800),
            ("Aug", 800),
            ("Sep", 2600),
            ("Okt", 0),
            ("Nov", 0),
            ("Dec", 0)
        };

        private double highestMonthEarning { get; set; }
        private double yearlyTotal { get; set; }

        private int[] weekNumbers { get; set; }
        private int currentWeek { get; set; }
        private int currentMonth { get; set; }
        private int currentYear { get; set; }
        private int year { get; set; }
        private int maxWeek { get; set; }
        private string CurrentMonthName => new DateTime(currentYear, currentMonth, 1).ToString("MMM", new CultureInfo("nl-NL"));
        private bool isNotCurrentDay { get; set; }

        private int activeIndex = 0;

        protected override void OnInitialized()
        {
            var today = DateTime.Today;
            currentYear = today.Year;
            currentMonth = today.Month;
            currentWeek = GetWeekNumber(today);
            maxWeek = GetMaxWeekNumber(currentYear);
            highestWeekEarning = WeekDays.Max(x => x.Amount);
            highestMonthEarning = Months.Max(x => x.Amount);
            weeklyTotal = WeekDays.Sum(x => x.Amount);
            //monthlyTotal = Months.Sum(x => x.Amount);
            yearlyTotal = Months.Sum(x => x.Amount);
            //highestMonthEarning = FindMax(amountJanuary, amountFebruary, amountMarch, amountApril, amountMay, amountJune, amountJuly, amountAugust, amountSeptember, amountOctober, amountNovember, amountDecember);
            //monthlyTotal = Math
        }
        decimal FindMax(params decimal[] values)
        {
            return values.Max();
        }

        private async Task OnActiveIndexChanged(int index)
        {
            activeIndex = index;
            await ActiveIndexChanged.InvokeAsync(index);
            StateHasChanged();
        }

        private void GoBackTimeSelection()
        {
            if (activeIndex == 0)
            {
                if (currentWeek > 1)
                {
                    currentWeek--;
                    Console.WriteLine("minus 1 week");
                }
                else
                {
                    currentYear--;
                    maxWeek = GetMaxWeekNumber(currentYear);
                    currentWeek = maxWeek;
                    Console.WriteLine("minus 1 year");
                }
            }
            else if (activeIndex == 1)
            {
                if (currentMonth > 1)
                {
                    currentMonth--;
                }
                else if (currentYear > 1)
                {
                    currentMonth = 12;
                    currentYear--;
                    maxWeek = GetMaxWeekNumber(currentYear);
                }
            }
            else if (activeIndex == 2)
            {
                if (currentYear > 1)
                {
                    currentYear--;
                    maxWeek = GetMaxWeekNumber(currentYear);
                }
            }
            var today = DateTime.Today;
            if (currentWeek != GetWeekNumber(today) || currentMonth != today.Month || currentYear != today.Year)
            {
                isNotCurrentDay = true;
            }
            else
            {
                isNotCurrentDay = false;
            }
        }
        private void GoForwardTimeSelection()
        {
            if (activeIndex == 0)
            {
                if (currentWeek < maxWeek)
                {
                    currentWeek++;
                    Console.WriteLine("plus 1 week");
                }
                else
                {
                    currentYear++;
                    currentWeek = 1;
                    maxWeek = GetMaxWeekNumber(currentYear);
                    Console.WriteLine("plus 1 year");
                }
            }
            else if (activeIndex == 1)
            {
                if (currentMonth < 12)
                {
                    currentMonth++;
                }
                else
                {
                    currentMonth = 1;
                    currentYear++;
                    maxWeek = GetMaxWeekNumber(currentYear);
                }
            }
            else if (activeIndex == 2)
            {
                currentYear++;
                maxWeek = GetMaxWeekNumber(currentYear);
            }
            var today = DateTime.Today;
            if (currentWeek != GetWeekNumber(today) || currentMonth != today.Month || currentYear != today.Year)
            {
                isNotCurrentDay = true;
            }
            else
            {
                isNotCurrentDay = false;
            }
        }

        private int GetWeekNumber(DateTime date)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }

        private int GetMaxWeekNumber(int year)
        {
            if (year < 1 || year > 9999)
                throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 1 and 9999.");

            var lastDay = new DateTime(year, 12, 31);
            return GetWeekNumber(lastDay);
        }

        private void ShowToday()
        {
            var today = DateTime.Today;
            currentYear = today.Year;
            currentMonth = today.Month;
            currentWeek = GetWeekNumber(today);
            isNotCurrentDay = false;
        }
    }
}
