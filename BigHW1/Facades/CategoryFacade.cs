using BigHW1.Entities;
using BigHW1.Factories;

namespace BigHW1.Facades
{
    public class CategoryFacade
    {
        public List<Category> _categories { get; set; }

        public CategoryFacade()
        {
            _categories = new List<Category>();
        }

        public void AddCategory(Category category)
        {    
            _categories.Add(category);
        }

        public void EditCategory(Guid categoryId, string newName)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                throw new InvalidOperationException("Категория с таким ID не найдена");
            }
            category.Name = newName;
            
        }

        public void DeleteCategory(Guid categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                throw new InvalidOperationException("Категория с таким ID не найдена");
            }
            _categories.Remove(category);
            
        }

    }

}
