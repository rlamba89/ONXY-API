namespace Onyx.Contracts.Data.Entities
{
    public class Product : BaseEntity
    {
        public Product(string productName, string productColour, string productCategory)
        {
            if (string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName)) throw new ArgumentNullException(nameof(productName));
            if (string.IsNullOrEmpty(productColour) || string.IsNullOrWhiteSpace(productColour)) throw new ArgumentNullException(nameof(productColour));
            if (string.IsNullOrEmpty(productCategory) || string.IsNullOrWhiteSpace(productCategory)) throw new ArgumentNullException(nameof(productCategory));

            ProductName = productName;
            ProductColour = productColour;
            ProductCategory = productCategory;
        }

        public string ProductName { get; private set; }
        public string ProductColour { get; private set; }
        public string ProductCategory { get; private set; }
    }
}