using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BL
{
    sealed partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int idD)
        {
            lock (accessIDal)
            {
                accessIDal.DeleteParcel(idD);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(BO.Parcel parcel)
        {
            lock (accessIDal)
            {
                DO.Parcel p = new DO.Parcel();

                p.Senderld = parcel.CustomerInParcelSender.Id;
                p.Targetld = parcel.CustomerInParcelTarget.Id;
                p.Weight = (DO.WeightCategories)parcel.Weight;
                p.Priority = (DO.Priority)parcel.Priority;
                p.Droneld = parcel.DroneInParcel.Id;
                p.Requested = parcel.Requested;
                p.Scheduled = parcel.Scheduled;
                p.PichedUp = parcel.PichedUp;
                p.Delivered = parcel.Delivered;
                accessIDal.UpdetParcel(p);
            }
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public BO.PackageInTransfer GetParcelInTransfer(int idD)// 
        //{
        //    BO.PackageInTransfer bop;
        //    try
        //    {
        //        DO.Parcel dop = accessIDal.GetParcelByDrone(idD);//parcel from dalObect

        //        // IDAL.DO.Drone d = accessIDal.GetDrone(dop.Droneld);//drone from dalObject
        //        bop = new BO.PackageInTransfer()
        //        {
        //            Id = dop.Id,
        //            PackageMode = StatuseParcelKnowBool(dop.Id),
        //            PriorityParcel = (BO.Priority)dop.Priority,
        //            Weight = (BO.WeightCategories)dop.Weight,
        //            CustomerInParcelSender = new BO.CustomerInParcel() { Id = dop.Senderld, Name = GetCustomer(dop.Senderld).Name },
        //            CustomerInParcelTarget = new BO.CustomerInParcel() { Id = dop.Targetld, Name = GetCustomer(dop.Targetld).Name },
        //            Collection = GetCustomer(dop.Senderld).LocationOfCustomer,
        //            DeliveryDestination = GetCustomer(dop.Targetld).LocationOfCustomer,
        //            far = calculateDist(DroneToLisToPrint(idD).LocationDrone.Latitude, DroneToLisToPrint(idD).LocationDrone.Longitude, GetCustomer(dop.Targetld).LocationOfCustomer.Latitude, GetCustomer(dop.Targetld).LocationOfCustomer.Longitude)//צריך לחשב את המרחק


        //        };
        //    }
        //    catch (BO.Excptions ex)
        //    {
        //        throw new BO.Excptions(ex.Message);
        //    }
        //    return bop;

        //}
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel GetParcel(int id)//v
        {
            BO.Parcel bop;
            try
            {
                DO.Parcel dop = accessIDal.GetParcel(id);
                BO.DroneToList bo = BlDrone.Find(p => p.Id == dop.Droneld);
              
                bop = new BO.Parcel()
                {
                    Id = dop.Id,
                    CustomerInParcelSender = new BO.CustomerInParcel() { Id = accessIDal.GetParcel(id).Senderld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(id).Senderld).Name },
                    CustomerInParcelTarget = new BO.CustomerInParcel() { Id = accessIDal.GetParcel(id).Targetld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(id).Targetld).Name },
                    Weight = (BO.WeightCategories)dop.Weight,
                    Priority = (BO.Priority)dop.Priority,
                    Requested = dop.Requested,
                    Delivered = dop.Delivered,
                    PichedUp = dop.PichedUp,
                    Scheduled = dop.Scheduled
                };
                if (dop.Droneld != 0)
                {
                    DO.Drone d = accessIDal.GetDrone(dop.Droneld);
                    bop.DroneInParcel = new BO.DroneInParcel() { Id = d.Id, StatusBatter = GetDrone(d.Id).StatusBatter, LocationDroneInParcel = bo.LocationDrone };
                }
                else
                    bop.DroneInParcel = null;


            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bop;

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(BO.Parcel parcel)//v
        {
            lock (accessIDal)
            {
                DO.Parcel p = new DO.Parcel();

                p.Senderld = parcel.CustomerInParcelSender.Id;
                p.Targetld = parcel.CustomerInParcelTarget.Id;
                p.Weight = (DO.WeightCategories)parcel.Weight;
                p.Priority = (DO.Priority)parcel.Priority;
                p.Droneld = default;
                p.Requested = DateTime.Now;
                p.Scheduled = default;
                p.PichedUp = default;
                p.Delivered = default;

                try
                {
                    int i = accessIDal.AddParcel(p);
                    return i;
                }
                catch (BO.Excptions ex)
                {
                    throw new BO.Excptions(ex.Message);
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public  double DistanceTo(double lac1, double loc1, double lac2, double loc2, char unit = 'K')
        {
            const double Radios = 6371000;//meters
            //deg to radians
            double lat1 = lac1 * Math.PI / 180;
            double lat2 = lac2 * Math.PI / 180;
            double lng1 = loc1 * Math.PI / 180;
            double lng2 = loc2 * Math.PI / 180;

            //Haversine formula
            double a = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lng2 - lng1) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Radios * c;


            //double rlat1 = Math.PI * lat1 / 180;
            //double rlat2 = Math.PI * lat2 / 180;
            //double theta = lon1 - lon2;
            //double rtheta = Math.PI * theta / 180;
            //double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *  Math.Cos(rlat2) * Math.Cos(rtheta);
            //dist = Math.Acos(dist);
            //dist = dist * 180 / Math.PI;
            //dist = dist * 60 * 1.1515;

            //switch (unit)
            //{
            //    case 'K': //Kilometers -> default
            //        return dist * 1.609344;
            //    case 'N': //Nautical Miles 
            //        return dist * 0.8684;
            //    case 'M': //Miles
            //        return dist;
            //}

            //return dist;
        }
        private double calculateDist(double lac1, double loc1, double lac2, double loc2)
        {
            const double Radios = 6371000;//meters
            //deg to radians
            double lat1 = lac1 * Math.PI / 180;
            double lat2 = lac2 * Math.PI / 180;
            double lng1 = loc1 * Math.PI / 180;
            double lng2 = loc2 * Math.PI / 180;

            //Haversine formula
            double a = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lng2 - lng1) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Radios * c;
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.ParcelToLIst ParcelToListToPrint(int idp)//v
        {
            BO.ParcelToLIst bop;
            try
            {
               DO.Parcel dop = accessIDal.GetParcel(idp);
                bop = new BO.ParcelToLIst()
                {
                    Id = dop.Id,
                    SenderName = GetCustomer(dop.Senderld).Name,
                    TargetName = GetCustomer(dop.Targetld).Name,
                    Priority = (BO.Priority)dop.Priority,
                    Weight = (BO.WeightCategories)dop.Weight,
                    statusParcel = StatuseParcelKnow(dop.Id)

                };
            }

            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bop;

        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PrintUnconnectedParceslList()//ptint list parcel that not connection to drone
        {
            IEnumerable<DO.Parcel> a = accessIDal.GetAllParcel();
            for (int i = 0; i < a.Count(); i++)
            {
                if (GetParcel(a.ElementAt(i).Id).Scheduled == default)
                    Console.WriteLine(ParcelToListToPrint(a.ElementAt(i).Id));

            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.ParcelToLIst PrintUnconnectedParceslList(int pr)//ptint list parcel that not connection to drone
        {
            IEnumerable<DO.Parcel> a = accessIDal.GetAllParcel();
            for (int i = 0; i < a.Count(); i++)
            {
                if (GetParcel(a.ElementAt(i).Id).Scheduled == default)
                    return ParcelToListToPrint(a.ElementAt(i).Id);

            }
            throw new Exception();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.StatusParcel StatuseParcelKnow(int idP)
        {//return the statuse of parcel
            DO.Parcel dop = accessIDal.GetParcel(idP);

            if (dop.Delivered.Date != default)//סופק
                return BO.StatusParcel.provided;

            if (dop.PichedUp != default)//נאסף
                return BO.StatusParcel.collected;

            if (dop.Scheduled != default)//שוייך
                return BO.StatusParcel.associated;
            return BO.StatusParcel.Created;//נוצר
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private bool StatuseParcelKnowBool(int idP)// מחזיר לא נכון אם ממתין לאיסוף מחזיר נכון אם בדרך ליעד 
        {//return the statuse of parcel
            DO.Parcel dop = accessIDal.GetParcel(idP);

            //if (dop.Delivered.Date != default)//סופק
            // return StatusParcel.provided;

            if (dop.Delivered != default)//סופק
                return true;

            if (dop.PichedUp != default)//נאסף
                return true;
            
            return false;//לא נאסף
        }

        //איסוף חבילה על ידי רחפן צריך להיות במצב הובלה חישוב מרחק מהמקום עד שמגיעה לאסוף תחבילה הבטריה יורדת באתאם


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickUpPackage(int id)//pick up package by drone איסוף חבילה לאחר שהיא שוייכה לחבילה
        {
            
            if (id < 0)
                throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");
            bool fal = BlDrone.Any(p => p.Id == id);
            BO.DroneToList drone = BlDrone.Find(p => p.Id == id);
            if (fal == true)//exsist
            {
                if (drone.StatusDrone != BO.StatusDrone.delivered)
                    throw new InvalidOperationException("The drone is not assigned to any package");
                var parcel = accessIDal.GetParcel(drone.IdParcel);
                if ((parcel.Scheduled == default(DateTime)) || (parcel.Requested == default(DateTime))|| (parcel.PichedUp != default(DateTime)))//החבילה לא נוצרה ולא שוייכה
                    throw new InvalidOperationException("The package is not ready to pick up");
                try
                {

                    DO.Customer sender = accessIDal.GetCustomer(parcel.Senderld);
                    double distance = calculateDist(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, sender.Lattitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
                    drone.StatusBatter -= BatteryConsumption(distance);
                    drone.LocationDrone = new BO.Location//עדכון מיקום למיקום שולח
                    {
                        Latitude = sender.Lattitude,
                        Longitude = sender.Longitude
                    };
                    accessIDal.PackageCollectionByDrone(parcel.Id);
                    BlDrone.Remove(BlDrone.Find(p => p.Id == id));
                    BlDrone.Add(drone);//ברשימת הרחפנים משנים לרחפן רק את המיקום ואת הסוללה
                   // accessIDal.UpdetParcel(parcel); //קיבלנו עצם מועתק
                   
                }
                catch (Exception)
                {
                    throw new Exception();//חריגה
                }

            }
            else
                throw new Exception();//חריגה

        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        private DO.Parcel MIUNParcelByGood(int idd)//מחזיר את החבילה הכי טובה לביצוע
        {

            try
            {
                BO.WeightCategories weight = GetDrone(idd).Weight;
                BO.DroneToList BlDronepp = DroneToLisToPrint(idd);//drone from BL
                BO.Location locationDrone = GetDrone(idd).LocationDrone;//Location drone
                IEnumerable<DO.Parcel> aa = accessIDal.GetAllParcel();
                //IDAL.DO.Parcel newGood;
                var peoperty = aa.OrderBy(parcel => parcel.Priority).ThenBy(parcel => parcel.Weight).
                    ThenBy(parcel => calculateDist(BlDronepp.LocationDrone.Latitude, BlDronepp.LocationDrone.Latitude, accessIDal.GetCustomer(parcel.Senderld).Lattitude, accessIDal.GetCustomer(parcel.Senderld).Longitude));
                for (int i = 0; i < peoperty.Count(); i++)
                {
                    //המרחק מהמיקום של הרחפן הנוכחי ועש הלאסוף תחבילה
                    double farToS = calculateDist(locationDrone.Latitude, locationDrone.Longitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Longitude);//מרחק ממקום של הרחפן למקום של ההזמנה
                   //vnrje nvaukj kneck
                    double farFromSToT = calculateDist(GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Longitude, GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Longitude);//מרחק ממיקום השולח למיקום של המקבל
                    BO.Location location = new BO.Location() { Latitude = GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Latitude, Longitude = GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Longitude };//מיקום של מי שצריך לקבל
                    //מרחק מהמקבל ועד לתחנה הקרובה אליו
                    double fatToStationMinDis = returnMinDistancFromLicationToStation(location);//מרחק מהתחנה הקרובה לרחפן לאחר מישלוח
                    DO.WeightCategories weight1 = peoperty.ElementAt(i).Weight;//משקל ההזמנה
                   
                    double battery1 = BatteryConsumption(farFromSToT, weight1) + BatteryConsumption(farToS);//בטריה שמתבזבזת לרחפן ממקומו הנוכחי ליפני ביצוע הזמנה למקום של איסוף הזמנה + הבטריה ממקום המקבל למקום השולח
                    double batteryToStation1 = BatteryConsumption(fatToStationMinDis);//בטריה שמתבזבזת לתחנה הקרובה לאחר הספקה
                    BO.WeightCategories weightPa = (BO.WeightCategories)peoperty.ElementAt(i).Weight;
                    if ((BlDronepp.StatusBatter - battery1 - batteryToStation1 > 0) && (weightPa <= weight)&(peoperty.ElementAt(i).Droneld==0))//המשב בסדר וגם הסוללה תספיק כדי להגיע לתחנה הקרובה אם יצטרך
                    {
                        return peoperty.ElementAt(i);
                    }
                        
                }
            }
            catch (BO.Excptions )
            {
                throw new BO.Excptions("We're sorry but there's no way\n for a handful to place an order");
            }
            throw new BO.Excptions("We're sorry but there's no way\n for a handful to place an order");

        }
        //שיוך חבילה לרחפן

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignPackageToDrone(int id)
        {
            lock (accessIDal)
            {
                bool fal = BlDrone.Any(p => p.Id == id);
                BO.DroneToList drone = BlDrone.Find(p => p.Id == id);
                DO.Drone dronidal = accessIDal.GetDrone(id);
                if (fal == false || id < 0)
                    throw new ArgumentOutOfRangeException("id", "The drone number is error");
                if (accessIDal.GetAllParcel().Count() == 0)
                    throw new Exception("No have parcel to Assign");

                DO.Parcel pp = MIUNParcelByGood(drone.Id);//קיבלתי את המשלוח לפי העדיפות טובה
                if ( pp.Delivered == default)
                {
                    drone.StatusDrone = BO.StatusDrone.delivered;//שינוי מצב רחפן
                    drone.IdParcel = pp.Id;
                    accessIDal.AssignPackageToDrone(pp.Id, id);//שליחת הרחפן והחבילה לשיכבת הנתונים
                    BlDrone.Remove(BlDrone.Find(p => p.Id == id));
                    BlDrone.Add(drone);
                }
            }

        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PackageDeliveryByDrone(int id)//אספקת חבילה ע"י רחפן
        {
            lock (accessIDal)
            {
                bool fal = BlDrone.Any(p => p.Id == id);
                BO.DroneToList drone = BlDrone.Find(p => p.Id == id);
                DO.Drone dronidal = accessIDal.GetDrone(id);
                DO.Parcel parcel = accessIDal.GetParcel(drone.IdParcel);
                if (fal == false || id < 0)
                    throw new ArgumentOutOfRangeException("The drone number is error");
                if (parcel.Delivered != default)//אספו

                    throw new Exception();//להוסיף חריגה
                double distance = calculateDist(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, accessIDal.GetCustomer(parcel.Targetld).Lattitude, accessIDal.GetCustomer(parcel.Targetld).Longitude);
                drone.StatusBatter -= BatteryConsumption(distance, parcel.Weight);
                drone.LocationDrone.Latitude = accessIDal.GetCustomer(parcel.Targetld).Lattitude;//שינוי מיקום
                drone.LocationDrone.Longitude = accessIDal.GetCustomer(parcel.Targetld).Longitude;
                accessIDal.DeliveryOfPackageToTheCustomer(parcel.Id);//טת המישלוח לשנו תאת זמן ההספקה
                drone.StatusDrone = BO.StatusDrone.available;//שינוי סטטוס
                BlDrone.Remove(BlDrone.Find(p => p.Id == id));
                BlDrone.Add(drone);//לשנות לרחפן את הבטריה ןאת המיקום
            }
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ParcelToLIst> GetParcels()
        {
            List<BO.ParcelToLIst> b = new List<BO.ParcelToLIst>();
            IEnumerable<DO.Parcel> a = accessIDal.GetAllParcel();
            for (int i = 0; i < a.Count(); i++)
            {
                b.Add(ParcelToListToPrint(a.ElementAt(i).Id));
            }
            return b;
        }
        public IEnumerable<BO.ParcelToLIst> GetParcelByPerdicate(Predicate<BO.ParcelToLIst> predicate)
        {
            return from parcel in this.GetParcels()
                   where predicate(parcel)
                   select parcel;
        }



    }






}

    
