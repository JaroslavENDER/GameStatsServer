namespace GameStatsServer.Services
{
    public class NormalizeReportsCount : INormalizeReportsCount
    {
        public int Normalize(int? count)
        {
            if (!count.HasValue) return 5;
            if (count <  0) return 0;
            if (count > 50) return 50;
            return count.Value;
        }
    }
}
