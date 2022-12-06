using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceTypeDto;

public class UpdateServiceType : UpdateDto, IUpdateRequest<ServiceType>
{
    [MaxLength(255)] public string? Name { get; set; }

    public int? ParentTypeId { get; set; }

    public double? DefaultCommissionRate { get; set; }

    public ModelStatus? Status { get; set; }
}