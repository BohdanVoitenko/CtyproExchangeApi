using System;
namespace CryptoExchange.Domain
{
	public class Order
	{
        public Guid Id { get; set; }

        //public string UserId { get; set; }

        public Guid ExchangerId { get; set; }

        public Exchanger Exchanger { get; set; }

        public string ExchangerName { get; set; }

        public string ExchangeFrom { get; set; }

        public string ExchangeTo { get; set; }

        public double IncomeSum { get; set; }

        public double OutcomeSum { get; set; } = 1.0;

        public double Amount { get; set; }

        public double MinAmount { get; set; }

        public double MaxAmount { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? EditTime { get; set; }

    }
}

