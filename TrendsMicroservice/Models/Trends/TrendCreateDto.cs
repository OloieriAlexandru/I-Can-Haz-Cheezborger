namespace Models.Trends
{
    public class TrendCreateDto : UserInfoModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string DateTime { get; set; }
    }
}
