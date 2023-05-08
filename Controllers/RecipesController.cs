using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ApplicationData;
using WebAPI.DataClass;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("[controller]")]	
	[ApiController]
	public class RecipesController : Controller
	{
		private readonly ApplicationDBContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		public RecipesController(ApplicationDBContext context, UserManager<IdentityUser> userMnager)
		{
			_context = context;
			_userManager = userMnager;
		}

		[Authorize]
		[HttpGet("recipes")]
		public  ActionResult<IEnumerable<RecipesListViewDTO>> GetAllRecipes()
		{
			return _context.Recipes.ToList().Select(x => new RecipesListViewDTO()
			{
				CategoriesId = x.CategoriesId,
				RecipesId = x.RecipesId,
				Difficulty = x.Difficulty,
				Time = x.Time,
				Title = x.Title

			}).ToList();
		}

		[Authorize]
		[HttpGet("search")]
		public ActionResult<IEnumerable<RecipesListViewDTO>> Search(string? title, int? time, Difficulty? difficulty, string? categories)
		{
			return _context.Recipes.ToList()
				.Where(x=> (difficulty == null) || (x.Difficulty) <= difficulty)
				.Where(x => (categories == null) || _context.Categories.Find(x.CategoriesId)?.Name == categories)
				.Where(x => (title == null) || x.Title == title)
				.Where(x => (time == null) || x.Time <= time)
				.Select(x => new RecipesListViewDTO()
			{
				CategoriesId = x.CategoriesId,
				RecipesId = x.RecipesId,
				Difficulty = x.Difficulty,
				Time = x.Time,
				Title = x.Title
			}).ToList();
		}

		[Authorize]
		[HttpGet("{id}")]
		public ActionResult<Recipes> GetRecipes(int id)
		{
			Recipes recipes = _context.Recipes.Find(id);

			if (recipes == null)
			{
				return NotFound();
			}

			recipes.Ingredienten = new List<Ingredienten>(_context.Ingredientens.Where(x => x.RecipesFK == recipes.RecipesId).ToList());

			return recipes;
				
		}

		[Authorize(Policy  = "AdminOnly")]
		[HttpDelete("{id}")]
		public IActionResult DeleteRecipes(int id)
		{
			Recipes recipesToDelete = _context.Recipes.Find(id);

			if(recipesToDelete == null)
			{
				return NotFound();
			}

			_context.Recipes.Remove(recipesToDelete);
			_context.SaveChanges();

			return NoContent();
		}

		[Authorize( Policy = "AdminOnly")]
		[HttpPost("")]
		public ActionResult<Recipes> Create(RecipesDTO recipesDTO)
		{
			var recipes = new Recipes(recipesDTO);

			if(((int)recipesDTO.Difficulty) > 3 || (_context.Categories.Find(recipesDTO.CategoriesId) == null))
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Recipe's creation failed! Please check user details and try again." });
			}

			_context.Recipes.Add(recipes);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetRecipes), new {id = recipes.RecipesId}, recipes);
		}
	}
}
