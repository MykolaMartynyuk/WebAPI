using WebAPI.DataClass;

namespace WebAPI.Sevices
{
	public interface IRecipesService
	{
		public void CreateUpdate(Recipes recipes);

		public void Delete(int id);

		public List<Recipes> GetAll();

		public Recipes GetById(int id);
	}
}
