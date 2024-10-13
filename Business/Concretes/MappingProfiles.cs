using AutoMapper;
using Entities.Concretes;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Entities.Concrete;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductCreateDto, Product>().ReverseMap();
        CreateMap<ProductUpdateDto, Product>().ReverseMap();
        CreateMap<Product, ProductResponseDto>().ReverseMap();
        CreateMap<ProductUpdateDto, ProductResponseDto>().ReverseMap();

        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Bill.Table.TableName))
            .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Bill.Table.Id))
            .ForMember(dest => dest.BillDate, opt => opt.MapFrom(src => src.Bill.CreatedDate))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest=>dest.CategoryId, opt=>opt.MapFrom(src=>src.Product.Category.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name)) // CategoryName için ekleme
            .ReverseMap();
        CreateMap<Order, OrderCreateDto>().ReverseMap();
        CreateMap<Order, OrderUpdateDto>().ReverseMap();
        CreateMap<OrderResponseDto, OrderUpdateDto>()  .ReverseMap();

        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        CreateMap<Category, CategoryResponseDto>().ReverseMap();

        CreateMap<TableCreateDto, Table>();
        CreateMap<TableResponseDto, Table>().ReverseMap();
        CreateMap<TableResponseDto, TableUpdateDto>().ReverseMap();
        CreateMap<TableUpdateDto, Table>().ReverseMap();

        CreateMap<StoreBillCreateDto, StoreBill>().ReverseMap();
        CreateMap<StoreBillResponseDto, StoreBill>().ReverseMap();
        CreateMap<StoreBillUpdateDto, StoreBill>().ReverseMap();
        CreateMap<StoreBillUpdateDto, StoreBillResponseDto>().ReverseMap();

        CreateMap<BillCreateDto, Bill>().ReverseMap();
        CreateMap<Bill, BillResponseDto>()
                  .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Table.TableName))
                  .ReverseMap(); CreateMap<BillUpdateDto, Bill>().ReverseMap();
        CreateMap<BillUpdateDto, BillResponseDto>().ReverseMap();

        //CreateMap<ProductResponseDto, ProductUpdateDto>();

        CreateMap<Payment, PaymentResponseDto>()
           .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.BillId)) // Ödeme faturası ID'si
           .ForMember(dest => dest.StoreBillId, opt => opt.MapFrom(src => src.Bill.StoreBillId)) // İlgili StoreBill ID'si
           .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Bill.TableId)) // İlgili masa ID'si
           .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Bill.Table.TableName)) // Masa adı
           .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount)) // Ödeme tutarı
           .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod)).ReverseMap(); // Ödeme yöntemi
        CreateMap<Payment, PaymentCreateDto>().ReverseMap();
    }
}
