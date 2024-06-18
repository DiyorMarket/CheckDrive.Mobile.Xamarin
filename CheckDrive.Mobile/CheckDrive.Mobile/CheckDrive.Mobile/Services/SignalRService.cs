using CheckDrive.Mobile.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CheckDrive.Mobile.Services
{
    public class SignalRService
    {
        public ICommand SendResponseCommand { get; set; }

        private HubConnection _hubConnection;

        public SignalRService()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://2bvq12nl-7111.euw.devtunnels.ms/api/chat", options =>
                {
                    options.AccessTokenProvider = async () => await GetTokenAsync();
                })
            .Build();

            _hubConnection.On<string>("ReceiveMessage", async (message) =>
            {
                await ShowPopupAsync(message);
            });
        }

        public async Task SendResponse(bool isAccepted)
        {
            try
            {
                _hubConnection.SendAsync("ReceivePrivateResponse", isAccepted);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task StartConnectionAsync()
        {
            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("Соединение установлено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public async Task StopConnectionAsync()
        {
            try
            {
                await _hubConnection.StopAsync();
                Console.WriteLine("Соединение закрыто.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private async Task ShowPopupAsync(string message)
        {
                var popup = new CheckControlPopup(message);
                await PopupNavigation.Instance.PushAsync(popup);
        }


        private async Task<string> GetTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync("tasty-cookies");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении токена: {ex.Message}");
                return string.Empty;
            }
        }

    }
}
