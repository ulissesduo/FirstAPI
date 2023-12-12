using FirstAPI.Models.Domain;
using FirstAPI.Models.DTO;

namespace FirstAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAll();

        Task<Products> GetById(Guid id);

        Task<bool> DeleteProduct(Guid id);

        bool Save();
        ProductDTO CreateProduct(AddProductRequestDto addRegionRequest);
        ProductDTO UpdateProduct(Guid regionId, AddProductRequestDto addProductRequest);
        ProductDTO UpdateSpecificAttProduct(Guid regionId, AddProductRequestDto addProductRequest);

    }
}
