using System.ComponentModel.DataAnnotations;

namespace WebAPI.DataClass
{
	public class Categories
	{
		public Categories()
		{

		}

		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

	}
}
