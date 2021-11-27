using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    public interface IBL
    {
        #region Customer
        BO.DroneBL GetCustomerBL(int id);
        IEnumerable<BO.CustomerBL> GetAllCustomer();
        IEnumerable<BO.CustomerBL> GetAllCustomerForList(Predicate<BO.CustomerBL> predicate);
        #endregion

        #region Drone
        BO.DroneBL GetDroneBL(int id);
        IEnumerable<BO.DroneBL> GetAllDrone();
        IEnumerable<BO.DroneBL> GetAllDroneForList(Predicate<BO.DroneBL> predicate);
        #endregion

        #region Station
        BO.StationBL GetStationBL(int id);
        IEnumerable<BO.StationBL> GetAllStation();
        IEnumerable<BO.StationBL> GetAllStationForList(Predicate<BO.StationBL> predicate);
        #endregion

        #region Parcel
        BO.ParcelBL GetParcelBL(int id);
        IEnumerable<BO.ParcelBL> GetAllParcel();
        IEnumerable<BO.ParcelBL> GetAllParcelForList(Predicate<BO.ParcelBL> predicate);
        #endregion

    }
}
