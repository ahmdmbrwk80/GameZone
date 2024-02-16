using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel
    {
        //[MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        //[MaxLength(2500)]
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        
        public IEnumerable<SelectListItem> categories { get; set; }= Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supported Device")]
        public List<int> selectedDevices { get; set; }=new List<int>();
        public IEnumerable<SelectListItem> devices { get; set; } = Enumerable.Empty<SelectListItem>();
        public string Description { get; set; } = string.Empty;

        //[MaxLength(500)]
        public IFormFile Cover { get; set; } = default!;

    }
}
