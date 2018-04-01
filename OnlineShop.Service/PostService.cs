using System.Collections.Generic;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IPostService
    {
        void AddPost(Post post);
        void Update(Post post);
        void Delete(int? postId);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllPaging(int? postCategoryId, int? page, int? pageSize, out int totalRow);
        Post GetById(int? postId);
        IEnumerable<Post> GetAllByTagPaging(string tagId, int? page, int? pageSize, out int totalRow);
        void SaveChanges();
    }
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddPost(Post post)
        {
            _postRepository.Add(post);
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }

        public void Delete(int? postId)
        {
            _postRepository.Delete(postId);
        }

        public IEnumerable<Post> GetAllPaging(int? postCategoryId, int? page, int? pageSize, out int totalRow)
        {
            var query = _postRepository.GetMultiPaging(x => x.Status && x.PostCategoryId == postCategoryId.Value, out totalRow,
              page.GetValueOrDefault(), pageSize.GetValueOrDefault(), new string[] { "PostCategory" });
            return query;
        }

        public Post GetById(int? postId)
        {
            return _postRepository.GetSingleById(postId.GetValueOrDefault());
        }

        public IEnumerable<Post> GetAllByTagPaging(string tagId, int? page, int? pageSize, out int totalRow)
        {
            return _postRepository.GetAllByTagPaging(tagId,
                page.GetValueOrDefault(), pageSize.GetValueOrDefault(), out totalRow);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[]
            {
                "PostCategory"
            });
        }
    }
}
