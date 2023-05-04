namespace WebApi.Models.Puppeteer
{
    public class PuppeteerJsonResponse
    {
        public List<string> Images { get; set; }
        public List<string> Videos { get; set; }
        public List<string> Headings { get; set; }
        public List<string> Anchors { get; set; }
        public List<string> Paragraphs { get; set; }
        public List<string> Divs { get; set; }
    }
}
