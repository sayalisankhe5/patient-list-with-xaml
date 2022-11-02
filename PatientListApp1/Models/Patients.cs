using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientListApp1.Models
{
    public class Patients
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
    }
}
