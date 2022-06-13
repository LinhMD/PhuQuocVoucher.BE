using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Controller;

public class PhuQuocDataContext : DbContext
{
    private readonly IConfiguration _config;
    public PhuQuocDataContext(IConfiguration configuration)
    {
        _config = configuration;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config["ConnectionStrings:PhuQuocDB"]);
    }



}