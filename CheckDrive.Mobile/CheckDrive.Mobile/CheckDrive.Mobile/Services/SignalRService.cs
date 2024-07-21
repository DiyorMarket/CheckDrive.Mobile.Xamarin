using CheckDrive.Mobile.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

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

            _hubConnection.On<int, int, string>("ReceiveMessage", async (status, reviewId, message) =>
            {
                DataService.SaveSignalRDataFOrStatus(status);
                    DataService.SaveSignalRDataForReviewID(reviewId);
                await ShowPopupAsync(message);
            });
        }

        public async Task SendResponse(bool isAccepted)
        {
            var signalRData = DataService.GetSignalRData();
            var status = signalRData.statusNumber;
            int reviewId = signalRData.reviewId;
            try
            {
                await Task.Delay(2000);
                await _hubConnection.SendAsync("ReceivePrivateResponse",status, reviewId, isAccepted);
                DataService.RemoveSignalRData();
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
                Console.WriteLine($"Error: {ex.Message}");
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
            await SecureStorage.SetAsync("popup_message", message);
            await SecureStorage.SetAsync("popup_visible", "true");

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
