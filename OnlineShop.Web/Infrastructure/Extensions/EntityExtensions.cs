﻿using AutoMapper;
using OnlineShop.Model.Model;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.Alias = productViewModel.Alias;//
            product.CreatedBy = productViewModel.CreatedBy;//
            product.CreatedDate = productViewModel.CreatedDate;//
            product.Description = productViewModel.Description;//
            product.Content = productViewModel.Content;//
            product.DisplayOrder = productViewModel.DisplayOrder;//
            product.HomeFlag = productViewModel.HomeFlag;//
            product.HotFlag = productViewModel.HotFlag;//
            product.ID = productViewModel.ID;//
            product.Image = productViewModel.Image;//
            product.MoreImages = productViewModel.MoreImages;
            product.MetaDescription = productViewModel.MetaDescription;//
            product.MetaKeyword = productViewModel.MetaKeyword;//
            product.Tags = productViewModel.Tags;
            product.Name = productViewModel.Name;//
            product.CategoryID = productViewModel.CategoryID;//
            product.Price = productViewModel.Price;//
            product.Quantity = productViewModel.Quantity;
            product.PromotionPrice = productViewModel.PromotionPrice;//
            product.Warranty = productViewModel.Warranty;//
            product.Status = productViewModel.Status;//
            product.UpdatedBy = productViewModel.UpdatedBy;//
            product.UpdatedDate = productViewModel.UpdatedDate;//
        }

        public static void UpdatePage(this Page page, PageViewModel pageViewModel)
        {
            page.ID = pageViewModel.ID;
            page.Name = pageViewModel.Name;
            page.Alias = pageViewModel.Alias;
            page.Content = pageViewModel.Content;
            page.MetaKeyword = pageViewModel.MetaKeyword;
            page.MetaDescription = pageViewModel.MetaDescription;
            page.CreatedDate = pageViewModel.CreatedDate;
            page.UpdatedDate = pageViewModel.UpdatedDate;
            page.CreatedBy = pageViewModel.CreatedBy;
            page.UpdatedBy = pageViewModel.UpdatedBy;
            page.Status = pageViewModel.Status;
        }

        public static void UpdateContactDetails(this ContactDetail contact, ContactDetailViewModel contactViewModel)
        {
            contact.ID = contactViewModel.ID;
            contact.Name = contactViewModel.Name;
            contact.Phone = contactViewModel.Phone;
            contact.Address = contactViewModel.Address;
            contact.Email = contactViewModel.Email;
            contact.Status = contactViewModel.Status;
            contact.Other = contactViewModel.Other;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackViewModel)
        {
            feedback.ID = feedbackViewModel.ID;
            feedback.Name = feedbackViewModel.Name;
            feedback.Email = feedbackViewModel.Email;
            feedback.Content = feedbackViewModel.Content;
            feedback.CreatedDate = feedbackViewModel.CreatedDate == null ? DateTime.Now : feedbackViewModel.CreatedDate;
            feedback.Status = feedbackViewModel.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderViewModel)
        {
            order.ID = orderViewModel.ID;
            order.CustomerName = orderViewModel.CustomerName;
            order.CustomerAddress = orderViewModel.CustomerAddress;
            order.CustomerEmail = orderViewModel.CustomerEmail;
            order.CustomerMobile = orderViewModel.CustomerMobile;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderViewModel.CreatedBy;
            order.CustomerMessage = orderViewModel.CustomerMessage;
            order.Status = orderViewModel.Status;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.PaymentStatus = orderViewModel.PaymentStatus;
            order.CreatedBy = orderViewModel.CreatedBy;
            order.OrderDetails = Mapper.Map<IEnumerable<OrderDetailViewModel>, IEnumerable<OrderDetail>>(orderViewModel.OrderDetails);
        }

        public static void UpdateAppGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupVm)
        {
            appGroup.Description = appGroupVm.Description;
            appGroup.ID = appGroupVm.ID;
            appGroup.Name = appGroupVm.Name;
        }

        public static void UpdateRoles(this ApplicationRole appRole, ApplicationRoleViewModel appRoleVm, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleVm.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleVm.Name;
            appRole.Description = appRoleVm.Description;
        }

        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserVm)
        {
            appUser.Id = appUserVm.Id;
            appUser.Fullname = appUserVm.Fullname;
            appUser.UserName = appUserVm.UserName;
            appUser.Email = appUserVm.Email;
            appUser.PhoneNumber = appUserVm.PhoneNumber;
            appUser.Birthday = appUserVm.Birthday;
            appUser.Address = appUserVm.Address;
        }

        public static void UpdateEmail(this Email email, EmailViewModel emailVm)
        {
            email.ID = emailVm.ID;
            email.EmailAddress = emailVm.EmailAddress;
            email.CreatedDate = emailVm.CreatedDate;
            email.Status = emailVm.Status;
        }

        public static void UpdateEmailManager(this EmailManager emailManager, EmailManagerViewModel emailManagerVm)
        {
            emailManager.ID = emailManagerVm.ID;
            emailManager.EmailUser = emailManagerVm.EmailUser;
            emailManager.MailTitle = emailManagerVm.MailTitle;
            emailManager.MailContent = emailManagerVm.MailContent;
            emailManager.SendDate = DateTime.Now;
            emailManager.RecipientCount = emailManagerVm.RecipientEmails.Count();
        }
    }
}