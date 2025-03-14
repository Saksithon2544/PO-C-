using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; // เพิ่มการใช้งาน Swashbuckle

namespace SaksithonApp.Controllers
{
    [ApiController]
    [Route("api/po")]
    public class POController : ControllerBase
    {
        private static readonly Dictionary<int, string> POs = new();

        // POST: รับข้อมูลใบสั่งซื้อ
        [HttpPost]
        [SwaggerOperation(Summary = "รับข้อมูลใบสั่งซื้อใหม่", 
                          Description = "API สำหรับการรับข้อมูลใบสั่งซื้อใหม่และบันทึกในระบบ")]
        [SwaggerResponse(201, "สร้างใบสั่งซื้อสำเร็จ", typeof(object))]
        [SwaggerResponse(400, "ข้อมูลไม่ถูกต้อง")]
        public IActionResult CreatePO([FromBody] string poDetails)
        {
            int id = POs.Count + 1;
            POs[id] = poDetails;
            return CreatedAtAction(nameof(GetPO), new { id }, new { id, poDetails });
        }

        // GET: ดึงข้อมูลใบสั่งซื้อทั้งหมด
        [HttpGet]
        [SwaggerOperation(Summary = "ดึงข้อมูลใบสั่งซื้อทั้งหมด", 
                          Description = "API สำหรับดึงข้อมูลใบสั่งซื้อทั้งหมดที่มีอยู่ในระบบ")]
        [SwaggerResponse(200, "ข้อมูลใบสั่งซื้อทั้งหมด", typeof(IEnumerable<object>))]
        [SwaggerResponse(404, "ไม่พบข้อมูลใบสั่งซื้อ")]
        public IActionResult GetPOs()
        {
            return Ok(POs.Select(kvp => new { kvp.Key, kvp.Value }));
        }

        // GET: ดึงข้อมูลใบสั่งซื้อตาม ID
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "ดึงข้อมูลใบสั่งซื้อตาม ID", 
                          Description = "API สำหรับดึงข้อมูลใบสั่งซื้อเฉพาะตาม ID ที่ระบุ")]
        [SwaggerResponse(200, "ข้อมูลใบสั่งซื้อที่ค้นหา", typeof(object))]
        [SwaggerResponse(404, "ไม่พบใบสั่งซื้อที่มี ID ดังกล่าว")]
        public IActionResult GetPO(int id)
        {
            if (POs.TryGetValue(id, out string? poDetails))
                return Ok(new { id, poDetails });
            return NotFound();
        }

        // GET: ดึงเลขคู่จาก 1 ถึง 20
        [HttpGet("even-numbers")]
        [SwaggerOperation(Summary = "ดึงเลขคู่จาก 1 ถึง 20", 
                          Description = "API สำหรับดึงเลขคู่จาก 1 ถึง 20 โดยใช้การวนลูป for-loop")]
        [SwaggerResponse(200, "ผลลัพธ์เลขคู่", typeof(IEnumerable<int>))]
        public IActionResult GetEvenNumbers()
        {
            var evenNumbers = new List<int>();
            for (int i = 1; i <= 20; i++)
            {
                if (i % 2 == 0)
                {
                    evenNumbers.Add(i);
                }
            }
            return Ok(evenNumbers);
        }
    }
}
