using System.Collections.Generic;
using System.Linq;
using OnlineShop.Common;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyword);
        IEnumerable<Product> GetProductsByCategoryPaging(int categoryId , int page , int pageSize , out int totalRow);
        IEnumerable<Product> GetLastest(int top = 3);
        IEnumerable<Product> GetHotProduct(int top = 3);
        Product GetById(int? id);
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int? id);
        void Save();
    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository , IProductTagRepository productTagRepository , ITagRepository tagRepository , IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productTagRepository = productTagRepository;
            _tagRepository = tagRepository;
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

        public IEnumerable<Product> GetProductsByCategoryPaging(int categoryId , int page , int pageSize , out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.ProductCategoryId == categoryId).ToList();
            totalRow = query.Count;
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Product GetById(int? id)
        {
            return _productRepository.GetSingleById(id.GetValueOrDefault());
        }

        public Product Add(Product product)
        {
            var productAddResult = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                var tags = product.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    if (_tagRepository.Count(x => x.Id == tagId) == 0)
                    {
                        Tag tagAdd = new Tag
                        {
                            Name = tag ,
                            Id = tagId ,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tagAdd);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        ProductId = product.Id ,
                        TagId = tagId
                    };

                    _productTagRepository.Add(productTag);
                }
            }
            return productAddResult;
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                var tags = product.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    if (_tagRepository.Count(x => x.Id == tagId) == 0)
                    {
                        Tag tagAdd = new Tag
                        {
                            Name = tag ,
                            Id = tagId ,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tagAdd);
                    }

                    _productTagRepository.DeleteMulti(x => x.ProductId == product.Id);
                    ProductTag productTag = new ProductTag
                    {
                        ProductId = product.Id ,
                        TagId = tagId
                    };

                    _productTagRepository.Add(productTag);
                }

            }
            _unitOfWork.Commit();
        }

        public Product Delete(int? id)
        {
            return _productRepository.Delete(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetLastest(int top = 3)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHotProduct(int top = 3)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);

        }
    }
}
