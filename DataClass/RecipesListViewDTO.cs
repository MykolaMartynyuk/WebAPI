using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DataClass
{
	public class RecipesListViewDTO
	{
		public string Title { get; set; }

		public int Time { get; set; }

		public Difficulty Difficulty { get; set; }

		[ForeignKey("CategoriesId")]
		public int CategoriesId { get; set; }

		[Key]
		public int RecipesId { get; set; }
	}
}