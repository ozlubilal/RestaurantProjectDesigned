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
    }
}
