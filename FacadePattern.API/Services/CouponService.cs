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
    public async Task<bool> AddAsync(CouponSave couponSave)
    {
        if (await couponRepository.ExistsAsync(c => c.Name == couponSave.Name))
        {
            notificationHandler.AddNotification(nameof(EMessage.Exists), EMessage.Exists.Description().FormatTo("Name"));

            return false;
        }

        var coupon = couponMapper.SaveToDomain(couponSave);

        if (!IsValid(coupon))
        {
            notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Coupon"));

            return false;
        }

        return await couponRepository.AddAsync(coupon);
    }

    public async Task<bool> UpdateAsync(CouponUpdate couponUpdate)
    {
        var coupon = await couponRepository.GetByPredicateAsync(c => c.Id == couponUpdate.Id);

        if (coupon is null)
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Product"));

            return false;
        }

        if (!coupon.Name.Equals(couponUpdate.Name) && await couponRepository.ExistsAsync(c => c.Name == couponUpdate.Name))
        {
            notificationHandler.AddNotification(nameof(EMessage.Exists), EMessage.Exists.Description().FormatTo("Name"));

            return false;
        }

        couponMapper.UpdateToDomain(couponUpdate, coupon);

        if (!IsValid(coupon))
        {
            notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Coupon"));

            return false;
        }

        return await couponRepository.UpdateAsync(coupon);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await couponRepository.ExistsAsync(c => c.Id == id))
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Product"));

            return false;
        }

        return await couponRepository.DeleteAsync(id);
    }

    public async Task<List<CouponResponse>> GetAllAsync()
    {
        var couponList = await couponRepository.GetAllAsync();

        return couponMapper.DomainListToResponseList(couponList);
    }

    public async Task<bool> IsCouponValid(string name)
    {
        if(!await couponRepository.ExistsAsync(c => c.Name == name))
        {
            notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Coupon"));

            return false;
        }

        return true;
    }

    public async Task <bool> ProcessDiscountAsync(Order order, string name)
    {
        var coupon = await couponRepository.GetByPredicateAsync(c => c.Name == name);

        if (coupon is null)
        {
            notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Coupon"));

            return false;
        }

        decimal totalDiscount = 0;

        foreach (var productOrder in order.ProductsOrder)
        {
            decimal discountPercentage = coupon.DiscountPorcentage / 100;
            decimal discountAmount = productOrder.TotalValue * discountPercentage;

            productOrder.TotalValue -= discountAmount;

            totalDiscount += discountAmount;
        }

        order.TotalValue -= totalDiscount;

        return true;
    }

    private static bool IsValid(Coupon coupon) =>
        !string.IsNullOrEmpty(coupon.Name)
        && coupon.Name.Length > 1
        && coupon.Name.Length < 100
        && coupon.DiscountPorcentage > 0
        && coupon.DiscountPorcentage < 100;
}
