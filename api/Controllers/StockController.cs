using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
       public IActionResult GetAll()
       {
           var stocks = _context.Stock.ToList().Select(stock => stock.ToStockDto());
           return Ok(stocks);
       }

       [HttpGet("{id}")]
         public IActionResult GetById([FromRoute] int id)
         {
            var stock = _context.Stock.Find(id);
            if(stock == null)
            {
                return NotFound();
            } else
            {
                return Ok(stock.ToStockDto());
            }
         }

         [HttpPost]
         public IActionResult Create([FromBody] CreateStockRequestDto stockDto) 
         {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
         }

         [HttpPut]
         [Route("{id}")]
         public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
         {
            var stock = _context.Stock.FirstOrDefault(stock => stock.Id == id);
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
            _context.SaveChanges();
            return Ok(stock.ToStockDto());
         }

         [HttpDelete]
         [Route("{id}")]
            public IActionResult Delete([FromRoute] int id)
            {
                var stock = _context.Stock.FirstOrDefault(stock => stock.Id == id);
                if(stock == null)
                {
                    return NotFound();
                }
                _context.Stock.Remove(stock);
                _context.SaveChanges();
                return NoContent();
            }

    }
}