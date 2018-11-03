using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IPostCategoryRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new PostCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = objRepository.GetAll();
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "Test Category";
            postCategory.Alias = "Test-Category";
            postCategory.Status = true;
            postCategory.Description = "Test-Category";
            postCategory.Image = "Test-Category";
            postCategory.ParentID = 1;
            postCategory.DisplayOrder = 1;
            postCategory.HomeFlag = true;

            var rs = objRepository.Add(postCategory);
            unitOfWork.Commit();

            Assert.IsNotNull(rs);
            Assert.AreEqual(3, rs.ID);
        }
    }
}
