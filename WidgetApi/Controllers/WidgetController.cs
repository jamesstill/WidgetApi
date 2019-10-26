using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WidgetApi.EFCore;
using WidgetApi.Models;

namespace WidgetApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly WidgetContext _context;

        public WidgetController(WidgetContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _context.ChangeTracker.QueryTrackingBehavior =
                QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Widget>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var items = await _context.Widgets
                .OrderBy(w => w.Name).ToListAsync();

            if (items == null || items.Count == 0)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var item = await _context.Widgets.FirstOrDefaultAsync(w => w.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
