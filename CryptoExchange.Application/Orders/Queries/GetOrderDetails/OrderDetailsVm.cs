using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Orders.Queries.GetOrderDetails
{
	public class OrderDetailsVm : IMapWith<Order>
	{
        public Guid Id { get; set; }
        public string Exchanger { get; set; }
        public string ExchangeFrom { get; set; }
        public string ExchangeTo { get; set; }
        public double IncomeSum { get; set; }
        public int OutcomeSum { get; set; } = 1;
        public double Amount { get; set; }
        public double MinAmount { get; set; }
        public double MaxAmount { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? EditTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsVm>()
                .ForMember(orderVm => orderVm.ExchangeFrom,
                opt => opt.MapFrom(order => order.ExchangeFrom))
                .ForMember(orderVm => orderVm.ExchangeTo,
                opt => opt.MapFrom(order => order.ExchangeTo))
                .ForMember(orderVm => orderVm.IncomeSum,
                opt => opt.MapFrom(order => order.IncomeSum))
                .ForMember(orderVm => orderVm.OutcomeSum,
                opt => opt.MapFrom(order => order.OutcomeSum))
                .ForMember(orderVm => orderVm.Amount,
                opt => opt.MapFrom(order => order.Amount))
                .ForMember(orderVm => orderVm.MinAmount,
                opt => opt.MapFrom(order => order.MinAmount))
                .ForMember(orderVm => orderVm.MaxAmount,
                opt => opt.MapFrom(order => order.MaxAmount))
                .ForMember(orderVm => orderVm.CreationTime,
                opt => opt.MapFrom(order => order.CreationTime))
                .ForMember(orderVm => orderVm.EditTime,
                opt => opt.MapFrom(order => order.EditTime));


        }
    }
}

