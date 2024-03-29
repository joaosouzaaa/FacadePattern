﻿using FacadePattern.API.DataTransferObjects.Coupon;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Settings.NotificationHandlerSettings;
using Microsoft.AspNetCore.Mvc;

namespace FacadePattern.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class CouponController(ICouponService couponService) : ControllerBase
{
    [HttpPost("add-coupon")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] CouponSave couponSave) =>
        couponService.AddAsync(couponSave);

    [HttpPut("update-coupon")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] CouponUpdate couponUpdate) =>
        couponService.UpdateAsync(couponUpdate);

    [HttpDelete("delete-coupon")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> DeleteAsync([FromQuery]int id) =>
        couponService.DeleteAsync(id);

    [HttpGet("get-all-coupons")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CouponResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<CouponResponse>> GetAllAsync() =>
        couponService.GetAllAsync();
}
