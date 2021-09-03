using AutoMapper;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.data.Mappers
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(i => i.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemModel>()
                .ForMember(o=>o.ProductId, ex=>ex.MapFrom(i=>i.Product.Id))
                .ForMember(o=>o.ProductName, ex=>ex.MapFrom(i=>i.Product.ProductName))
                .ForMember(o=>o.ImageName, ex=>ex.MapFrom(i=>i.Product.ImageName))
                .ForMember(o=>o.Size, ex=>ex.MapFrom(i=>i.Product.Size))
                .ForMember(o=>o.Category, ex=>ex.MapFrom(i=>i.Product.Category))
                .ForMember(o=>o.OrderId, ex=>ex.MapFrom(i=>i.Order.Id))
                .ReverseMap()
                .ForMember(m=>m.Product, opt=>opt.Ignore());

            CreateMap<Product, ProductModel>()
                .ForMember(p => p.Category, ex => ex.MapFrom(i => i.CategoryNavigation.CategoryName));

        }
    }
}
