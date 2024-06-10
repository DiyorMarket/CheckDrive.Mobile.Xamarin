﻿using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Responses;

namespace CheckDrive.Web.Stores.Drivers
{
    public interface IDriverDataStore
    {
        GetDriverResponse GetDrivers(int accountId);
        DriverDto GetDriver(int id);
    }
}