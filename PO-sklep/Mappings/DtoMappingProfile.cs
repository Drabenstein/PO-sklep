﻿using AutoMapper;
using PO_sklep.DTO;
using PO_sklep.Models;
using System;

namespace PO_sklep.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Opinia, ReviewDto>()
                .ForMember(x => x.Comment, src => src.MapFrom(x => x.Komentarz))
                .ForMember(x => x.Rating, src => src.MapFrom(x => x.Ocena))
                .ForMember(x => x.Author, src => src.MapFrom(x => x.Klient != null ? x.Klient.Email : null))
                .ReverseMap();

            CreateMap<Produkt, ProductDto>()
                .ForMember(x => x.CategoryId, src => src.MapFrom(x => x.IdKategorii))
                .ForMember(x => x.Description, src => src.MapFrom(x => x.Opis))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.NazwaProduktu))
                .ForMember(x => x.Price, src => src.MapFrom(x => Math.Round(x.CenaNetto * (1.0m + (x.Vat / 100.0m)), 2)))
                .ForMember(x => x.ProducerName, src => src.MapFrom(x => x.Producent))
                .ForMember(x => x.ProductId, src => src.MapFrom(x => x.IdProduktu))
                .ForMember(x => x.Reviews, src => src.MapFrom(x => x.Opinie))
                .ReverseMap();

            CreateMap<ZamowienieProdukt, OrderItemDto>()
                .ForMember(x => x.ProductId, src => src.MapFrom(x => x.IdProduktu))
                .ForMember(x => x.Count, src => src.MapFrom(x => x.Ilosc))
                .ReverseMap();
        }
    }
}
