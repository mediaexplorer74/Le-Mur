﻿using le_mur.Consts;
using le_mur.Helpers;
using le_mur.NetworkCalling;
using le_mur.View;
using le_mur.View.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using le_mur.View.Folders;
using le_mur.View.Folders.Timer;
using Xamarin.Forms;

namespace le_mur.ViewModel.Auth
{
    public class StartViewModel : BaseViewModel 
    {
        public INavigation Navigation { get; set; }

        public StartViewModel()
        {
            Auth();
        }

        private async void Auth()
        {
            AuthStatus status;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                status = await TelegramApi.CheckAuth();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"An error occurred: {ex.Message}");
                status = AuthStatus.NeedAuth;

                //RnD: temporary set your phone number here
                PreferencesHelper.SetPhoneNumber("");
            }

            stopwatch.Stop();

            TimeSpan elapsedTime = stopwatch.Elapsed;

            if (elapsedTime < TimeSpan.FromSeconds(3))
                await Task.Delay(TimeSpan.FromSeconds(3) - elapsedTime);

            switch (status)
            {
                case AuthStatus.Ok: 
                    await Navigation.PushAsync(new FeedPage()); 
                    break;
                case AuthStatus.NeedAuth: 
                    await Navigation.PushAsync(new NumberPage()); 
                    break;
                case AuthStatus.NeedCode:
                    string PN = PreferencesHelper.GetPhoneNumber();
                    await Navigation.PushAsync(new CodePage(PN));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
