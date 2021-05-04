namespace AutoWorkshop.Specs.Stateless.UI
{
    public class ToolbarButton
    {
        public ToolbarButton(string url, string altText)
        {
            Url = url;
            AltText = altText;
        }

        public string Url { get; }

        public string AltText { get; }
    }
}