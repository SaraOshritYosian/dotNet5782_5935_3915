using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL
{
  public partial class BL : IBL
    {
        public IDAL.IDal accessIDal;
        public List<Drone> BLDrones;
        public static double Free;
        public static double LightWeight;
        public static double MediumWeight;
        public static double HeavyWeight;
        public static double LoadingPrecents;
        public Random rand = new Random(DateTime.Now.Millisecond);
        public BL()
        {
            accessIDal = new DalObject();
                double[] arr = new accessIDal.RequestPowerConsuptionByDrone();//מה זו הפונקציה  הזו
                Free = arr[0];
                LightWeight = arr[1];
                MediumWeight = arr[2];
                HeavyWeight = arr[3];
                LoadingPrecents = arr[4];
            BLDrones = new List<Drone>();
            List<IDAL.DO.Drone> DALDrones = accessIDal.GetDrone().ToList();//?
            foreach(var item in DALDrones)
            {
                BLDrones.Add(new Drone { Id = item.Id, Model = item.Model, Weight = (WeightCategories)item.Weight });//weightcategories
            }
            List<Customer> BLCustomer = new List<Customer>();
            List<IDAL.DO.Customer> DALCustomer = accessIDal.GetCustomer().ToList();//?
            foreach (var item in DALCustomer)
            {
                BLCustomer.Add(new Customer { Id = item.Id, Name = item.Name, Pone=item.Pone,LocationOfCustomer=new Location() { Longitude = item.Longitude, Latitude = item.Latitude } });//lattitud with one t
            }
            List<Station> BLStation = new List<Station>();
            List<IDAL.DO.Station> DALStation = accessIDal.GetStation().ToList();//?
            foreach (var item in DALStation)
            {
                BLStation.Add(new Station { Name = item.Name, Id = item.Id, ChargeSlots = item.ChargeSlots, LocationOfStation = new Location() { Longitude = item.Longitude, Latitude = item.Latitude } });//lattitud with one t
            }
            List<IDAL.DO.Parcel> DALParcel = accessIDal.GetParcel().ToList();//?
            foreach(var item in BLDrones)
                    {
                int index = DALParcel.FindIndex(x=>x.Droneld==item.Id&&x.Delivered==DateTime.MinValue);
                if(index!=-1)
                {
                    item.Statuse = DroneStatuse.busy;
                    Location senderLocation = BLCustomer.Find(x => x.Id == DALParcel[index].Targetld).LocationOfCustomer;
                    double distanceBsenderAreciever = GetDistance(senderLocation, recieverLocation);
                    double distanceBrecieverAstation = minDistanceBetween....(BLStation, recieverLocation).item2;//?
                    double electricityUse = distanceBrecieverAstation * Free;
                    switch((weightCategories)DALParcel[index].Weight)
                    {
                        case weightCategories.light:
                            electricityUse += distanceBsenderAreciever * LightWeight;
                            break;
                        case weightCategories.mediume:
                            electricityUse += distanceBsenderAreciever * MediumWeight;
                            break;
                        case weightCategories.heavy:
                            electricityUse += distanceBsenderAreciever * HeavyWeight;
                            break;
                            if(DALParcel[index].PichedUp==DateTime.MinValue)
                            {
                                item.CurrentLocation = minDistanceBetween....(BLStation, senderLocation).item1;

                                electricityUse += GetDistance(item.CurrentLocation, senderLocation) * Free;
                            }
                            else 
                            {
                                item.CurrentLocation = senderLocation;
                            }

                    }

                }
            }
        }

    }
}
