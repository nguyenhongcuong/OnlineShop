using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using OnlineShop.Service;

namespace OnlineShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockPostCategoryRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> _postCategories;
        [TestInitialize]
        public void Initialize()
        {
            _mockPostCategoryRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockPostCategoryRepository.Object, _mockUnitOfWork.Object);
            _postCategories = new List<PostCategory>
            {
                new PostCategory
                {
                    Id = 1,
                    Name = "Danh mục 1",
                    Status = true
                },
                 new PostCategory
                {
                    Id = 2,
                    Name = "Danh mục 2",
                    Status = false
                },
                  new PostCategory
                {
                    Id = 3,
                    Name = "Danh mục 3",
                    Status = true
                },
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            // Setup Method
            _mockPostCategoryRepository.Setup(x => x.GetAll(null)).Returns(_postCategories);


            // Call Action
            var result = _postCategoryService.GetAll() as List<PostCategory>;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory
            {
                Name = "Post Category Test",
                Alias = "post-category-test",
                Status = true
            };

            _mockPostCategoryRepository.Setup(x => x.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.Id = 1;
                return p;
            });
            _postCategoryService.Add(postCategory);
            Assert.AreEqual(1, postCategory.Id);
        }
    }
}
