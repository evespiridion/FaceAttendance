using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAttendance.Domain.Entities
{
    public class EmployeePhotos
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public byte[] EmployeePhoto { get; set; }
    }

    public class DailyTimeRecord
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateToday { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
    }
}
