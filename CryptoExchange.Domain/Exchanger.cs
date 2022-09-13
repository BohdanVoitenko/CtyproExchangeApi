using System;
namespace CryptoExchange.Domain
{
	public class Exchanger
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
		public string WebResuorceUrl { get; set; }

		public ICollection<Order> Orders { get; set; }

        public Exchanger()
        {
			Orders = new List<Order>();
        }
	}
}

