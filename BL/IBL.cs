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

         void UpdateCustomer(int id, string name, string phone);
         void AddCustomer(Customer customer);
        //IEnumerable<BO.Customer> GetAllCustomer();
        //IEnumerable<BO.Customer> GetAllCustomerForList(Predicate<BO.Customer> predicate);
        #endregion

        #region Drone
        void SendingDroneforCharging(int droneId);
         BO.Drone GetDrone(int id);
         IEnumerable<BO.Drone> GetAllDrone();
        void AddDrone(Drone drone,int cod);//add
        void UpdateDrone(Drone drone);
        
        #endregion

        #region Station
        void AddStation(Station station);
         void UpdateStation(int idS, int names, int chargeSlote);
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
