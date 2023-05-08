using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ApplicationData;
using WebAPI.DataClass;

namespace WebAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ApplicationDBContext _context;

		public CategoriesController(ApplicationDBContext context)
		{
			_context = context;
		}

		[Authorize]
		[HttpGet("list")]
		public ActionResult<IEnumerable<Categories>> GetAllCategories()
		{
			return _context.Categories.ToList();
		}

		[Authorize]
		[HttpGet("{id}")]
		public ActionResult<Categories> GetCategories(int id)
		{
			Categories categories = _context.Categories.Find(id);

			if (categories == null)
			{
				return NotFound();
			}

			return categories;
		}

		[Authorize(Policy = "AdminOnly")]
		[HttpPost("")]
		public ActionResult<Categories> CreateCategories(Categories categories)
		{
			_context.Categories.Add(categories);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetCategories), new { id = categories.Id }, categories);
		}
	}
}
