using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Misa.Qlts.Solution.BL.FixedAssetService;
using Misa.Qlts.Solution.DL.Contracts;
using NSubstitute;
using NSubstitute.Exceptions;
using NSubstitute.ReturnsExtensions;

namespace Misa.Qlts.Solution.UnitTest.Service
{
    [TestFixture]
    public class FixedAssetServiceTest
    {
        public async Task GetAsync_NotFound_ReturnException()
        {
            var id = Guid.Parse("2b9a8121-083d-4b31-a442-f29d4d97ee1e");

            var fixedAssetRepository = Substitute.For<IFixedAssetRepository>();
            fixedAssetRepository.GetAsync(id).ReturnsNull();

            var mapper = Substitute.For<Mapper>();

            var fixedAssetService = new FixedAssetService(fixedAssetRepository, mapper);

            var ex = Assert.ThrowsAsync<Exception>(
                async () => await fixedAssetService.GetAsync(id)
            );
            Assert.That(ex.Message, Is.EqualTo("Not Found"));
        }
    }
}
