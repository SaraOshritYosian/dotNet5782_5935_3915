using System;
using System.Collections.Generic;
using System.Text;
using DAL.DO;

namespace DAL
{
    public interface IDAL 
    {
        #region Customer
        DO.Customer GetCustomers(int id);//get
        void addCustomer(DO.Customer Customer);//add
        void updetCustomer(DO.Customer Customer);//updet
        void deleteCustomer(int id);//delete

        #endregion
        #region Drone
        DO.Drone GetDrone(int id);//get
        void addDrone(DO.Drone Drone);//add
        void updetDrone(DO.Drone Drone);//updet
        void deleteCDrone(int id);//delete

        #endregion
        #region DroneCharge
        DO.DroneCharge GetDroneCharge(int id);//get
        void addDroneCharge(DO.DroneCharge DroneCharge);//add
        void updetDroneCharge(DO.DroneCharge DroneCharge);//updet
        void deleteDroneCharge(int id);//delete


        #endregion
        #region Parcel


        #endregion
        #region Station
        DO.Drone GetDrone(int id);//get
        void addStation(DO.Station Station);//add
        void updetStation(DO.Station Station);//updet
        void deleteStation(int id);//delete

        #endregion
    }


}
