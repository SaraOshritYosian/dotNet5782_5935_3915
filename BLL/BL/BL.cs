
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;


namespace BL
{
   public sealed partial class BL : IBL
    {
        static readonly IBL instance = new BL();
        public static IBL Instance { get => instance; }

        internal IDal dal = DalFactory.GetDal();
       

      //  public DalObject.DalObject accessIDal;
       // public IDAL.IDal accessIDal;אני חושבת שזה לא נכון
        public List<DroneToList> BlDrone;
        public static double Free;
        public static double LightWeight;
        public static double MediumWeight;
        public static double HeavyWeight;
        public static double LoadingPrecents;
        public Random rand = new Random(DateTime.Now.Millisecond);
        public BL()

        {
            accessIDal = new DalObject.DalObject();
           // accessIDal = new DalObject();
            double[] arr =  accessIDal.RequestPowerConsuption();// בקשת צריכת חשמל ע"י רחפן
            Free = arr[0];
            LightWeight = arr[1];
            MediumWeight = arr[2];
            HeavyWeight = arr[3];
            LoadingPrecents = arr[4];
             BlDrone = new List<DroneToList>();//רשימה של רחפנים בביאל
            //BLDrones = new List<Drone>();
            List<IDAL.DO.Drone> DALDrones = accessIDal.GetAllDrone().ToList();//רשימה של רחפנים מDAL
            foreach (var item in DALDrones)
            {
                BlDrone.Add(new DroneToList { Id = item.Id, Model = item.Model, Weight = (Enums.WeightCategories)item.Weight });//weightcategories
            }
            List<Customer> BLCustomer = new List<Customer>();
            List<IDAL.DO.Customer> DALCustomer = accessIDal.CcustomerList().ToList();//רשימה של לקוחות מDAL
            foreach (var item in DALCustomer)
            {
                BLCustomer.Add(new Customer { Id = item.Id, Name = item.Name, Pone = item.Pone, LocationOfCustomer = new Location() { Longitude = item.Longitude, Latitude = item.Lattitude } });//lattitud with one t
            }
            List<Station> BLStation = new List<Station>();
            List<IDAL.DO.Station> DALStation = accessIDal.SStationList().ToList();
            foreach (var item in DALStation)
            { 
                BLStation.Add(new Station { Name = item.Name, Id = item.Id, ChargeSlotsFree = item.ChargeSlots, LocationStation = new Location() { Longitude = item.Longitude, Latitude = item.Latitude } });//lattitud with one t
            }
            List<IDAL.DO.Parcel> DALParcel = accessIDal.PparcelList().ToList();//רשימה של חביחות מ DAL
            foreach (var item in BlDrone)
            {
                int index = DALParcel.FindIndex(x => x.Droneld == item.Id && x.Delivered == DateTime.MinValue);
                if (index != -1)
                {
                    item.StatusDrone = Enums.StatusDrone.delivered;
                    Location senderLocation = BLCustomer.Find(x => x.Id == DALParcel[index].Senderld).LocationOfCustomer;
                    Location targetLocation = BLCustomer.Find(x => x.Id == DALParcel[index].Targetld).LocationOfCustomer;//מיקום של השולח
                    double distanceBsenderAreciever = DistanceTo(senderLocation.Latitude, senderLocation.Longitude, targetLocation.Latitude, targetLocation.Longitude);
                    double distanceBrecieverAstation = returnMinDistancFromLicationToStation(targetLocation);//
                    double electricityUse = distanceBrecieverAstation * Free;
                    switch ((Enums.WeightCategories)DALParcel[index].Weight)
                    {
                        case Enums.WeightCategories.Light:
                            electricityUse += distanceBsenderAreciever * LightWeight;
                            break;
                        case Enums.WeightCategories.Medium:
                            electricityUse += distanceBsenderAreciever * MediumWeight;
                            break;
                        case Enums.WeightCategories.Heavy:
                            electricityUse += distanceBsenderAreciever * HeavyWeight;
                            break;


                    }
                    if (DALParcel[index].PichedUp == DateTime.MinValue)
                    {
                        item.LocationDrone = senderLocation;

                        electricityUse += DistanceTo(item.LocationDrone.Latitude,item.LocationDrone.Longitude, targetLocation.Latitude, targetLocation.Longitude) * Free;
                    }
                    else
                    {
                        item.LocationDrone = senderLocation;
                    }

                    item.StatusBatter = (float)((float)(rand.NextDouble() * (100 - electricityUse)) + electricityUse);
                    item.IdParcel = DALParcel[index].Id;

                }
                else
                {
                    item.StatusDrone = (BO.Enums.StatusDrone)rand.Next(0, 2);
                    if (item.StatusDrone == BO.Enums.StatusDrone.InMaintenance)
                    {
                        Station station = BLStation[rand.Next(0, BLStation.Count)];
                        item.LocationDrone = station.LocationStation;
                        accessIDal.SendDroneToCharge(station.Id, item.Id);
                        //IDAL.DO.DroneCharge  droneCharge= new IDAL.DO.DroneCharge();
                       // accessIDal.UpdetDroneCharge(station.Id);
                        item.StatusBatter = rand.Next(0, 21);
                    }
                    else
                    {
                        List<IDAL.DO.Parcel> DeliveredBySameId = DALParcel.FindAll(x => x.Droneld == item.Id && x.Delivered != DateTime.MinValue);
                        if (DeliveredBySameId.Any())
                        {
                            item.LocationDrone = BLCustomer.Find(x => x.Id == DeliveredBySameId[rand.Next(0, DeliveredBySameId.Count)].Targetld).LocationOfCustomer;
                            double electricityUse = returnMinDistancFromLicationToStation( item.LocationDrone)* Free;
                            item.StatusBatter = (float)((float)(rand.NextDouble() * (100 - electricityUse)) + electricityUse);

                        }
                        else
                        {
                            item.LocationDrone = BLStation[rand.Next(0, BLStation.Count)].LocationStation;
                            item.StatusBatter = rand.Next(0, 101);
                        }
                    }
                }
            }


        }

    }
}

