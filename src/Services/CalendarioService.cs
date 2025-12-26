namespace Bmp.Services
{
    public class CalendarioService
    {
        public bool EhDiaUtil(DateTime data)
        {
            return data.DayOfWeek != DayOfWeek.Saturday &&
                   data.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
