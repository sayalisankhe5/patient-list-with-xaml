using PatientListApp1.Data;
using PatientListApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace PatientListApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientDetailPage : ContentPage
    {
        private Patients _p; 
        public PatientDetailPage()
        {
            InitializeComponent();
            pAge.Text = "0";
            Title = "Add new patient";
        }

        public PatientDetailPage(Patients p,bool tapped)
        {
            InitializeComponent();
            _p = p;
            pName.Text = _p.Name;
            pEmail.Text = _p.Email;
            pPhone.Text = _p.PhoneNumber.ToString();
            pDOB.Date = _p.DOB;
            pAge.Text = _p.Age.ToString();
            Delete.IsVisible = true;
            Title = "Edit Info";
            if(tapped)
            {
                pName.IsReadOnly = true;
                pEmail.IsReadOnly = true;
                pPhone.IsReadOnly = true;
                pDOB.IsEnabled = false;
                Save.IsVisible = false;
                Title = "View Info";
            }
        }

        

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            //var patient = (Patients)BindingContext;
            bool isValid = false;
            PatientsDatabase patientsDb = await PatientsDatabase.dbInstance;
            Regex emailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            Regex phoneRegex = new Regex(@"^[0-9]{10}$");

            if (pName.Text != null && pPhone.Text != null && pEmail.Text != null)
            {
                if (emailRegex.IsMatch(pEmail.Text) && phoneRegex.IsMatch(pPhone.Text))
                {
                    isValid = true;
                }
                else if (!phoneRegex.IsMatch(pPhone.Text))
                {
                    isValid = false;
                    await DisplayAlert("Incorrect Data", "Phone number is in incorrect format.Please enter correct data.", "OK");
                }
                else if (!emailRegex.IsMatch(pEmail.Text))
                {
                    isValid = false;

                    await DisplayAlert("Incorrect Data", "Email is in incorrect format.Please enter correct data.", "OK");
                }
            }
            else if (pName.Text == null)
            {
                isValid = false;

                await DisplayAlert("Incorrect Data", "Please enter your name", "OK");
            }
            else if (pPhone.Text == null)
            {
                isValid = false;

                await DisplayAlert("Incorrect Data", "Please enter your Phone number", "OK");
            }
            else if (pEmail.Text == null)
            {
                isValid = false;

                await DisplayAlert("Incorrect Data", "Please enter your Email address", "OK");
            }
            if(isValid)
            {
                if (_p != null)
                {
                    await patientsDb.SavePatient(_p);
                    await Navigation.PopAsync();
                }
                else
                {
                    var patient = new Patients
                    {
                        Name = pName.Text,
                        Email = pEmail.Text,
                        PhoneNumber = long.Parse(pPhone.Text),
                        DOB = pDOB.Date,
                        Age = int.Parse(pAge.Text)
                    };
                    var existingPhoneNumber = await patientsDb.GetPatientByPhoneNumber(patient.PhoneNumber);
                    var existingEmail = await patientsDb.GetPatientByEmail(patient.Email);
                    if (existingPhoneNumber != null)
                    {
                        isValid = false;
                        await DisplayAlert("Incorrect Data", "Phone number already exists.Please enter new data.", "OK");
                    }
                    else if (existingEmail != null)
                    {
                        isValid = false;
                        await DisplayAlert("Incorrect Data", "Email already exists.Please enter new data.", "OK");
                    }
                    else
                    {
                        await patientsDb.SavePatient(patient);
                        await Navigation.PopAsync();
                    }
                }
                
            }
                     
        }

          
        

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            
            if(_p != null)
            {
                bool answer = await DisplayAlert("Delete", "Do you want to delete this Patient?", "Yes", "No"); 
                if(answer == true)
                {
                    PatientsDatabase patientsDb = await PatientsDatabase.dbInstance;
                    await patientsDb.DeletePatient(_p);
                    await Navigation.PopAsync();

                }
            }
        }

        private void pDOB_DateSelected(object sender, DateChangedEventArgs e)
        {
            int years = DateTime.Now.Year - e.NewDate.Year;
            pAge.Text = years.ToString();
        }
    }
}