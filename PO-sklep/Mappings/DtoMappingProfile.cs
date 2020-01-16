using AutoMapper;
using PO_sklep.DTO;
using PO_sklep.Models;
using System;

namespace PO_sklep.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(x => x.Comment, src => src.MapFrom(x => x.Comment))
                .ForMember(x => x.Rating, src => src.MapFrom(x => x.Rating))
                .ForMember(x => x.Author, src => src.MapFrom(x => x.Client != null ? x.Client.Email : null))
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryId, src => src.MapFrom(x => x.CategoryId))
                .ForMember(x => x.Description, src => src.MapFrom(x => x.Description))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.ProductName))
                .ForMember(x => x.Price, src => src.MapFrom(x => Math.Round(x.NetPrice * (1.0m + (x.Vat / 100.0m)), 2)))
                .ForMember(x => x.ProducerName, src => src.MapFrom(x => x.ProducerName))
                .ForMember(x => x.ProductId, src => src.MapFrom(x => x.ProductId))
                .ForMember(x => x.Reviews, src => src.MapFrom(x => x.Reviews))
                .ReverseMap();

            CreateMap<ZamowienieProdukt, OrderItemDto>()
                .ForMember(x => x.ProductId, src => src.MapFrom(x => x.IdProduktu))
                .ForMember(x => x.Count, src => src.MapFrom(x => x.Ilosc))
                .ReverseMap();
        }
    }
}
