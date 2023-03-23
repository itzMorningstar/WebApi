using Newtonsoft.Json;
using WebApi.Enums;

namespace WebApi.Models.Response
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            Message = String.Empty;
            ErrorsList = new List<KeyValuePair<string, string>>();
        }
        public ResponseStatus ResponseStatus { get; set; }
        public List<KeyValuePair<string, string>> ErrorsList { get; set; }
        public MetaData MetaData { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MetaData
    {
        public int TotalPages { get; set; }
        public string NextPage { get; set; }
        public string PrevPage { get; set; }
        public int Limit { get; set; }
        public int CurrentPage { get; set; }
    }
}
