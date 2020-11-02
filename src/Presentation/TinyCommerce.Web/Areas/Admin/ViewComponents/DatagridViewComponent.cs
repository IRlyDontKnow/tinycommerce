using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyCommerce.Web.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "Datagrid")]
    public class DatagridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
