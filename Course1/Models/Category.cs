namespace Course1.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
    
        public Category(string name)
        {
            Name = name;
        }

        public IEnumerable<Product> Products { get; set; }
    }
}
