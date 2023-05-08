using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebAPI.DataClass
{
	public class IngredientenDTO 
	{
		public IngredientenDTO()
		{
			
		}

		//public IngredientenDTO(Ingredienten ingredienten)
		//{
		//	//this.Unit = ingredienten.Unit;
		//	////this.Id = ingredienten.Id;
		//	//this.Name = ingredienten.Name;
		//	//this.Quantity = ingredienten.Quantity;
		//}



		public string Name { get; set; }

		public int Quantity { get; set; }

		[AllowNull]
		public string Unit { get; set; }

	}
}