using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoinAPI.Models;

namespace CoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly CoinAPIDBContext _context;

        public CoinController(CoinAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Coin
        [HttpGet]
        public async Task<ActionResult<Response>> GetCoins()
        {
            var coins = await _context.Coins.ToListAsync();

            if (coins == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "Coins not found"
                };
            }

            List<Coin> coinList = coins.Select(coin => new Coin
            {
                CoinId = coin.CoinId,
                CoinName = coin.CoinName,
                Price = coin.Price,
                Origin = coin.Origin,
                PurchasedAt = coin.PurchasedAt
            }).ToList();

            return new Response
            {
                statusCode = 200,
                statusDescription = "OK",
                coins = coinList
            };
        }

        // GET: api/Coin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetCoin(int? id)
        {
            var coin = await _context.Coins.FindAsync(id);

            if (coin == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "Coin not found"
                };
            }

            return new Response
            {
                statusCode = 200,
                statusDescription = "Success",
                coin = new Coin
                {
                    CoinId = coin.CoinId,
                    CoinName = coin.CoinName,
                    Price = coin.Price,
                    Origin = coin.Origin,
                    PurchasedAt = coin.PurchasedAt
                }
            };
        }

        // POST: api/Coin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostCoin(Coin coin)
        {
            if (coin.CoinName == null || coin.Price == null || coin.Origin == null || coin?.UserId == null)
            {
                return new Response
                {
                    statusCode = 400,
                    statusDescription = "Bad Request: Some required fields are missing",
                };
            }

            coin.PurchasedAt = DateTime.Now;
            _context.Coins.Add(coin);
            await _context.SaveChangesAsync();

            return new Response
            {
                statusCode = 201,
                statusDescription = "Created",
                coin = coin,
            };
        }

        // DELETE: api/Coin/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteCoin(int id)
        {
            if (_context.Coins == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "Coin not found"
                };
            }
            var coin = await _context.Coins.FindAsync(id);
            if (coin == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "Coin not found"
                };
            }

            _context.Coins.Remove(coin);
            await _context.SaveChangesAsync();

            return new Response
            {
                statusCode = 200,
                statusDescription = "Coin deleted",
                coin = coin
            };
        }

        private bool CoinExists(int? id)
        {
            return (_context.Coins?.Any(e => e.CoinId == id)).GetValueOrDefault();
        }
    }
}
