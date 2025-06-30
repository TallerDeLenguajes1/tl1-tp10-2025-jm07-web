namespace MiWebApi
{
    public class Facto
    {
        public string? Fact { get; set; }
        public int Length { get; set; }
    }

    public class CatFactResponse
    {
        public List<Facto>? Data { get; set; }
    }
}