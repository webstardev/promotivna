using MarkomPos.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AdministrationController(ApplicationContext context)
        {
            _context = context;

        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetGetPaymentMethod(int id)
        {
            var book = await _context.PaymentMethods.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
    }
}
