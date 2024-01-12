using my_bookss.Data.Models;

namespace my_bookss.Data.ViewModels
{
    public class CustomActionResultVM
    {
        //return exception or data
        public Exception Exception { get; set; }
        public Publisher Publisher { get; set; }
    }
}
