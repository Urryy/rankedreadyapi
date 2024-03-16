using RankedReadyApi.Common.Models.Transaction;
using System.Globalization;

namespace RankedReady.DataAccess.Extensions;

public static class DateTimeExtension
{
    public static DatePurchaseModel GetPurchaseModel(this DateTime date)
    {
        var dtPurchase = new DatePurchaseModel();

        dtPurchase.Date = date.ToString();
        dtPurchase.MonthName = date.ToString("MMM", new CultureInfo("en-US")); // Получение месяца в текстовом формате.
        dtPurchase.Time = date.ToString("HH:mm"); // Получение времени.
        dtPurchase.DayOfMonth = date.Day.ToString(); // Получение дня.

        return dtPurchase;
    }
}
