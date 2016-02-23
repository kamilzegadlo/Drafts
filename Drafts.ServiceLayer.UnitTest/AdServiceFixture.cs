using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drafts.ServiceLayer;
using NUnit.Framework;
using Rhino.Mocks;
using Drafts.ModelLayer;
using Drafts.DataAccessLayer;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Drafts.ServiceLayer.UnitTest
{
    [TestFixture]
    public class AdServiceFixture
    {
        private IAdService _adService;
        protected DbContextTransaction transaction;

        private void SetUpTransaction(Entities context )
        {
            transaction = context.Database.BeginTransaction();
        }

        private void RollBackTransaction(Entities context)
        {
            transaction.Rollback();
            transaction.Dispose();
            context.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            TestFactory.Clear();
        }

        private void PrepareSimpleSetOfMockedData()
        {
            Entities entities = MockRepository.GenerateMock<Entities>();
            IDbSet<Ad> MockSet = MockRepository.GenerateMock<IDbSet<Ad>, IQueryable>();
            IQueryable<Ad> MockData = new HashSet<Ad>{
                new Ad{Id=1,Title="tut1", Description="dut1",SellerID="1",Price=100000},
                new Ad{Id=2,Title="tut2", Description="dut2",SellerID="2",Price=110000},
                new Ad{Id=3,Title="tut3", Description="dut3",SellerID="2",Price=120000},
                new Ad{Id=4,Title="tut4", Description="dut4",SellerID="2",Price=130000},
                new Ad{Id=5,Title="tut5", Description="dut5",SellerID="2",Price=140000}
            }.AsQueryable();

            MockSet.Stub(m => m.Provider).Return(MockData.Provider);
            MockSet.Stub(m => m.Expression).Return(MockData.Expression);
            MockSet.Stub(m => m.ElementType).Return(MockData.ElementType);
            MockSet.Stub(m => m.GetEnumerator()).Return(MockData.GetEnumerator());

            entities.Stub(e => e.Ads).Repeat.Any().Return(MockSet);

            TestFactory.RegisterMock(entities);
        }

        [Test]
        public void UT_GetListOfAd()
        {
            PrepareSimpleSetOfMockedData();

            _adService = TestFactory.Get<IAdService>();

            IEnumerable<Advert> ads = _adService.GetListOfAd(1, 3);
            Assert.AreEqual(2, ads.Count());

            Assert.IsTrue(ads.Where(a => a.Id == 4).Any());
            Assert.IsTrue(ads.Where(a => a.Id == 5).Any());
        }

        [Test]
        public void UT_GetCountOfAd()
        {
            PrepareSimpleSetOfMockedData();

            _adService = TestFactory.Get<IAdService>();

            Assert.AreEqual(5, _adService.GetCountOfAd());
        }

        [Test]
        public void UT_GetFilteredSetOfAd()
        {
            PrepareSimpleSetOfMockedData();

            _adService = TestFactory.Get<IAdService>();

            AdSearchParams searchParams =new AdSearchParams();
            searchParams.PriceFrom=100000;
            searchParams.PriceTo=130000;

            IEnumerable<Advert> ads = _adService.GetListOfAd(searchParams, 1, 3);

            Assert.AreEqual(1, ads.Count());

            Assert.AreEqual(4, ads.First().Id);
        }

        [Test]
        public void UT_GetCountOfFilteredSetOfAd()
        {
            PrepareSimpleSetOfMockedData();

            _adService = TestFactory.Get<IAdService>();

            AdSearchParams searchParams = new AdSearchParams();
            searchParams.PriceFrom = 100000;
            searchParams.PriceTo = 130000;

            Assert.AreEqual(4, _adService.GetCountOfAd(searchParams));
        }

        [Test]
        public void UT_GetSellerList()
        {
            Entities entities = MockRepository.GenerateMock<Entities>();
            IDbSet<AspNetUser> MockSet = MockRepository.GenerateMock<IDbSet<AspNetUser>, IQueryable>();
            IQueryable<AspNetUser> MockData = new HashSet<AspNetUser>{
                new AspNetUser{Id="1", UserName="Name1"}
            }.AsQueryable();

            MockSet.Stub(m => m.Provider).Return(MockData.Provider);
            MockSet.Stub(m => m.Expression).Return(MockData.Expression);
            MockSet.Stub(m => m.ElementType).Return(MockData.ElementType);
            MockSet.Stub(m => m.GetEnumerator()).Return(MockData.GetEnumerator());

            entities.Stub(e => e.AspNetUsers).Repeat.Once().Return(MockSet);

            TestFactory.RegisterMock(entities);

            _adService = TestFactory.Get<IAdService>();

            IEnumerable<User> sellers = _adService.GetSellerList();

            Assert.AreEqual(1, sellers.Count());

            Assert.IsTrue(sellers.Where(s=>s.Id=="1").Any());
        }

        [Test]
        public void UT_GetAd()
        {
            PrepareSimpleSetOfMockedData();

            _adService = TestFactory.Get<IAdService>();

            Assert.AreEqual(3, _adService.GetAd(3).Id);
        }


        //i assume that there is existing, not blank, testing database
        [Test]
        public void UT_AddAd()
        {
            Entities entities = new Entities();

            SetUpTransaction(entities);

            try
            {
                _adService = new AdService(entities);

                int countBefore = _adService.GetCountOfAd();

                Advert ad = new Advert();
                ad.Title = "new add";
                ad.Description = "new add's description";
                ad.SellerID = "b5a3b686-d732-4b1a-a00a-a99d5a7d59ff";
                ad.Price = 100000;

                _adService.AddAd(ad);

                Assert.AreEqual(countBefore+1, _adService.GetCountOfAd());
            }
            finally
            {
                RollBackTransaction(entities);//make sure that the sample data gets cleaned up so the database looks like it did before your test
            }
        }

        [Test]
        public void UT_EditAd()
        {
            Entities entities = new Entities();

            SetUpTransaction(entities);

            try
            {
                _adService = new AdService(entities);

                Advert ad = _adService.GetAd(1);
                string titleBefore = ad.Title;

                ad.Title = "changed title";
                _adService.SaveAd(ad);

                ad = _adService.GetAd(1);

                Assert.AreEqual("changed title", ad.Title);
                Assert.AreNotEqual("changed title", titleBefore);
            }
            finally
            {
                RollBackTransaction(entities);
            }
        }

        [Test]
        public void UT_DeleteAd()
        {
            Entities entities = new Entities();

            SetUpTransaction(entities);

            try
            {
                _adService = new AdService(entities);

                Advert adBefore = _adService.GetAd(1);

                _adService.DeleteAd(1);

                Advert adAfter = _adService.GetAd(1);

                Assert.IsTrue(adBefore!=null);
                Assert.IsTrue(adAfter==null);
            }
            finally
            {
                RollBackTransaction(entities);
            }
        }
    }
}





        

 
        