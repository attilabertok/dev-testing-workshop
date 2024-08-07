using System.ComponentModel.DataAnnotations;

namespace TestingControllersSample.Controllers
{
    public class NewSessionModel
    {
        [Required]
        public string SessionName { get; set; }
    }
}
