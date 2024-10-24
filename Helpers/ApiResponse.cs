using Newtonsoft.Json;
namespace Maksab.Helpers
{
    public class ApiResponse
    {
        public bool Success { get; private set; }

        public int Code { get; private set; }

        public string Description { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object Data { get; private set; }

        // This line below added to hide the value when it is "NULL"
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        // This line below added to hide the value when it is "NULL"
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        // Dictionary to handle errors and convert them to friendly-readable way.
        public Dictionary<string, string[]> Errors { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object Metadata { get; private set; }
        public ApiResponse(int code, string description)
        {
            Success = code >= 2000 && code <= 2999 ? true : false;
            Code = code;
            Description = description;
        }

        public ApiResponse(int code, string description, string message)
            : this(code, description)
        {
            Message = message;
        }

        public ApiResponse(int code, string description, Dictionary<string, string[]> errors)
            : this(code, description)
        {
            if (errors != null)
            {
                Errors = errors;
            }
        }

        public ApiResponse(int code, string description, object data) : this(code, description)
        {
            Data = data;
        }
        public ApiResponse(int code, string description, object data, object metadata) : this(code, description)
        {
            Data = data;
            Metadata = metadata;
        }
    }
}
