﻿namespace PhuQuocVoucher.Data.Models;

public class Blog
{
    public int Id { get; set; }

    public string Content { get; set; }

    public string BannerImage { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public DateTime CreateDate { get; set; }

    public IEnumerable<Place> Places { get; set; }

    public IEnumerable<Tag> Tags { get; set; }
}