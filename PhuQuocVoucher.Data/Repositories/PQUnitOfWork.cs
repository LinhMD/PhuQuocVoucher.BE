using CrudApiTemplate.Repository;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;
using PhuQuocVoucher.Data.Repositories.Implements;

namespace PhuQuocVoucher.Data.Repositories;

public class PqUnitOfWork : UnitOfWork
{
    private readonly PhuQuocDataContext _dataContext;

    public PqUnitOfWork(PhuQuocDataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;

        Add(new BlogRepository(dataContext));
        Add(new CartRepository(dataContext));
        Add(new CartItemRepository(dataContext));
        Add(new CustomerRepository(dataContext));
        Add(new OrderItemRepository(dataContext));
        Add(new OrderRepository(dataContext));
        Add(new PaymentDetailRepository(dataContext));
        Add(new PlaceRepository(dataContext));
        Add(new ProviderRepository(dataContext));
        Add(new ReviewRepository(dataContext));
        Add(new SellerRepository(dataContext));
        Add(new ServiceRepository(dataContext));
        Add(new ServiceTypeRepository(dataContext));
        Add(new TagRepository(dataContext));
        Add(new UserRepository(dataContext));
        Add(new VoucherRepository(dataContext));
        Add(new QrCodeRepository(dataContext));
        Add(new PriceLevelRepository(dataContext));
    }

    public IBlogRepository Blogs => (IBlogRepository) Get<Blog>();

    public ICartRepository Carts => (ICartRepository) Get<Cart>();

    public ICartItemRepository CartItems => (ICartItemRepository) Get<CartItem>();

    //public IComboRepository Combos => (IComboRepository) Get<Combo>();

    public IOrderItemRepository OrderItems => (IOrderItemRepository) Get<OrderItem>();

    public IOrderRepository Orders => (IOrderRepository) Get<Order>();

    public IPaymentDetailRepository PaymentDetails => (IPaymentDetailRepository) Get<PaymentDetail>();

    public IPlaceRepository Places => (IPlaceRepository) Get<Place>();

    public IProviderRepository Providers => (IProviderRepository) Get<ServiceProvider>();

    public IReviewRepository Reviews => (IReviewRepository) Get<Review>();

    public ISellerRepository Sellers => (ISellerRepository) Get<Seller>();

    public IServiceRepository Services => (IServiceRepository) Get<Service>();

    public IServiceTypeRepository ServiceTypes => (IServiceTypeRepository) Get<ServiceType>();

    public ITagRepository Tags => (ITagRepository) Get<Tag>();

    public IUserRepository Users => (IUserRepository) Get<User>();

    public IVoucherRepository Vouchers => (IVoucherRepository) Get<VoucherCompaign>();
}