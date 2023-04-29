using System;
namespace CoinAPI.Models
{
	public class Response
	{
        public int? statusCode { get; set; }
        public string? statusDescription { get; set; }
        public User? user { get; set; }
        public Coin? coin { get; set; }
        public List<Coin>? coins { get; set; }
        public List<User>? users { get; set; }
    }
}

