﻿using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Responses;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Drivers
{
    public interface IDriverDataStore
    {
        Task<GetDriverResponse> GetDriversAsync(int accountId);
        Task<DriverDto> GetDriverAsync(int id);
    }
}