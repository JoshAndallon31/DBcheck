namespace CampusFeedApi.Dto
{
    public class CampusFeedDto
    {
        public string CampusFeedId { get; set; } = String.Empty;
        public string Content { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public int Like { get; set; } = 0;
        public int Dislike { get; set; } = 0;
    }
}
