using WebAPI.ApplicationData;
using WebAPI.DataClass;

namespace WebAPI.Sevices
{
	public class CategoriesService : ICategoriesService
	{
		public void CreateUpdate(Categories categories)
		{
			using(ApplicationDBContext context = new ApplicationDBContext())
			{
				context.Categories.Update(categories);
				context.SaveChanges();
			}
		}

		public Categories GetById(int id)
		{
			using (var context = new ApplicationDBContext())
			{
				return context.Categories.FirstOrDefault(x => x.Id == id);
			}
		}

		//public void Delete(int id)
		//{
		//	using(var context = new ApplicationDBContext())
		//	{
		//		context.Categories.Remove(GetById(id));
		//		context.SaveChanges();
		//	}
		//}

		public List<Categories> GetAll()
		{
			using( var context = new ApplicationDBContext())
			{
				return context.Categories.ToList();
			}
		}
	}
}
