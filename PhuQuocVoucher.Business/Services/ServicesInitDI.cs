﻿using Microsoft.Extensions.DependencyInjection;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Business.Services.Implements;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services;

public static class ServicesInitDi
{
    public static void InitServices(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IBlogService, BlogService>();
        serviceCollection.AddScoped<ICartItemService, CartItemService>();
        serviceCollection.AddScoped<ICartService, CartService>();
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
        serviceCollection.AddScoped<IOrderService, OrderService>();
        serviceCollection.AddScoped<IOrderItemService, OrderItemService>();
        serviceCollection.AddScoped<IPaymentDetailService, PaymentDetailService>();
        serviceCollection.AddScoped<IPlaceService, PlaceService>();
        serviceCollection.AddScoped<IProfileService, ProfileService>();
        serviceCollection.AddScoped<IProviderService, ProviderService>();
        serviceCollection.AddScoped<IReviewService, ReviewService>();
        serviceCollection.AddScoped<ISellerService, SellerService>();
        serviceCollection.AddScoped<IServiceService, ServiceService>();
        serviceCollection.AddScoped<IServiceTypeService, ServiceTypeService>();
        serviceCollection.AddScoped<ITagService, TagService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IVoucherService, VoucherService>();
        
        serviceCollection.AddScoped<IFirebaseServiceIntegration, FirebaseService>();
        serviceCollection.AddScoped<IMailingService, MailingService>();

        serviceCollection.AddScoped<IPriceBookService, PriceBookService>();
        serviceCollection.AddScoped<IQrCodeService, QrCodeService>();
        serviceCollection.AddScoped<MomoSetting>();
        serviceCollection.AddScoped<PaymentService>();
    }
}