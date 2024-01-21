using FacadePattern.API.DataTransferObjects.Coupon;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Interfaces.Settings;
using FacadePattern.API.Services;
using Moq;
using UnitTests.TestBuilders;
using UnitTests.TestBuilders.BuildUtils;

namespace UnitTests.ServiceTests;
public sealed class CouponServiceTests
{
    private readonly Mock<ICouponRepository> _couponRepositoryMock;
    private readonly Mock<ICouponMapper> _couponMapperMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly CouponService _couponService;

    public CouponServiceTests()
    {
        _couponRepositoryMock = new Mock<ICouponRepository>();
        _couponMapperMock = new Mock<ICouponMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _couponService = new CouponService(_couponRepositoryMock.Object, _couponMapperMock.Object, _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var couponSave = CouponBuilder.NewObject().SaveBuild();

        _couponRepositoryMock.Setup(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(false);

        var coupon = CouponBuilder.NewObject().DomainBuild();
        _couponMapperMock.Setup(c => c.SaveToDomain(It.IsAny<CouponSave>()))
            .Returns(coupon);

        _couponRepositoryMock.Setup(c => c.AddAsync(It.IsAny<Coupon>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _couponService.AddAsync(couponSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_NameAlreadyExists_ReturnsFalse()
    {
        // A
        var couponSave = CouponBuilder.NewObject().SaveBuild();

        _couponRepositoryMock.Setup(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _couponService.AddAsync(couponSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _couponMapperMock.Verify(c => c.SaveToDomain(It.IsAny<CouponSave>()), Times.Never());
        _couponRepositoryMock.Verify(c => c.AddAsync(It.IsAny<Coupon>()), Times.Never());

        Assert.False(addResult);
    }

    [Theory]
    [MemberData(nameof(AddInvalidEntityParameters))]
    public async Task AddAsync_EntityInvalid_ReturnsFalse(Coupon coupon)
    {
        // A
        var couponSave = CouponBuilder.NewObject().SaveBuild();

        _couponRepositoryMock.Setup(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(false);

        _couponMapperMock.Setup(c => c.SaveToDomain(It.IsAny<CouponSave>()))
            .Returns(coupon);

        // A
        var addResult = await _couponService.AddAsync(couponSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _couponRepositoryMock.Verify(c => c.AddAsync(It.IsAny<Coupon>()), Times.Never());

        Assert.False(addResult);
    }

    public static IEnumerable<object[]> AddInvalidEntityParameters()
    {
        yield return new object[]
        {
            CouponBuilder.NewObject().WithName("").DomainBuild()
        };

        yield return new object[]
        {
            CouponBuilder.NewObject().WithName(new string('a', 101)).DomainBuild()
        };

        yield return new object[]
        {
            CouponBuilder.NewObject().WithDiscountPorcentage(-2).DomainBuild()
        };
    }

    [Fact]
    public async Task UpdateAsync_NameIsEqualSuccessfulScenario_ReturnsTrue()
    {
        // A
        const string name = "test";
        var couponUpdate = CouponBuilder.NewObject().WithName(name).UpdateBuild();

        var coupon = CouponBuilder.NewObject().WithName(name).DomainBuild();
        _couponRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(coupon);

        _couponMapperMock.Setup(c => c.UpdateToDomain(It.IsAny<CouponUpdate>(), It.IsAny<Coupon>()));

        _couponRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<Coupon>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _couponService.UpdateAsync(couponUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _couponRepositoryMock.Verify(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()), Times.Never());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_NameIsDifferentSuccessfulScenario_ReturnsTrue()
    {
        // A
        var couponUpdate = CouponBuilder.NewObject().WithName("teste").UpdateBuild();

        var coupon = CouponBuilder.NewObject().WithName("different").DomainBuild();
        _couponRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(coupon);

        _couponRepositoryMock.Setup(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(false);

        _couponMapperMock.Setup(c => c.UpdateToDomain(It.IsAny<CouponUpdate>(), It.IsAny<Coupon>()));

        _couponRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<Coupon>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _couponService.UpdateAsync(couponUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _couponRepositoryMock.Verify(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()), Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var couponUpdate = CouponBuilder.NewObject().UpdateBuild();

        _couponRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult<Coupon?>(null));

        // A
        var updateResult = await _couponService.UpdateAsync(couponUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _couponRepositoryMock.Verify(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()), Times.Never());
        _couponMapperMock.Verify(c => c.UpdateToDomain(It.IsAny<CouponUpdate>(), It.IsAny<Coupon>()), Times.Never());
        _couponRepositoryMock.Verify(c => c.UpdateAsync(It.IsAny<Coupon>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_NameAlreadyExist_ReturnsFalse()
    {
        // A
        var couponUpdate = CouponBuilder.NewObject().WithName("teste").UpdateBuild();

        var coupon = CouponBuilder.NewObject().WithName("different").DomainBuild();
        _couponRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(coupon);

        _couponRepositoryMock.Setup(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _couponService.UpdateAsync(couponUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _couponRepositoryMock.Verify(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()), Times.Once());
        _couponMapperMock.Verify(c => c.UpdateToDomain(It.IsAny<CouponUpdate>(), It.IsAny<Coupon>()), Times.Never());
        _couponRepositoryMock.Verify(c => c.UpdateAsync(It.IsAny<Coupon>()), Times.Never());

        Assert.False(updateResult);
    }

    [Theory]
    [MemberData(nameof(AddInvalidEntityParameters))]
    public async Task UpdateAsync_EntityInvalid_ReturnsFalse(Coupon coupon)
    {
        // A
        const string name = "test";
        var couponUpdate = CouponBuilder.NewObject().WithName(name).UpdateBuild();

        _couponRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(coupon);

        _couponRepositoryMock.Setup(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(false);

        _couponMapperMock.Setup(c => c.UpdateToDomain(It.IsAny<CouponUpdate>(), It.IsAny<Coupon>()));

        // A
        var updateResult = await _couponService.UpdateAsync(couponUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _couponRepositoryMock.Verify(c => c.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()), Times.Once());
        _couponRepositoryMock.Verify(c => c.UpdateAsync(It.IsAny<Coupon>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = 9;

        _couponRepositoryMock.Setup(p => p.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(true);

        _couponRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        // A
        var deleteResult = await _couponService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

        Assert.True(deleteResult);
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = 9;

        _couponRepositoryMock.Setup(p => p.ExistsAsync(TestBuildUtil.BuildExpressionFunc<Coupon>()))
            .ReturnsAsync(false);

        // A
        var deleteResult = await _couponService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _couponRepositoryMock.Verify(p => p.DeleteAsync(It.IsAny<int>()), Times.Never());

        Assert.False(deleteResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsEntityList()
    {
        // A
        var couponList = new List<Coupon>()
        {
            CouponBuilder.NewObject().DomainBuild()
        };
        _couponRepositoryMock.Setup(p => p.GetAllAsync())
            .ReturnsAsync(couponList);

        var couponResponseList = new List<CouponResponse>()
        {
            CouponBuilder.NewObject().ResponseBuild()
        };
        _couponMapperMock.Setup(p => p.DomainListToResponseList(It.IsAny<List<Coupon>>()))
            .Returns(couponResponseList);

        // A
        var couponResponseListResult = await _couponService.GetAllAsync();

        // A
        Assert.Equal(couponResponseListResult.Count, couponResponseList.Count);
    }
}
