using Newtonsoft.Json;

namespace my_bookss.Data.ViewModels
{
    public class ErrorVM
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; } //where was this error thrown from

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
