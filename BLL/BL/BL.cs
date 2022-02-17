
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
        public  double Free;
        public  double LightWeight;
        public  double MediumWeight;
        public  double HeavyWeight;
        public  double LoadingPrecents;

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
            IEnumerable<DO.DroneCharge> DALDronesDroneCharge = accessIDal.GetAllDroneCharge().ToList();//רשימה של רחפנים מDAL

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
                DALParcel1.Add(new DO.Parcel { Id = item.Id, Senderld = item.Senderld, Delivered = item.Delivered, Droneld = item.Droneld, PichedUp = item.PichedUp, Priority = item.Priority, Requested = item.Requested, Scheduled = item.Scheduled, Targetld = item.Targetld, Weight = item.Weight });//lattitud with one t
            }
            // IEnumerable<DO.Parcel> DALParcel = accessIDal.GetAllParcel();//רשימה של חביחות מ DAL
            List<DO.DroneCharge> DALDroneCharge1 = new List<DO.DroneCharge>();//להעביר לתוכו
            IEnumerable<DO.DroneCharge> DALDroneCharge = accessIDal.GetAllDroneCharge();//רשימה של חבילות מDAL
            foreach (var item in DALDroneCharge)
            {
                DALDroneCharge1.Add(new DO.DroneCharge { Stationld = item.Stationld, Droneld = item.Droneld });//lattitud with one t
            }




            foreach (var item in BlDrone)
            {

                int index = DALParcel1.FindIndex(x => x.Droneld == item.Id && x.Delivered == default);//לחבפש רחפן בתוך רשימת ההחבילות שההזמנה עדיין לא סופקה
                if (index != -1)//אם מבצע משלוח ולא סופק עדיין
                {
                    item.StatusDrone = StatusDrone.delivered;
                    Location senderLocation = BLCustomer.Find(x => x.Id == DALParcel1[index].Senderld).LocationOfCustomer;//מיקום של שולח
                    Location targetLocation = BLCustomer.Find(x => x.Id == DALParcel1[index].Targetld).LocationOfCustomer;//מיקום של במקבל
                    double distanceBsenderAreciever = DistanceTo(senderLocation.Latitude, senderLocation.Longitude, targetLocation.Latitude, targetLocation.Longitude);
                    double distanceBrecieverAstation = returnMinDistancFromLicationToStation(targetLocation);//
                    double electricityUse = distanceBrecieverAstation * Free;
                    switch (DALParcel1[index].Weight)
                    {
                        case (DO.WeightCategories)WeightCategories.Light:
                            electricityUse += distanceBsenderAreciever * LightWeight;
                            break;
                        case (DO.WeightCategories)WeightCategories.Medium:
                            electricityUse += distanceBsenderAreciever * MediumWeight;
                            break;
                        case (DO.WeightCategories)WeightCategories.Heavy:
                            electricityUse += distanceBsenderAreciever * HeavyWeight;
                            break;


                    }
                    if (DALParcel1[index].PichedUp == DateTime.MinValue)//אם לא נאסף ולא סופק המיקום יהיה בתחנה קרוב לשולח
                    {
                        item.LocationDrone = new Location { Latitude = GetStationCalculatesTheSmallestDistance(senderLocation).Latitude, Longitude = GetStationCalculatesTheSmallestDistance(senderLocation).Longitude };// שמים את הרחן במקום של התחנה הקרובה לשולח לא נאסף ןלא סופק 
                        electricityUse += DistanceTo(item.LocationDrone.Latitude, item.LocationDrone.Longitude, senderLocation.Latitude, senderLocation.Longitude) * Free;
                    }
                    else
                    {
                        item.LocationDrone = senderLocation;//נאסף החבילה יהיה במיקום השולח
                    }
                    int a= rand.Next((int)electricityUse, 100);
                    item.StatusBatter = a;
                    item.IdParcel = DALParcel1[index].Id;

                }
                else
                {

                    int index1 = DALDroneCharge1.FindIndex(x => x.Droneld == item.Id);//לחבפש רחפן בתוך רשימת הרחפנים בטעינה
                    if (index1 != -1)//אם הרחפן בטעינה
                    {
                        item.StatusDrone = StatusDrone.InMaintenance;
                        item.StatusBatter =(int) rand.Next(0, 21);
                        item.LocationDrone = new Location { Latitude = accessIDal.GetStation(DALDroneCharge1.Find(x => x.Droneld == item.Id).Stationld).Latitude, Longitude = accessIDal.GetStation(DALDroneCharge1.Find(x => x.Droneld == item.Id).Stationld).Longitude };

                    }
                    else
                    {

                        item.StatusDrone = StatusDrone.available;
                        List<DO.Parcel> DeliveredBySameId = DALParcel1.FindAll(x => x.Droneld == item.Id && x.Delivered != DateTime.MinValue);
                        int a = rand.Next(0, DeliveredBySameId.Count);
                        BO.Location location = BLCustomer.Find(x => x.Id == DeliveredBySameId[a].Targetld).LocationOfCustomer;
                        
                        if (DeliveredBySameId.Any()&& location != null)
                        {
                           
                            item.LocationDrone = location;
                            double electricityUse = returnMinDistancFromLicationToStation(item.LocationDrone) * Free;
                            item.StatusBatter = (int)((int)(rand.NextDouble() * (100 - electricityUse)) + electricityUse);

                        }
                        else
                        {
                            item.LocationDrone = BLStation[rand.Next(0, BLStation.Count)].LocationStation;
                            item.StatusBatter = (int)rand.Next(0, 101);
                        }

                    }
                }
            }
        }
            public void StartDroneSimulator(int id, Action update, Func<bool> checkStop)
            {
                new Simulator(this, id, update, checkStop);
            }


        }
    }


