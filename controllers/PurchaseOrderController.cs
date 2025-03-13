using Microsoft.AspNetCore.Mvc;

namespace SaksithonApp.controllers
{
    [ApiController]
    [Route("api/po")]
    public class POController : ControllerBase
    {
        private static readonly Dictionary<int, string> POs = new();

        // POST: รับข้อมูลใบสั่งซื้อ
        [HttpPost]
        public IActionResult CreatePO([FromBody] string poDetails)
        {
            int id = POs.Count + 1;
            POs[id] = poDetails;
            return CreatedAtAction(nameof(GetPO), new { id }, new { id, poDetails });
        }

        // GET: ดึงข้อมูลใบสั่งซื้อทั้งหมด
        [HttpGet]
        public IActionResult GetPOs()
        {
            return Ok(POs.Select(kvp => new { kvp.Key, kvp.Value }));
        }

        // GET: ดึงข้อมูลใบสั่งซื้อตาม ID
        [HttpGet("{id}")]
        public IActionResult GetPO(int id)
        {
            if (POs.TryGetValue(id, out string? poDetails))
                return Ok(new { id, poDetails });
            return NotFound();
        }
    }
}