using System;
namespace CoinAPI.Models
{
	public class Coin
	{
        public int CoinId { get; set; }
        public string? CoinName { get; set; }
        public int? Price { get; set; }
        public string? Origin { get; set; }
        public int UserId { get; set; }
        public DateTime PurchasedAt { get; set; }
    }
}

