using OnlineShop.Model.Models;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory ,
            PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.Id = postCategoryViewModel.Id;
            postCategory.Name = postCategoryViewModel.Name;
            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.ParentId = postCategoryViewModel.ParentId;
            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;

            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.Status = postCategoryViewModel.Status;

        }

        public static void UpdatePost(this Post post , PostViewModel postViewModel)
        {
            post.Id = postViewModel.Id;
            post.Name = postViewModel.Name;
            post.Alias = postViewModel.Alias;
            post.PostCategoryId = postViewModel.PostCategoryId.GetValueOrDefault();
            post.Image = postViewModel.Image;
            post.Description = postViewModel.Description;
            post.Content = postViewModel.Content;
            post.HomeFlag = postViewModel.HomeFlag;
            post.ViewCount = postViewModel.ViewCount;

            post.CreatedDate = postViewModel.CreatedDate;
            post.CreatedBy = postViewModel.CreatedBy;
            post.UpdatedDate = postViewModel.UpdatedDate;
            post.UpdatedBy = postViewModel.UpdatedBy;
            post.MetaKeyword = postViewModel.MetaKeyword;
            post.MetaDescription = postViewModel.MetaDescription;
            post.Status = postViewModel.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory ,
            ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.Id = productCategoryViewModel.Id;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.ParentId = productCategoryViewModel.ParentId;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.Image = productCategoryViewModel.Image;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;

            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;
            productCategory.MetaDescription = productCategoryViewModel.MetaDescripton;
            productCategory.Status = productCategoryViewModel.Status;
        }

        public static void UpdateProduct(this Product product , ProductViewModel productViewModel)
        {
            product.Id = productViewModel.Id;
            product.Name = productViewModel.Name;
            product.Alias = productViewModel.Alias;
            product.ProductCategoryId = productViewModel.ProductCategoryId.GetValueOrDefault();
            product.Image = productViewModel.Image;
            product.MoreImages = productViewModel.MoreImages;
            product.Price = productViewModel.Price;
            product.PromotionPrice = productViewModel.PromotionPrice;
            product.Warranty = productViewModel.Warranty;
            product.Description = productViewModel.Description;
            product.Content = productViewModel.Content;
            product.HomeFlag = productViewModel.HomeFlag;
            product.HotFlag = productViewModel.HotFlag;
            product.Tags = productViewModel.Tags;
            product.Quantity = productViewModel.Quantity;


            product.CreatedDate = productViewModel.CreatedDate;
            product.CreatedBy = productViewModel.CreatedBy;
            product.UpdatedDate = productViewModel.UpdatedDate;
            product.UpdatedBy = productViewModel.UpdatedBy;
            product.MetaKeyword = productViewModel.MetaKeyword;
            product.MetaDescription = productViewModel.MetaDescription;
            product.Status = productViewModel.Status;


        }

        public static void UpdateFeedback(this Feedback feedback , FeedbackViewModel feedbackViewModel)
        {
            feedback.Id = feedbackViewModel.Id;
            feedback.Name = feedbackViewModel.Name;
            feedback.Email = feedbackViewModel.Email;
            feedback.Message = feedbackViewModel.Message;
            feedback.CreatdDate = feedbackViewModel.CreatdDate;
            feedback.Status = feedbackViewModel.Status;
        }

    }
}