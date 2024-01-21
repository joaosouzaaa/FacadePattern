﻿namespace FacadePattern.API.DataTransferObjects.Coupon;

public sealed record CouponUpdate(int Id,
                                  string Name,
                                  double DiscountPorcentage);
