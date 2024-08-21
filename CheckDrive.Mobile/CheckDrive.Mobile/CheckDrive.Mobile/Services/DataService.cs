﻿using CheckDrive.ApiContracts.Driver;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;

namespace CheckDrive.Mobile.Services
{
    public static class DataService
    {
        private const string securetyKey = "accountData";
        private const string securetyKeySavedDate = "savedDate";
        private const string securetyKeySavedTokenDate = "savedTokenDate";
        private const string securetyKeyToken = "tasty-cookies";
        private const string signalRKeyStatus = "signalRSatusdataurl";
        private const string signalRKeyReviewId = "signalRdataurl";

        public static void SaveAccount(DriverDto account)
        {
            try
            {
                var jsonDateTime = JsonConvert.SerializeObject(DateTime.Now);
                var json = JsonConvert.SerializeObject(account);

                SecureStorage.Remove(securetyKey);
                
                SecureStorage.SetAsync(securetyKey, json);
                SecureStorage.SetAsync(securetyKeySavedDate, jsonDateTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving {securetyKey}: {ex.Message}");
            }
        }

        public static void SaveToken(string token)
        {
            var jsonDateTime = JsonConvert.SerializeObject(DateTime.Now);
            try
            {
                SecureStorage.Remove(securetyKeyToken);

                SecureStorage.SetAsync(securetyKeyToken, token);
                SecureStorage.SetAsync(securetyKeySavedTokenDate, jsonDateTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving token. {ex.Message}");
            }
        }
        public static DriverDto GetAccount()
        {
            try
            {
                if (SecureStorage.GetAsync(securetyKey).GetAwaiter().GetResult() != null)
                {
                    var json = SecureStorage.GetAsync(securetyKey).GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<DriverDto>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting {securetyKey}: {ex.Message}");
            }
            return null;
        }

        public static DateTime GetCreationDate()
        {
            try
            {
                if (SecureStorage.GetAsync(securetyKeySavedDate).GetAwaiter().GetResult() != null)
                {
                    var json = SecureStorage.GetAsync(securetyKeySavedDate).GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<DateTime>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting {securetyKeySavedDate}: {ex.Message}");
            }
            return new DateTime();
        }

        public static DateTime GetTokenCreationDate()
        {
            try
            {
                if (SecureStorage.GetAsync(securetyKeySavedTokenDate).GetAwaiter().GetResult() != null)
                {
                    var json = SecureStorage.GetAsync(securetyKeySavedTokenDate).GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<DateTime>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting {securetyKeySavedTokenDate}: {ex.Message}");
            }
            return new DateTime();
        }

        public static void RemoveAllAcoountData()
        {
            try
            {
                if (SecureStorage.GetAsync(securetyKey).GetAwaiter().GetResult() != null 
                    && SecureStorage.GetAsync(securetyKeySavedDate).GetAwaiter().GetResult() != null)
                {
                    SecureStorage.Remove(securetyKey);
                    SecureStorage.Remove(securetyKeySavedDate);
                    SecureStorage.Remove(securetyKeyToken);
                    SecureStorage.Remove(securetyKeySavedTokenDate);
                    Console.WriteLine("file successfuly deleted");
                    return;
                }

                Console.WriteLine("file with this name cannot fined");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting: {ex.Message}");
            }
        }

        public static void SaveSignalRDataFOrStatus(int status)
        {
            try
            {
                SecureStorage.Remove(signalRKeyStatus);
                SecureStorage.SetAsync(signalRKeyStatus, status.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving token. {ex.Message}");
            }
        }

        public static void SaveSignalRDataForReviewID(int reviewid)
        {
            try
            {
                SecureStorage.Remove(signalRKeyReviewId);
                SecureStorage.SetAsync(signalRKeyReviewId, reviewid.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving token. {ex.Message}");
            }
        }

        public static (int statusNumber, int reviewId) GetSignalRData()
        {
            try
            {
                int status = 0;
                int reviewId = 0;

                if (SecureStorage.GetAsync(signalRKeyStatus).GetAwaiter().GetResult() != null)
                {
                    var json = SecureStorage.GetAsync(signalRKeyStatus).GetAwaiter().GetResult();
                    status = JsonConvert.DeserializeObject<int>(json);
                }
                if (SecureStorage.GetAsync(signalRKeyReviewId).GetAwaiter().GetResult() != null)
                {
                    var json = SecureStorage.GetAsync(signalRKeyReviewId).GetAwaiter().GetResult();
                    reviewId = JsonConvert.DeserializeObject<int>(json);
                }
                return (status, reviewId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting {securetyKeySavedTokenDate}: {ex.Message}");
            }
            return (0, 0);
        } 

        public static void RemoveSignalRData()
        {
            try
            {
                if (SecureStorage.GetAsync(signalRKeyStatus).GetAwaiter().GetResult() != null)
                {
                    SecureStorage.Remove(signalRKeyStatus);
                    Console.WriteLine("file successfuly deleted");
                }
                if (SecureStorage.GetAsync(signalRKeyReviewId).GetAwaiter().GetResult() != null)
                {
                    SecureStorage.Remove(signalRKeyReviewId);
                    Console.WriteLine("file successfuly deleted");
                    return;
                }

                Console.WriteLine("file with this name cannot fined");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting: {ex.Message}");
            }
        }
    }
}
