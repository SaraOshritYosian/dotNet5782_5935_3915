using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    public partial class BL
    {

        public BO.PackageInTransfer GetParcelInTransfer(int idD)//צריך מצב החבילה וזה בול וצריך לחשב מרחק
        {
            BO.PackageInTransfer bop;
            try
            {
                IDAL.DO.Parcel dop = accessIDal.GetParcelByDrone(idD);//parcel from dalObect

                IDAL.DO.Drone d = accessIDal.GetDrone(dop.Droneld);//drone from dalObject
                bop = new BO.PackageInTransfer()
                {
                    Id = dop.Id,
                    PriorityParcel = (Priority)dop.Priority,
                    Weight = (WeightCategories)dop.Weight,
                    CustomerInParcelSender = new CustomerInParcel() { Id = dop.Senderld, Name = GetCustomer(dop.Senderld).Name },
                    CustomerInParcelTarget = new CustomerInParcel() { Id = dop.Targetld, Name = GetCustomer(dop.Targetld).Name },
                    Collection = GetCustomer(dop.Senderld).LocationOfCustomer,
                    DeliveryDestination = GetCustomer(dop.Targetld).LocationOfCustomer,
                    far =//צריך לחשב את המרחק

                };
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bop;

        }

        public BO.Parcel GetParcel(int id)//v
        {
            BO.Parcel bop;
            try
            {
                IDAL.DO.Parcel dop = accessIDal.GetParcel(id);
                DroneToList bo = BlDrone.Find(p => p.Id == dop.Droneld);
                IDAL.DO.Drone d = accessIDal.GetDrone(dop.Droneld);
                bop = new BO.Parcel()
                {
                    Id = dop.Id,
                    CustomerInParcelSender = new CustomerInParcel() { Id = accessIDal.GetParcel(id).Senderld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(id).Senderld).Name },
                    CustomerInParcelTarget = new CustomerInParcel() { Id = accessIDal.GetParcel(id).Targetld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(id).Targetld).Name },
                    Weight = (WeightCategories)dop.Weight,
                    Priority = (Priority)dop.Priority,
                    Requested = dop.Requested,
                    Delivered = dop.Delivered,
                    PichedUp = dop.PichedUp,
                    Scheduled = dop.Scheduled,
                    DroneInParcel = new DroneInParcel() { Id = d.Id, StatusBatter = GetDrone(d.Id).StatusBatter, LocationDroneInParcel = bo.LocationDrone }
                };
            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bop;

        }
        public void AddParcel(BO.Parcel parcel)//v
        {
            IDAL.DO.Parcel p = new IDAL.DO.Parcel();
            p.Senderld = parcel.CustomerInParcelSender.Id;
            p.Targetld = parcel.CustomerInParcelTarget.Id;
            p.Weight = (IDAL.DO.WeightCategories)parcel.Weight;
            p.Priority = (IDAL.DO.Priority)(WeightCategories)parcel.Priority;
            p.Requested = DateTime.Now;
            p.Scheduled = new DateTime(0);
            p.PichedUp = new DateTime(0) ;
            p.Delivered = new DateTime(0);
           
            try
            {
                accessIDal.AddParcel(p);
            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }

        
        public void PickUpPackage(int id)//איסוף חבילה על ידי רחפן
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");

            DroneToList drone = BLDrones.Find(d => d.Id == id);
            if (drone == default(DroneToList))
                throw new ArgumentException("Drone with the given ID number doesn't exist");

            if (drone.StatusDrone != StatusDrone.delivered)
                throw new InvalidOperationException("The drone is not assigned to any package");

            var parcel = id.GetParcel(drone.IdParcel);
            if (parcel.Scheduled == default(DateTime) || parcel.PickedUp != default(DateTime))//ביקשתי את החריגה הזאת
                throw new InvalidOperationException("The package is not ready to pick up");

            try
            {

                var sender = accessIDal.GetCustomer(parcel.Senderld);
                double distance = DistanceTo(drone.LocationDrone.Latitude, sender.Lattitude,
                    drone.LocationDrone.Longitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
                drone.LocationDrone = new Location//עדכון מיקום למיקום שולח
                {
                    Latitude = sender.Lattitude,
                    Longitude = sender.Longitude
                };
                drone.StatusBatter -= BatteryConsumption(distance, drone.Weight);
                accessIDal.PackageCollectionByDrone(parcel.Id);

            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }
            private IDAL.DO.Parcel minDis(List<IDAL.DO.Parcel> parcels, Location location)
        {
            List<double> listDis = new List<double>();
            foreach (var obj in parcels)
            {

                Location locationSender = GetCustomer(obj.Senderld).LocationOfCustomer;//לסדר פעולתGetCustomer 
                listDis.Add(getdis(location, locationSender));//getdisצריך פעולת 
            }
            return parcels[listDis.FindIndex(x => x == listDis.Min())];
        }


        public BO.ParcelToLIst ParcelToList(int idp)//יש ליצור פונקציה שמחזירה מצב חבילה
        {
            BO.ParcelToLIst bop;
            try
            {
                IDAL.DO.Parcel dop = accessIDal.GetParcel(idp);
                bop = new BO.ParcelToLIst()
                {
                    Id = dop.Id,
                   SenderName=GetCustomer( dop.Senderld).Name,
                   TargetName = GetCustomer(dop.Targetld).Name,
                   Priority= (Priority)dop.Priority,
                   Weight= (WeightCategories)dop.Weight,
                   statusParcel=
                   
                };

            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bop;

        }
        private StatusParcel StatuseParcelKnow(int idP) {
            IDAL.DO.Parcel dop = accessIDal.GetParcel(idP);

            if (dop.Delivered.Date !=0)//סופק
                return StatusParcel.provided;

            if (dop.PichedUp != null)//נאסף
                return StatusParcel.collected;

            if (dop.Scheduled != null)//שוייך
                return StatusParcel.associated;
            return StatusParcel.Created;//נוצר
        }
    }

    
}
