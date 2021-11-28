using IBL.BO;
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
        BO.Drone GetCustomerBL(int id);
        IEnumerable<BO.Customer> GetAllCustomer();
        IEnumerable<BO.Customer> GetAllCustomerForList(Predicate<BO.Customer> predicate);
        #endregion

        #region Drone
       // void AddDrone(Drone drone);//add
       // BO.Drone GetDroneBL(int id);
       // IEnumerable<BO.Drone> GetAllDrone();
      //  IEnumerable<BO.Drone> GetAllDroneForList(Predicate<BO.Drone> predicate);
        #endregion

        #region Station
     //   BO.Station GetStationBL(int id);
      //  IEnumerable<BO.Station> GetAllStation();
     //   IEnumerable<BO.Station> GetAllStationForList(Predicate<BO.Station> predicate);
        #endregion

        #region Parcel
     //   BO.Parcel GetParcelBL(int id);
      //  IEnumerable<BO.Parcel> GetAllParcel();
     //   IEnumerable<BO.Parcel> GetAllParcelForList(Predicate<BO.Parcel> predicate);
        #endregion

    }
}
