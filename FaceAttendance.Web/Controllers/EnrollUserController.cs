using FaceAttendance.Domain.Entities;
using FaceAttendance.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FaceAttendance.Web.Controllers
{
    public class EnrollUserController : Controller
    {
        private ApplicationDbContext _db;

        public class PhotoModel
        {
            public string EmployeeName { get; set; }
            public string Photo { get; set; }
        }

        public EnrollUserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto([FromBody] PhotoModel data)
        {
            if (data == null)
            {
                return BadRequest("Invalid data received.");
            }


            string name = data.EmployeeName;
            //byte[] photo = data.photo.ToObject<byte[]>(); // Convert JSON array to byte array
            // Convert base64 to byte array
            byte[] imageBytes = Convert.FromBase64String(data.Photo);

            /*
            var record = await _db.EmployeePhotos
                .FirstOrDefaultAsync(r => r.EmployeeName == name);
            
            if (record == null)
            {
                record = new EmployeePhotos
                {
                    EmployeeName = name,
                    EmployeePhoto = imageBytes
                };
                _db.EmployeePhotos.Add(record);
            }
            else
            {
                return BadRequest("Employee name already exists.");
            }
            */
            var record = await _db.EmployeePhotos
                .FirstOrDefaultAsync(r => r.EmployeeName == name);

            record = new EmployeePhotos
            {
                EmployeeName = name,
                EmployeePhoto = imageBytes
            };
            _db.EmployeePhotos.Add(record);

            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
