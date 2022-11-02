using PatientListApp1.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PatientListApp1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
           
            Routing.RegisterRoute(nameof(PatientDetailPage), typeof(PatientDetailPage));
        }

    }
}
