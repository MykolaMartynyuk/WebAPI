using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebAPI.DataClass
{
	public class Ingredienten : IngredientenDTO
	{
		public Ingredienten(IngredientenDTO ingredientenDTO, int fk)
		{
			this.Unit = ingredientenDTO.Unit;
			this.Quantity = ingredientenDTO.Quantity;
			this.Name = ingredientenDTO.Name;
			this.RecipesFK = fk;
		}

		public Ingredienten()
		{

		}

		[Key]
		public int Id { get; set; }

		[NotNull]
		
		public int RecipesFK { get; set; }

		//[NotMapped
		[ForeignKey("RecipesFK")]
		public Recipes Recipes { get; set; }
	}
}
