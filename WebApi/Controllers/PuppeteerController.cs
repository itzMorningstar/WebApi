
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PuppeteerSharp;
using System.Text.RegularExpressions;
using WebApi.Models.Puppeteer;
using WebApi.Models.Response;

namespace WebApi.Controllers
{
    [Route("puppeteer")]
    [ApiController]
    public class PuppeteerController : ControllerBase
    {
        #region Constructor

        public PuppeteerController()
        {
            
        }

        #endregion

        #region PrivateMethods
        private async Task<PuppeteerJsonResponse> ExtractUseFullData(IPage page,string? className)
        {
            // Extract all images on the page
            var images = await page.QuerySelectorAllAsync("img");
            var imageList = new List<string>();
            foreach (var image in images)
            {
                var src = await image.GetPropertyAsync("src");
                imageList.Add(src.RemoteObject.Value.ToString());
            }

            // Extract all videos on the page
            var videos = await page.QuerySelectorAllAsync("video");
            var videoList = new List<string>();
            foreach (var video in videos)
            {
                var src = await video.GetPropertyAsync("src");
                videoList.Add(src.ToString());
            }

            // Extract all main headings on the page (h1, h2, h3, h4, h5, h6)
            var headings = await page.QuerySelectorAllAsync("h1, h2, h3, h4, h5, h6");
            var headingList = new List<string>();
            foreach (var heading in headings)
            {
                var text = await heading.GetPropertyAsync("textContent");
                headingList.Add(text.RemoteObject.Value.ToString());
            }

            // Extract all anchor tags on the page
            var anchors = await page.QuerySelectorAllAsync("a");
            var anchorList = new List<string>();
            foreach (var anchor in anchors)
            {
                var href = await anchor.GetPropertyAsync("href");
                anchorList.Add(href.RemoteObject.Value.ToString());
            }

            var content = await page.GetContentAsync();


            // Extract all paragraphs on the page
            var paragraphs = new List<string>();
            var paragraphMatches = Regex.Matches(content, @"<p[^>]*>(.*?)</p>", RegexOptions.Singleline);
            foreach (Match paragraphMatch in paragraphMatches)
            {
                var text = paragraphMatch.Groups[1].Value;
                paragraphs.Add(text);
            }

            var divs = new List<string>();
            if (string.IsNullOrEmpty(className))
            {
            // Extract all divs with a specific class
            var divMatches = Regex.Matches(content, $@"<div\s+class=""{className}"">(.*?)</div>", RegexOptions.Singleline);
            foreach (Match divMatch in divMatches)
            {
                var text = divMatch.Groups[1].Value;
                divs.Add(text);
            }

            }

            // Create a WebPageData object and populate it with the extracted data
            var webPageData = new PuppeteerJsonResponse
            {
                Images = imageList,
                Videos = videoList,
                Headings = headingList,
                Anchors = anchorList,
                Paragraphs = paragraphs,
                Divs = divs
            };

            return webPageData;

        }
        #endregion

        #region PublicActionMethods

        [HttpGet]
        [Route("fetchhtml")]
        public async Task<IActionResult> FetchHtml(string url,string? className)
        {
            ResponseModel response = new ResponseModel();
            try
            {
            if (Uri.IsWellFormedUriString(url,UriKind.RelativeOrAbsolute))
            {

                var fetcher = new BrowserFetcher();
                await fetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    Timeout = 60000
                });
                var page = await browser.NewPageAsync();
                await page.GoToAsync(url);
                
                var content = await ExtractUseFullData(page, className);

                var responseObj = new List<KeyValuePair<string, string>>();
                
                //var title = await page.GetTitleAsync();
                //var cookieParams = await page.GetCookiesAsync();

                await browser.CloseAsync();

                //responseObj.Add(new KeyValuePair<string, string>("Title", title));
                //responseObj.Add(new KeyValuePair<string, string>("content", content));
                //responseObj.Add(new KeyValuePair<string, string>("cookieParams", cookieParams.ToString() ?? "No cookies Prams Found"));
                {
                    response.ResponseStatus = Enums.ResponseStatus.Success;
                    response.Message = "Successfully Fetched the Data";
                    response.Data = content;
                };
                return Ok(response);
            }

            response.Message = "The provided URL is not a valid url please try a different one or contact the administrator";
            response.ResponseStatus = Enums.ResponseStatus.Error;
            return BadRequest(response);

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.ResponseStatus = Enums.ResponseStatus.Error;
                return BadRequest(response);
            }

        }

        #endregion
    }
}
