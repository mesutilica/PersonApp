using PersonApp.BL;
using PersonApp.Entities;
using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange : hazýrlýk
            IRepository<AppUser> repository = new Repository<AppUser>();
            //Act : Aksiyon
            var list = repository.GetAll();
            //Assert
            Assert.Equal(3, list.Count);
        }
    }
}
