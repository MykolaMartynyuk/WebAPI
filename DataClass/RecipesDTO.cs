using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataClass
{
	public class RecipesDTO
	{
		public RecipesDTO(Recipes recipes)
		{
		}

		public RecipesDTO()
		{

		}

		public string Title { get; set; }

		public int Time { get; set; }

		public Difficulty Difficulty { get; set; }

		[ForeignKey("CategoriesId")]
		public int CategoriesId { get; set; }

		[NotMapped]
		public ICollection<IngredientenDTO> IngredientensDTO { get; set; }



	}
}
