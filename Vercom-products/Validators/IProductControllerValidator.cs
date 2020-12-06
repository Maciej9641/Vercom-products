namespace Vercom_products.Validators
{
    public interface IProductControllerValidator
    {
        bool ValidateIfProductCategoryExists(string categoryName);
    }
}
