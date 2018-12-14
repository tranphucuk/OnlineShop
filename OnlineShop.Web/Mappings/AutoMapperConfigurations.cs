﻿using AutoMapper;
using OnlineShop.Model.Model;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Web.Mappings
{
    public class AutoMapperConfigurations
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();

            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<ProductTag, ProductTagViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<Footer, FooterViewModel>();
            Mapper.CreateMap<Slide, SlideViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();
            Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();
            Mapper.CreateMap<Feedback, FeedbackViewModel>();
            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}