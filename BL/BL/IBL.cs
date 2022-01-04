using enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace IBL
{
    public interface IBL
    {
        #region Customer
         BO.Customer GetCustomer(int id);
         void UpdateCustomer(int id, string name, string phone);
         void AddCustomer(Customer customer);
      
        
        #endregion

        #region Drone

        BO.Drone GetDrone(int id);
        void AddDrone(Drone drone,int cod);//add
        void UpdateDrone(int id, string name);
        void SendingDroneToCharging(int droneId);
         void ReleaseDrone(int id, TimeSpan time);
       
       
        #endregion

        #region Station
        /*BO.*/Station GetStation(int id);
        void AddStation(Station station);
         void UpdateStation(int idS, int names, int chargeSlote);
     

        #endregion

        #region Parcel
        BO.Parcel GetParcel(int id);
         void AddParcel(BO.Parcel parcel);
        public void PackageDeliveryByDrone(int Id);//הספקה
         void PickUpPackage(int id);//איסוף
        void AssignPackageToDrone(int id);//שיוך

        #endregion

    }
}
