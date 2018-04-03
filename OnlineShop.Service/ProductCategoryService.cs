using System.Collections.Generic;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        ProductCategory Delete(ProductCategory productCategory);
        ProductCategory Delete(int? id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAllByParentId(int? parentId);
        IEnumerable<ProductCategory> GetAll(string keywork);
        ProductCategory GetById(int? id);
        void Save();
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public ProductCategory Add(ProductCategory productCategory)
        {
            return _productCategoryRepository.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            _productCategoryRepository.Update(productCategory);
        }

        public ProductCategory Delete(ProductCategory productCategory)
        {
            return _productCategoryRepository.Delete(productCategory);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int? parentId)
        {
            return _productCategoryRepository.GetMulti(x => x.ParentId == parentId & x.Status);
        }

        public ProductCategory GetById(int? id)
        {
            return _productCategoryRepository.GetSingleById(id.GetValueOrDefault());
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            return string.IsNullOrEmpty(keyword) ?
                _productCategoryRepository.GetAll() :
                _productCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
        }

        public ProductCategory Delete(int? id)
        {
            return _productCategoryRepository.Delete(id);
        }
    }
}
