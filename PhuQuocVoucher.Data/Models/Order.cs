﻿namespace PhuQuocVoucher.Data.Models;

public class Order
{
    public int Id { get; set; }

    public double TotalPrice { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime CompleteDate { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Processing;

    public Customer? Customer { get; set; }

    public Seller? Seller { get; set; }

    public PaymentDetail? PaymentDetail { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; }


}