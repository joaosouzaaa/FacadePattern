using FacadePattern.API.DataTransferObjects.Coupon;
using FacadePattern.API.Entities;
using FacadePattern.API.Enums;
using FacadePattern.API.Extensions;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Interfaces.Settings;

namespace FacadePattern.API.Services;

public sealed class CouponService(ICouponRepository couponRepository, ICouponMapper couponMapper,
                                  INotificationHandler notificationHandler) : ICouponService
{
    private readonly ICouponRepository _couponRepository = couponRepository;
    private readonly ICouponMapper _couponMapper = couponMapper;
    private readonly INotificationHandler _notificationHandler = notificationHandler;

    public async Task<bool> AddAsync(CouponSave couponSave)
    {
        if (await _couponRepository.ExistsAsync(c => c.Name == couponSave.Name))
        {
            _notificationHandler.AddNotification(nameof(EMessage.Exists), EMessage.Exists.Description().FormatTo("Name"));

            return false;
        }

        var coupon = _couponMapper.SaveToDomain(couponSave);

        if (!IsValid(coupon))
        {
            _notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Coupon"));

            return false;
        }

        return await _couponRepository.AddAsync(coupon);
    }

    public async Task<bool> UpdateAsync(CouponUpdate couponUpdate)
    {
        var coupon = await _couponRepository.GetByIdAsync(couponUpdate.Id);

        if (coupon is null)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Product"));

            return false;
        }

        if (!coupon.Name.Equals(couponUpdate.Name) && await _couponRepository.ExistsAsync(c => c.Name == couponUpdate.Name))
        {
            _notificationHandler.AddNotification(nameof(EMessage.Exists), EMessage.Exists.Description().FormatTo("Name"));

            return false;
        }

        _couponMapper.UpdateToDomain(couponUpdate, coupon);

        if (!IsValid(coupon))
        {
            _notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Coupon"));

            return false;
        }

        return await _couponRepository.UpdateAsync(coupon);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _couponRepository.ExistsAsync(c => c.Id == id))
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Product"));

            return false;
        }

        return await _couponRepository.DeleteAsync(id);
    }

    public async Task<List<CouponResponse>> GetAllAsync()
    {
        var couponList = await _couponRepository.GetAllAsync();

        return _couponMapper.DomainListToResponseList(couponList);
    }

    private static bool IsValid(Coupon coupon) =>
        !string.IsNullOrEmpty(coupon.Name)
        && coupon.Name.Length > 1
        && coupon.Name.Length < 100
        && coupon.DiscountPorcentage > 0;
}
