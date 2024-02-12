using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
       private readonly ApplicationDBContext _context;
       public StockController(ApplicationDBContext context)
       {
              _context = context;
       }
       [HttpGet]
       public async Task<IActionResult> GetAll()
       {
           var stocks = await _context.Stock.ToListAsync();
           var stockDto = stocks.Select(stock => stock.ToStockDto());
           return Ok(stocks);
       }

       [HttpGet("{id}")]
         public async Task<IActionResult> GetById([FromRoute] int id)
         {
            var stock = await _context.Stock.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            } else
            {
                return Ok(stock.ToStockDto());
            }
         }

         [HttpPost]
         public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) 
         {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
         }

         [HttpPut]
         [Route("{id}")]
         public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
         {
            var stock = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
            if(stock == null)
            {
                return NotFound();
            }
            stock.Symbol = updateDto.Symbol;
            stock.CompanyName = updateDto.CompanyName;
            stock.Purchase = updateDto.Purchase;
            stock.LastDiv = updateDto.LastDiv;
            stock.Industry = updateDto.Industry;
            stock.MarketCap = updateDto.MarketCap;
            await _context.SaveChangesAsync();
            return Ok(stock.ToStockDto());
         }

         [HttpDelete]
         [Route("{id}")]
            public async Task<IActionResult> Delete([FromRoute] int id)
            {
                var stock = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
                if(stock == null)
                {
                    return NotFound();
                }
                _context.Stock.Remove(stock);
                await _context.SaveChangesAsync();
                return NoContent();
            }

    }
}