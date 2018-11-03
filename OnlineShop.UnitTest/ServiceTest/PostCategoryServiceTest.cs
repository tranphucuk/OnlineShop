using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory(){Name = "DM1",Alias="DM1",Status = true},
                new PostCategory(){Name = "DM2",Alias="DM2",Status = true},
                new PostCategory(){Name = "DM3",Alias="DM3",Status = true},
                new PostCategory(){Name = "DM4",Alias="DM4",Status = true},
            };
        }

        [TestMethod]
        public void Test_CategoryService_GetAll()
        {
            // setup method
            var rs1 = _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);

            // call action
            var rs = _postCategoryService.GetAll() as List<PostCategory>;

            // compare
            Assert.IsNotNull(rs);
            Assert.AreEqual(4, rs.Count);
        }

        [TestMethod]
        public void Test_Caegory_Add()
        {
            PostCategory postCategory = new PostCategory();
            int id = 1;
            postCategory.Name = "test";
            postCategory.Alias = "test";
            postCategory.Status = true;

            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.ID = id;
                return p;
            });

            var result = _postCategoryService.Add(postCategory);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
