using Xamarin.Essentials;

namespace CheckDrive.Mobile.Services
{
    public static class ConnectivityService
    {
        public static bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }
    }
}
