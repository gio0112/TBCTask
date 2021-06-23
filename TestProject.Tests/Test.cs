using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject.Tests
{
    public class Test
    {
        [Fact]
        public void TestGenders ()
        {
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<Gender>>();
            dbSetMock.Setup(x => x.FindAsync(It.IsAny<Guid>()).Result);
            dbContextMock.Setup(x => x.Set<Gender>()).Returns(dbSetMock.Object);

            var repository = new Repository<Gender>(dbContextMock.Object);
            var genders = repository.GetAll().ToListAsync().Result;
            var expected = GendersResult();

            Assert.NotNull(genders);
            Assert.Equal(genders.Count, expected.Count);
        }

        private List<Gender> GendersResult ()
        {
            return  new List<Gender>
            {
                new Gender{ Id = 1, Value= "Male" },
                new Gender{ Id = 2, Value = "Female"}
            };
        }
    }
}
