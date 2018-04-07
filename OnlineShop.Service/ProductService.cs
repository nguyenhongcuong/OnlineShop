using System.Collections.Generic;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyword);
        Product GetById(int? id);
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int? id);
        void Save();
    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            return string.IsNullOrEmpty(keyword)
                ? _productRepository.GetAll()
                : _productRepository.GetMulti(
                    x =>
                        x.Name.ToUpper().Contains(keyword.ToUpper()) ||
                        x.Description.ToUpper().Contains(keyword.ToUpper()));
        }

        public Product GetById(int? id)
        {
            return _productRepository.GetSingleById(id.GetValueOrDefault());
        }

        public Product Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public void Update(Product product)
        {
           _productRepository.Update(product);
        }

        public Product Delete(int? id)
        {
            return _productRepository.Delete(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
