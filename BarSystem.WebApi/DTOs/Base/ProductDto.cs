namespace BarSystem.WebApi.DTOs.Base
{
    public abstract class ProductDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
