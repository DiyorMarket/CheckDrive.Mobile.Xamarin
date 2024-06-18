﻿using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Responses;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Drivers
{
    public interface IDriverDataStore
    {
        GetDriverResponse GetDriversAsync(int accountId);
        DriverDto GetDriverAsync(int id);
    }
}