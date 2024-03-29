﻿using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class QrCodeService : ServiceCrud<QrCodeInfo>, IQrCodeService
{
    private readonly ILogger<QrCodeService> _logger;
    public QrCodeService(IUnitOfWork work, ILogger<QrCodeService> logger) : base(work.Get<QrCodeInfo>(), work, logger)
    {
        _logger = logger;
    }
}