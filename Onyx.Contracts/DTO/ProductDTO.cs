namespace Onyx.Contracts.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductColour { get; set; }
        public string ProductCategory { get; set; }
        public DateTime AddedOn { get; set; }
    }
}