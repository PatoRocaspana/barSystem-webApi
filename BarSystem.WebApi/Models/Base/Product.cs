namespace BarSystem.WebApi.Models.Base
{
    public abstract class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
