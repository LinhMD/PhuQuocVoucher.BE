﻿using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos;

public class DeleteDto : IDto
{
    public DateTime DeleteAt { get; } = DateTime.Now;

    public ModelStatus Status { get; } = ModelStatus.Delete;
    public virtual void InitMapper()
    {
    }
}