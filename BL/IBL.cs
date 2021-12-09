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
         BO.Customer GetCustomer(int id);
         void UpdateCustomer(int id, string name, string phone);
         void AddCustomer(Customer customer);
        void PrintCustomersList();
         void PrintCustomerById(int ids);
        #endregion

        #region Drone

        BO.Drone GetDrone(int id);
      //   IEnumerable<BO.Drone> GetAllDrone();
        void AddDrone(Drone drone,int cod);//add
        void UpdateDrone(int id, string name);
        void SendingDroneToCharging(int droneId);
         void ReleaseDrone(int id, double time);
         void PrintDroneList();
         void PrintDroneById(int ids);
        #endregion

        #region Station
        BO.Station GetStation(int id);
        void AddStation(Station station);
         void UpdateStation(int idS, int names, int chargeSlote);
         void PrintStationList();
        void PrintStationById(int ids);
         void PrintAvailableStationToChargeList();

        #endregion

        #region Parcel
        BO.Parcel GetParcel(int id);
         void AddParcel(BO.Parcel parcel);
         void PickUpPackage(int id);
      

        #endregion

    }
}
