/*
 * View components are similar to partial views, but they're much more powerful.
 * View components don't use model binding, and only depend on the data provided when calling into it
 * This View components creates the dynamic navigation menu. 
 * Because menu will be reusable across the app, best practice is not to repeat code
 */

using Microsoft.AspNetCore.Mvc;
using shoppersdata;
using shoppersweb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shoppersweb.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await ParentsOrChildrenCategories(null);
            return View(items);
        }
        public async Task<List<Category>> ParentsOrChildrenCategories(int? parentId)
        {
            DataHelper helper = new DataHelper();
            var result = helper.Categories();
            List<Category> categories = new List<Category>();
            var awaitResult = await result;

            if (parentId == null)
                categories = awaitResult.Where(p => !p.ParentCategoryId.HasValue).ToList();
            else
                categories = awaitResult.Where(p => p.ParentCategoryId.HasValue && p.ParentCategoryId == parentId).ToList();

            return categories;
        }
    }
}
