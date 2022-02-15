using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        #region Customer
        Customer GetCustomer(int id);
        void UpdateCustomer(int id, string name, string phone);
        void AddCustomer(Customer customer);
        IEnumerable<BO.ParcelToLIst> GetParcels();
        IEnumerable<BO.CustomerToList> GetCustomers();
         BO.CustomerToList CostumerToListToPrint(int idc);
        IEnumerable<BO.ParcelInCustomer> ListParcelToCustomer(int idc);
        IEnumerable<BO.ParcelInCustomer> ListParcelFromCustomers(int idc);
        IEnumerable<string> GetCustomersName();

        #endregion

        #region Drone
        IEnumerable<BO.DroneToList> GetDrons();
        
        Drone GetDrone(int id);
        void AddDrone(Drone drone, int cod);//add
        void UpdateDrone(int id, string name);
        void SendingDroneToCharging(int droneId);
        void ReleaseDrone(int id, TimeSpan time);
        double GetFarCalculatesTheSmallestDistance(int idDr);
       // IEnumerable GetParcels();
        DO.Station GetStationCalculatesTheSmallestDistance(int idDr);
        double returnMinDistancFromLicationToStation(BO.Location lo);
        double DistanceToFromStationToDroneLocation(double lat1, double lon1, double lat2, double lon2, char unit = 'K');
        BO.DroneToList DroneToLisToPrint(int id);
        int BatteryConsumption(double kilometrs, DO.WeightCategories weightcategories);
        double BatteryConsumption(double kilometrs);

        IEnumerable<BO.DroneToList> GetDronsByWeight(WeightCategories weightcategories);
        #endregion

        #region Station
        DO.Station GetStationCalculatesTheSmallestDistance(BO.Location location);
        IEnumerable<BO.DroneToList> GetDronesByPerdicate(Predicate<BO.DroneToList> predicate);
        BO.StationToList StationToListToPrint(int id);
        BO.Station GetStationByDrone(int idd);
        Station GetStation(int id);
        void AddStation(Station station);
        void UpdateStation(int idS, int names, int chargeSlote);
        IEnumerable<int> AvailableStationToChargeListt();
        IEnumerable<BO.Station> AvailableStationToChargeList();
        IEnumerable<BO.StationToList> GetStations();
        IEnumerable<BO.StationToList> AvailableStationToChargeListStationToList();
        #endregion

        #region Parcel
        BO.ParcelToLIst ParcelToListToPrint(int idp);
        void DeleteParcel(int idD);
        Parcel GetParcel(int id);
        int AddParcel(BO.Parcel parcel);
        public void PackageDeliveryByDrone(int Id);//הספקה
        void PickUpPackage(int id);//איסוף
        void AssignPackageToDrone(int id);//שיוך
        BO.StatusParcel StatuseParcelKnow(int idP);
        IEnumerable<DroneInCharge> ListDroneInStation(int idS);
        IEnumerable<BO.ParcelToLIst> GetParcelByPerdicate(Predicate<BO.ParcelToLIst> predicate);
        #endregion

    }
}
