using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.ActionFilters
{
    public class ValidationFiltersAttribute : IAsyncActionFilter
    {
        readonly ApplicationDbContext _dbContext;
        public ValidationFiltersAttribute(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value != null &&
            _dbContext.Model.FindEntityType(p.Value.GetType()) != null);

            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Data object is null");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
            var result = await next();
        }
    }
}
