
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlApi;
using BO;
using BL;
using DalApi;


namespace BL
{
    sealed partial class BL : IBL
    {
        //
        public Random rand = new Random(DateTime.Now.Millisecond);
        internal IDal accessIDal = DalFactory.GetDal();//access to dalobject
        private List<DroneToList> BlDrone;
        public static double Free;
        public static double LightWeight;
        public static double MediumWeight;
        public static double HeavyWeight;
        public static double LoadingPrecents;
        
        static readonly IBL instance = new BL();
        public static IBL Instance { get => instance; }

        BL()

        {
           
            double[] arr = accessIDal.RequestPowerConsuption();// בקשת צריכת חשמל ע"י רחפן
            Free = arr[0];
            LightWeight = arr[1];
            MediumWeight = arr[2];
            HeavyWeight = arr[3];
            LoadingPrecents = arr[4];
             BlDrone = new List<DroneToList>();//רשימה של רחפנים בביאל
            //BLDrones = new List<Drone>();
            IEnumerable<DO.Drone> DALDrones = accessIDal.GetAllDrone().ToList();//רשימה של רחפנים מDAL
            foreach (var item in DALDrones)
            {
                BlDrone.Add(new DroneToList { Id = item.Id, Model = item.Model, Weight = (WeightCategories)item.Weight });//weightcategories
            }
            List<Customer> BLCustomer = new List<Customer>();
            IEnumerable<DO.Customer> DALCustomer = accessIDal.GetAllCustomer();//רשימה של לקוחות מDAL
            foreach (var item in DALCustomer)
            {
                BLCustomer.Add(new Customer { Id = item.Id, Name = item.Name, Pone = item.Pone, LocationOfCustomer = new Location() { Longitude = item.Longitude, Latitude = item.Lattitude } });//lattitud with one t
            }
            List<Station> BLStation = new List<Station>();
            IEnumerable<DO.Station> DALStation = accessIDal.GetAllStation();
           
            foreach (var item in DALStation)
            { 
                BLStation.Add(new Station { Name = item.Name, Id = item.Id, ChargeSlotsFree = item.ChargeSlots, LocationStation = new Location() { Longitude = item.Longitude, Latitude = item.Latitude } });//lattitud with one t
            }
            List<DO.Parcel> DALParcel1 = new List<DO.Parcel>();//להעביר לתוכו
            IEnumerable<DO.Parcel> DALParcel0 = accessIDal.GetAllParcel();//רשימה של חבילות מDAL
            foreach (var item in DALParcel0)
            {
                DALParcel1.Add(new DO.Parcel { Id = item.Id, Senderld = item.Senderld, Delivered = item.Delivered, Droneld = item.Droneld, PichedUp = item.PichedUp, Priority = item.Priority,Requested=item.Requested,Scheduled=item.Scheduled,Targetld=item.Targetld,Weight=item.Weight }) ;//lattitud with one t
            }
            IEnumerable<DO.Parcel> DALParcel = accessIDal.GetAllParcel();//רשימה של חביחות מ DAL

            //foreach (var item in BlDrone)
            //{
                
            //    int index = DALParcel1.FindIndex(x => x.Droneld == item.Id && x.Delivered == DateTime.MinValue);
            //    if (index != -1)
            //    {
            //        item.StatusDrone = StatusDrone.delivered;
            //        Location senderLocation = BLCustomer.Find(x => x.Id == DALParcel1[index].Senderld).LocationOfCustomer;
            //        Location targetLocation = BLCustomer.Find(x => x.Id == DALParcel1[index].Targetld).LocationOfCustomer;//מיקום של השולח
            //        double distanceBsenderAreciever = DistanceTo(senderLocation.Latitude, senderLocation.Longitude, targetLocation.Latitude, targetLocation.Longitude);
            //        double distanceBrecieverAstation = returnMinDistancFromLicationToStation(targetLocation);//
            //        double electricityUse = distanceBrecieverAstation * Free;
            //        switch (DALParcel1[index].Weight)
            //        {
            //            case (DO.WeightCategories)WeightCategories.Light:
            //                electricityUse += distanceBsenderAreciever * LightWeight;
            //                break;
            //            case (DO.WeightCategories)WeightCategories.Medium:
            //                electricityUse += distanceBsenderAreciever * MediumWeight;
            //                break;
            //            case (DO.WeightCategories)WeightCategories.Heavy:
            //                electricityUse += distanceBsenderAreciever * HeavyWeight;
            //                break;


            //        }
            //        if (DALParcel1[index].PichedUp == DateTime.MinValue)
            //        {
            //            item.LocationDrone = senderLocation;

            //            electricityUse += DistanceTo(item.LocationDrone.Latitude,item.LocationDrone.Longitude, targetLocation.Latitude, targetLocation.Longitude) * Free;
            //        }
            //        else
            //        {
            //            item.LocationDrone = senderLocation;
            //        }

            //        item.StatusBatter = (float)((float)(rand.NextDouble() * (100 - electricityUse)) + electricityUse);
            //        item.IdParcel = DALParcel1[index].Id;

            //    }
            //    else
            //    {
            //        item.StatusDrone = (StatusDrone)rand.Next(0, 2);
            //        if (item.StatusDrone == StatusDrone.InMaintenance)
            //        {
            //            Station station = BLStation[rand.Next(0, BLStation.Count)];
            //            item.LocationDrone = station.LocationStation;
            //            accessIDal.SendDroneToCharge(station.Id, item.Id);
            //            //IDAL.DO.DroneCharge  droneCharge= new IDAL.DO.DroneCharge();
            //           // accessIDal.UpdetDroneCharge(station.Id);
            //            item.StatusBatter = rand.Next(0, 21);
            //        }
            //        else
            //        {
            //            List<DO.Parcel> DeliveredBySameId = DALParcel1.FindAll(x => x.Droneld == item.Id && x.Delivered != DateTime.MinValue);
            //            if (DeliveredBySameId.Any())
            //            {
            //                item.LocationDrone = BLCustomer.Find(x => x.Id == DeliveredBySameId[rand.Next(0, DeliveredBySameId.Count)].Targetld).LocationOfCustomer;
            //                double electricityUse = returnMinDistancFromLicationToStation( item.LocationDrone)* Free;
            //                item.StatusBatter = (float)((float)(rand.NextDouble() * (100 - electricityUse)) + electricityUse);

            //            }
            //            else
            //            {
            //                item.LocationDrone = BLStation[rand.Next(0, BLStation.Count)].LocationStation;
            //                item.StatusBatter = rand.Next(0, 101);
            //            }
            //        }
            //    }
            //}


        }

    }
}

