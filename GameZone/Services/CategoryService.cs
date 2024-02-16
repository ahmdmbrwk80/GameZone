using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext _context;
        public CategoryService( ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetDevicesSelectListItems()
        {
            return _context.Devices.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text).AsNoTracking().ToList();
            
        }

        public IEnumerable<SelectListItem> GetSelectListItems( )
        {
            return _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .OrderBy(c => c.Text).AsNoTracking().ToList();

           
        }
    }
}
