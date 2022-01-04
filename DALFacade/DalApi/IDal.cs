using System;
using System.Collections.Generic;

namespace DalApi
{
    public interface IDal
    {

        #region Customer
        IEnumerable<DO.Customer> GetAllCustomer();
        IEnumerable<DO.Customer> GetAllCustomerBy(Predicate<DO.Customer> predicate);
        DO.Customer GetCustomer(int id);//get
        void AddCustomer(DO.Customer customer);//add
        void UpdetCustomer(DO.Customer customer);//updet
        void UpdetCustomer(int id, Action<DO.Customer> action);//updet
        void DeleteCustomer(int id);//delete

        #endregion

        #region Drone
        IEnumerable<DO.Drone> GetAllDrone();
        IEnumerable<DO.Drone> GetAllDroneBy(Predicate<DO.Drone> predicate);
        DO.Drone GetDrone(int id);//get
        void AddDrone(DO.Drone drone);//add
        void UpdetDrone(DO.Drone drone);//updet
        void UpdetDrone(int id, Action<DO.Drone> action);//updet
        void DeleteDrone(int id);//delete
        public IEnumerable<double> ElectricityUse();
        ///.
        #endregion

        #region DroneCharge
        IEnumerable<DO.DroneCharge> GetAllDroneCharge();
        IEnumerable<DO.DroneCharge> GetAllDroneChargeBy(Predicate<DO.DroneCharge> predicate);
        DO.DroneCharge GetDroneChargByDrone(int id);//get
        void AddDroneCharge(DO.DroneCharge droneCharge);//add
        void UpdetDroneCharge(DO.DroneCharge droneCharge);//updet
        void UpdetDroneCharge(int id, Action<DO.DroneCharge> action);//updet
                                                                     //  void DeleteDroneCharge(int id);//delete


        #endregion

        #region Parcel
        IEnumerable<DO.Parcel> GetAllParcel();
        IEnumerable<DO.Parcel> GetAllParcelBy(Predicate<DO.Parcel> predicate);
        DO.Parcel GetParcel(int id);//get
        void AddParcel(DO.Parcel parcel);//add
        void UpdetParcel(DO.Parcel parcel);//updet
        void UpdetParcel(int id, Action<DO.Parcel> action);//updet
        void DeleteParcel(int id);//delete


        #endregion

        #region Station
        IEnumerable<DO.Station> GetAllStation();
        IEnumerable<DO.Station> GetAllStationBy(Predicate<DO.Station> predicate);
        DO.Station GetStation(int id);//get
        void AddStation(DO.Station station);//add
        void UpdetStation(DO.Station station);//updet
        void UpdetStation(int id, Action<DO.Station> action);//updet
        void DeleteStation(int id);//delete
        void SendDroneToCharge(int id1, int id2);

        #endregion
    }
}
