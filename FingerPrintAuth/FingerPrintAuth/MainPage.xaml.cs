using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FingerPrintAuth
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var auth = await GetAuthentication();
            if (auth)
            {
                stack.IsVisible = true;
            }
        }

        private async Task<bool> GetAuthentication()
        {
            var availability = await CrossFingerprint.Current.GetAvailabilityAsync(true);
            if (availability == FingerprintAvailability.Available)
            {
                var authenticate = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Authentication required", "Touch the fingerprint sensor") { AllowAlternativeAuthentication = true });

                if (authenticate.Status == FingerprintAuthenticationResultStatus.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
