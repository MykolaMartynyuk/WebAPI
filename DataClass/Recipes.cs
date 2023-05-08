using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataClass
{
	public class Recipes
	{
		public Recipes()
		{
			
		}

		public Recipes(RecipesDTO recipesDTO)
		{
			this.RecipesId = 0;
			this.Difficulty = recipesDTO.Difficulty;
			this.Title = recipesDTO.Title;
			this.Time = recipesDTO.Time;
			this.CategoriesId = recipesDTO.CategoriesId;
			this.Ingredienten = new List<Ingredienten>();
			foreach( var ingredien in recipesDTO.IngredientensDTO)
			{
				this.Ingredienten.Add(new Ingredienten(ingredien, RecipesId));
			}
		}

		public string Title { get; set; }

		public int Time { get; set; }

		public Difficulty Difficulty { get; set; }

		[ForeignKey("CategoriesId")]
		public int CategoriesId { get; set; }

		[Key]
		public int RecipesId { get; set; }

		public ICollection<Ingredienten> Ingredienten { get; set; }


	}
}
