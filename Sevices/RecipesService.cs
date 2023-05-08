using WebAPI.ApplicationData;
using WebAPI.DataClass;

namespace WebAPI.Sevices
{
	public class RecipesService 
	{
		public void CreateUpdate(Recipes recipes)
		{
			using (var contex = new ApplicationDBContext())
			{
				contex.Recipes.Add(recipes);
				contex.SaveChanges();
			}
		}

		//public void Delete(int id)
		//{
		//	using (var context = new ApplicationDBContext())
		//	{
		//		context.Recipes.Remove(GetById(id));
		//		context.SaveChanges();
		//	}
		//}

		public List<Recipes> GetAll()
		{
			using (var contex = new ApplicationDBContext())
			{
				return contex.Recipes.ToList();
			}
		}

		//public Recipes GetById(int id)
		//{
		//	using (var context = new ApplicationDBContext())
		//	{
		//		return context.Recipes.FirstOrDefault(x => x.Id == id);
		//	}
		//}
	}
}
