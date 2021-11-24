using System;
using System.Collections.Generic;
using System.Text;
//using DAL.DO;
/// <summary>
//,,,,
/// </summary>

namespace DAL
{
    public interface IDAL 
    {
        #region Customer
        IEnumerable<DO.Customer> GetAllCustomer();
        IEnumerable<DO.Customer> GetAllCustomerBy(Predicate<DO.Customer> predicate);
        DO.Customer GetCustomers(int id);//get
        void addCustomer(DO.Customer customer);//add
        void updetCustomer(DO.Customer customer);//updet
        void updetCustomer(int id, Action<DO.Customer> action);//updet
        void deleteCustomer(int id);//delete

        #endregion

        #region Drone
        IEnumerable<DO.Drone> GetAllDrone();
        IEnumerable<DO.Drone> GetAllDroneBy(Predicate<DO.Drone> predicate);
        IDAL.DO.Drone GetDrone(int id);//get
        void addDrone(DO.Drone drone);//add
        void UpdetDrone(DO.Drone drone);//updet
        void UpdetDrone(int id, Action<DO.Drone> action);//updet
        void deleteCDrone(int id);//delete
        ///.
        #endregion

        #region DroneCharge
        IEnumerable<DO.DroneCharge> GetAllDroneCharge();
        IEnumerable<DO.DroneCharge> GetAllDroneChargeBy(Predicate<DO.DroneCharge> predicate);
        DO.DroneCharge GetDroneCharge(int id);//get
        void addDroneCharge(DO.DroneCharge droneCharge);//add
        void updetDroneCharge(DO.DroneCharge droneCharge);//updet
        void updetDroneCharge(int id,Action<DO.DroneCharge> action);//updet
        void deleteDroneCharge(int id);//delete


        #endregion

        #region Parcel
        IEnumerable<DO.Parcel> GetAllParcel();
        IEnumerable<DO.Parcel> GetAllParcelBy(Predicate<DO.Parcel> predicate);
        DO.Parcel GetParcel(int id);//get
        void addParcel(DO.Parcel parcel);//add
        void updetParcel(DO.Parcel parcel);//updet
        void updetParcel(int id, Action<DO.Parcel> action);//updet
        void deleteParcel(int id);//delete


        #endregion

        #region Station
        IEnumerable<DO.Station> GetAllStation();
        IEnumerable<DO.Parcel> GetAllStationBy(Predicate<DO.Station> predicate);
        DO.Station GetStation(int id);//get
        void addStation(DO.Station station);//add
        void updetStation(DO.Station station);//updet
        void updetStation(int id, Action<DO.Station> action);//updet
        void deleteStation(int id);//delete

        #endregion
    }


}
