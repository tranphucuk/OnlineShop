using OnlineShop.Model.Model;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;
            postCategory.ID = postCategoryViewModel.ID;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
            postCategory.Name = postCategoryViewModel.Name;
            postCategory.ParentID = postCategoryViewModel.ParentID;
            postCategory.Status = postCategoryViewModel.Status;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
        }

        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            post.Alias = postViewModel.Alias;//
            post.CategoryID = postViewModel.CategoryID;//
            post.Content = postViewModel.Content;//
            post.CreatedBy = postViewModel.CreatedBy;//
            post.CreatedDate = postViewModel.CreatedDate;//
            post.Description = postViewModel.Description;//
            post.DisplayOrder = postViewModel.DisplayOrder;
            post.HomeFlag = postViewModel.HomeFlag;//
            post.HotFlag = postViewModel.HotFlag;
            post.ID = postViewModel.ID;//
            post.Image = postViewModel.Image;//
            post.MetaDescription = postViewModel.MetaDescription;//
            post.MetaKeyword = postViewModel.MetaKeyword;//
            post.Name = postViewModel.Name;//
            post.Status = postViewModel.Status;//
            post.UpdatedBy = postViewModel.UpdatedBy;//
            post.UpdatedDate = postViewModel.UpdatedDate;//
            post.ViewCount = postViewModel.ViewCount;//
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Image = productCategoryViewModel.Image;
            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.ParentID = productCategoryViewModel.ParentID;
            productCategory.Status = productCategoryViewModel.Status;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
        }
    }
}