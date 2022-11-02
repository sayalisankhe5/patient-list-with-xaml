using NuGet.Common;
using PatientListApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PatientListApp1.Data
{
    public class PatientsDatabase
    {
        static SQLiteAsyncConnection database;

        public static readonly AsyncLazy<PatientsDatabase> dbInstance = new AsyncLazy<PatientsDatabase>(async() => 
        {
            var instance = new PatientsDatabase();
            CreateTableResult result = await database.CreateTableAsync<Patients>();
            return instance;
        });

        public PatientsDatabase()
        {
            database = new SQLiteAsyncConnection(Constants.databasePath, Constants.flags);
        }

        public Task<List<Patients>> GetAllPatients()
        {
            return database.Table<Patients>().ToListAsync();
        }

        public Task<Patients> GetPatientById(int id)
        {
            return database.Table<Patients>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<Patients> GetPatientByPhoneNumber(long phoneNumber)
        {
            if(phoneNumber != 0)
            {
               return  database.Table<Patients>().FirstOrDefaultAsync(ph => ph.PhoneNumber == phoneNumber);
            }
            return null;
        }

        public Task<Patients> GetPatientByEmail(string email)
        {
            if (email != null)
            {
                return database.Table<Patients>().FirstOrDefaultAsync(ph => ph.Email == email);
            }
            return null;
        }

        public Task<int> SavePatient(Patients patient)
        {
            if(patient.Id != 0)
            {
                return database.UpdateAsync(patient);
            }
            else
            {
                return database.InsertAsync(patient);
            }
        }

        public Task<int> DeletePatient(Patients patient)
        {
            return database.DeleteAsync(patient);
        }
    }
}
