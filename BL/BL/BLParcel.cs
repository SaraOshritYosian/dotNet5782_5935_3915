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
        public BO.Parcel GetParcel(int id)//x
        {
            BO.Parcel bop = new BO.Parcel();
            try
            {
                IDAL.DO.Parcel dop = accessIDal.GetParcel(id);

                IDAL.DO.Drone d = accessIDal.GetDrone(dop.Droneld);

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
            p.Senderld = parcel.Senderld.Id;
            p.Targetld = parcel.Targetld.Id;
            p.Weight = (IDAL.DO.WeightCategories)parcel.Weight;
            p.Priority = (IDAL.DO.Priority)(WeightCategories)parcel.Priority;
            p.Requested = DateTime.Now;
            p.Scheduled = new DateTime();
            p.PichedUp = new DateTime();
            p.Delivered = new DateTime();
            try
            {

            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }

        public IEnumerable<BO.Parcel> ParcelList() {
            return from item in accessIDal.pparcelList()
                   select GetParcel(item.Id);
        }

        //איסוף חבילה ע"י רחפן
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


        public IEnumerable<BO.ParcelToLIst> GetALLParcelToList()
        {
            ParcelToLIst parcel = new ParcelToLIst();
            foreach (IDAL.DO.Parcel item in accessIDal.GetAllParcel())
            {
                parcel.Id = item.Id;
                parcel.Priority = (Priority)item.Priority;
                parcel.Senderld = accessIDal.GetCustomer(item.Senderld).Name;
                parcel.Targetld = accessIDal.GetCustomer(item.Targetld).Name;
                parcel.Weight = (WeightCategories)(item.Weight);
                parcelBl.Add(parcel);

            }
            return parcelBl;
        }
    }
}
