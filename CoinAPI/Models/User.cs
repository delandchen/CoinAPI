using System;
namespace CoinAPI.Models
{
	public class User
	{

		public int UserId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int? EmailId { get; set; }
		public string? EmailAddress { get; set; }
	}
}

