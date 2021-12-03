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
        public BO.Parcel GetParcel(int id)//v
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


        private IDAL.DO.Parcel minDis(List<IDAL.DO.Parcel> parcels, Location location)
        {
            List<double> listDis = new List<double>();
            foreach (var obj in parcels)
            {

                Location locationSender = GetCustomer(obj.Senderld).location;//לסדר פעולתGetCustomer 
                listDis.Add(getdis(location, locationSender));//getdisצריך פעולת 
            }
            return parcels[listDis.FindIndex(x => x == listDis.Min())];
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
            for (int i = 0; i < BLDrones.Count; i++)
            {
                if (BLDrones[i].Id == id)
                {
                    DroneToList drone = BLDrones[i];//.Find(d => d.Id == id);
                    if (drone == default(DroneToList))
                        throw new ArgumentException("Drone with the given ID number doesn't exist");

                    if (drone.StatusDrone != StatusDrone.delivered)
                        throw new InvalidOperationException("The drone is not assigned to any package");

                    var parcel = accessIDal.GetParcel(drone.IdParcel);
                    if (parcel.Scheduled == default(DateTime) || parcel.PichedUp != default(DateTime))
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
            }
        }
    }
}
