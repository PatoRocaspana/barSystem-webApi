using BarSystem.Models.Base;

namespace BarSystem.WebApi.Models.Base
{
    public abstract class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
