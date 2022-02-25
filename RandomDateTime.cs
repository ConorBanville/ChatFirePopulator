namespace ChatFirePoupulator
{
    public class RandomDateTime
    {
        Random rnd = new Random();
        public string Next()
        {
            long time = Int64.Parse(DateTimeOffset.Now.ToUnixTimeSeconds() + "") - rnd.Next(1000,1000000);
            return time+"";
        }
    }
}