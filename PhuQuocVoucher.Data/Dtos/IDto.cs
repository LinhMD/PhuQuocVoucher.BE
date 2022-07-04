namespace PhuQuocVoucher.Data.Dtos;

public interface IDto
{
    public virtual void InitMapper()
    {

    }

    public static void Config<T>() where T : IDto, new()
    {
        new T().InitMapper();
    }
}