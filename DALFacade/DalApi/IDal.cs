
using System;
using System.Collections.Generic;

namespace DalApi
{
    public interface IDal
    {

        #region Customer
        IEnumerable<int> ListTargetParcel(int idta);
        IEnumerable<int> ListSendetParcel(int idse);
        IEnumerable<DO.Customer> GetAllCustomer();
        IEnumerable<DO.Customer> GetAllCustomerBy(Predicate<DO.Customer> predicate);
        DO.Customer GetCustomer(int id);//get
        void AddCustomer(DO.Customer customer);//add
        void UpdetCustomer(DO.Customer customer);//updet
        void UpdetCustomer(int id, Action<DO.Customer> action);//updet
        void DeleteCustomer(int id);//delete
        IEnumerable<DO.Customer> CcustomerList();
        #endregion

        #region Drone
        IEnumerable<DO.Drone> DdroneList();
        int GetDroneDoSndByParcelId(int idp);
        IEnumerable<DO.Drone> GetAllDrone();
        IEnumerable<DO.Drone> GetAllDroneBy(Predicate<DO.Drone> predicate);
        DO.Drone GetDrone(int id);//get
        void AddDrone(DO.Drone drone);//add
        void UpdetDrone(DO.Drone drone);//updet
        void UpdetDrone(int id, Action<DO.Drone> action);//updet
        void DeleteDrone(int id);//delete
        double[] RequestPowerConsuption();
         int MoneDroneChargByStationListInt(int ids);
        IEnumerable<int> GetDroneChargByStationListInt(int ids);
        IEnumerable<int> GetListDrone();
        int CoutCharge(int id);
         IEnumerable<double> ElectricityUse();
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
        DO.DroneCharge GetDroneChargByStation(int id);
        void SendDroneToCharge(int station, int drone);
        void ReleaseDroneFromCharging(int drone);


        #endregion

        #region Parcel
       
        IEnumerable<DO.Parcel> GetAllParcel();
        IEnumerable<DO.Parcel> GetAllParcelBy(Predicate<DO.Parcel> predicate);
        DO.Parcel GetParcel(int id);//get
        void AddParcel(DO.Parcel parcel);//add
        void UpdetParcel(DO.Parcel parcel);//updet
        void UpdetParcel(int id, Action<DO.Parcel> action);//updet
        void DeleteParcel(int id);//delete
        void AssignPackageToDrone(int idParcel, int idDrone);
       
        DO.Parcel GetParcelByDrone(int id);
       
        void PackageCollectionByDrone(int idParcel);
        IEnumerable<DO.Parcel> PparcelList();

        #endregion

        #region Station
        IEnumerable<DO.Station> GetAllStation();
        IEnumerable<DO.Station> GetAllStationBy(Predicate<DO.Station> predicate);
        DO.Station GetStation(int id);//get
        void AddStation(DO.Station station);//add
        void UpdetStation(DO.Station station);//updet
        void UpdetStation(int id, Action<DO.Station> action);//updet
        void DeleteStation(int id);//delete 
        void DeliveryOfPackageToTheCustomer(int idParcel);
        IEnumerable<DO.Drone> ListDroneInStation(int id);
        IEnumerable<DO.Station> ReturnStationHaveFreeCharde();
        IEnumerable<DO.Station> SStationList();

        #endregion
    }
}
