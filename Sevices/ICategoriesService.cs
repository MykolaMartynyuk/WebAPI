using WebAPI.DataClass;

namespace WebAPI.Sevices
{
	public interface ICategoriesService
	{
		public void CreateUpdate(Categories categories);

		public Categories GetById(int id);

		public List<Categories> GetAll();
	}
}
