namespace AutoWorkshop.Specs.Dto
{
    public class LinkInfo
    {
        public LinkInfo(string url, string altText)
        {
            Url = url;
            AltText = altText;
        }

        public string Url { get; }

        public string AltText { get; }
    }
}