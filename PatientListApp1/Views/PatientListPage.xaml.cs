using PatientListApp1.Data;
using PatientListApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PatientListApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientListPage : ContentPage
    {
        public PatientListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            PatientsDatabase patientsDb = await PatientsDatabase.dbInstance;
             
            patientList.ItemsSource = await patientsDb.GetAllPatients();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(PatientDetailPage));
            await Navigation.PushAsync(new PatientDetailPage());
        }

        private async void PatientList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item;
            var patient = item as Patients;
            //await Shell.Current.GoToAsync(nameof(PatientDetailPage(patient)));
            await Navigation.PushAsync(new PatientDetailPage(patient, true));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var patient = item.CommandParameter as Patients;
            //await Shell.Current.GoToAsync(nameof(PatientDetailPage(patient)));
            await Navigation.PushAsync(new PatientDetailPage(patient,false));
        }
    }
}