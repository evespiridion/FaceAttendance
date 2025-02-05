using Microsoft.AspNetCore.Mvc;
using FaceONNX;
using Microsoft.ML.OnnxRuntime;
using FaceAttendance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FaceAttendance.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing;
using UMapx.Visualization;
using FaceEmbeddingsClassification;
using System.Collections;
using UMapx.Imaging;
using System.Text.Json;
using Tensorflow.Contexts;

namespace FaceAttendance.Web.Controllers
{
    public class FaceRecognitionController : Controller
    {
        private ApplicationDbContext _db;


        public FaceRecognitionController(ApplicationDbContext db)
        {
            _db = db;      
        } 

        public IActionResult Index()
        {       
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetEmployeeInfo()
        {
            var photos = await _db.EmployeePhotos
                    .Select(e => new EmployeePhotos
                    {
                        EmployeeName = e.EmployeeName,
                        EmployeePhoto = e.EmployeePhoto
                    })
                    .ToListAsync(); // Get all columns from EmployeePhotos table

            return Ok(photos);
        }

        [HttpPost]
        public async Task<IActionResult> RecordTime([FromBody] string employeeName)
        {

            var dateToday = DateTime.Today; // Get today's date in the format yyyy-MM-dd

            // Find or create a record for today
            var record = await _db.DailyTimeRecord
                .FirstOrDefaultAsync(r => r.EmployeeName == employeeName && r.DateToday == dateToday);

            if (record == null)
            {
                // Create a new record if it doesn't exist
                record = new DailyTimeRecord
                {
                    EmployeeName = employeeName,
                    DateToday = dateToday,
                    TimeIn = DateTime.Now.TimeOfDay // Set TimeIn to the current time
                };
                _db.DailyTimeRecord.Add(record);
            }
            else
            {
                // If TimeIn is already set, update TimeOut to the current time
                record.TimeOut = DateTime.Now.TimeOfDay;
                _db.DailyTimeRecord.Update(record);
            }

            // Save changes to the database
            await _db.SaveChangesAsync();

            return Ok(record);
        }

        public static Bitmap ConvertBase64ToBitmap(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                throw new ArgumentException("The base64 string cannot be null or empty.", nameof(base64String));
            }

            // Strip data URL prefix if present
            if (base64String.StartsWith("data:image/png;base64,"))
            {
                base64String = base64String.Substring("data:image/png;base64,".Length);
            }

            // Convert base64 to byte array
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Create bitmap safely
            using (var ms = new MemoryStream(imageBytes))
            {
                return new Bitmap(ms);
            }
        }

    }

    public class ImageModel
    {
        public string Image { get; set; }
    }

}

