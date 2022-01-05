using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BL
{
    sealed partial class BL : IBL
    {


        public BO.PackageInTransfer GetParcelInTransfer(int idD)// 
        {
            BO.PackageInTransfer bop;
            try
            {
                DO.Parcel dop = accessIDal.GetParcelByDrone(idD);//parcel from dalObect

                // IDAL.DO.Drone d = accessIDal.GetDrone(dop.Droneld);//drone from dalObject
                bop = new BO.PackageInTransfer()
                {
                    Id = dop.Id,
                    PackageMode = StatuseParcelKnowBool(dop.Id),
                    PriorityParcel = (BO.Priority)dop.Priority,
                    Weight = (BO.WeightCategories)dop.Weight,
                    CustomerInParcelSender = new BO.CustomerInParcel() { Id = dop.Senderld, Name = GetCustomer(dop.Senderld).Name },
                    CustomerInParcelTarget = new BO.CustomerInParcel() { Id = dop.Targetld, Name = GetCustomer(dop.Targetld).Name },
                    Collection = GetCustomer(dop.Senderld).LocationOfCustomer,
                    DeliveryDestination = GetCustomer(dop.Targetld).LocationOfCustomer,
                    far = DistanceTo(GetCustomer(dop.Senderld).LocationOfCustomer.Latitude, GetCustomer(dop.Senderld).LocationOfCustomer.Longitude, GetCustomer(dop.Targetld).LocationOfCustomer.Latitude, GetCustomer(dop.Targetld).LocationOfCustomer.Longitude)//צריך לחשב את המרחק


                };
            }
            catch (BO.Excptions ex)
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
                DO.Parcel dop = accessIDal.GetParcel(id);
                BO.DroneToList bo = BlDrone.Find(p => p.Id == dop.Droneld);
               DO.Drone d = accessIDal.GetDrone(dop.Droneld);
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
                    Scheduled = dop.Scheduled,
                    DroneInParcel = new BO.DroneInParcel() { Id = d.Id, StatusBatter = GetDrone(d.Id).StatusBatter, LocationDroneInParcel = bo.LocationDrone }
                };
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bop;

        }
        public void AddParcel(BO.Parcel parcel)//v
        {
            DO.Parcel p = new DO.Parcel();
            p.Id = parcel.Id;
            p.Senderld = parcel.CustomerInParcelSender.Id;
            p.Targetld = parcel.CustomerInParcelTarget.Id;
            p.Weight = (DO.WeightCategories)parcel.Weight;
            p.Priority = (DO.Priority)parcel.Priority;
            p.Droneld = 0;
            p.Requested = DateTime.Now;
            p.Scheduled = default;
            p.PichedUp = default;
            p.Delivered = default;

            try
            {
                accessIDal.AddParcel(p);
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }

        private static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
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



        public void PrintUnconnectedParceslList()//ptint list parcel that not connection to drone
        {
            IEnumerable<DO.Parcel> a = accessIDal.GetAllParcel();
            for (int i = 0; i < a.Count(); i++)
            {
                if (GetParcel((a.ElementAt(i).Id)).Scheduled == default)
                    Console.WriteLine(ParcelToListToPrint(a.ElementAt(i).Id));

            }
        }

        public BO.ParcelToLIst PrintUnconnectedParceslList(int pr)//ptint list parcel that not connection to drone
        {
            IEnumerable<DO.Parcel> a = accessIDal.GetAllParcel();
            for (int i = 0; i < a.Count(); i++)
            {
                if (GetParcel((a.ElementAt(i).Id)).Scheduled == default)
                    return ParcelToListToPrint(a.ElementAt(i).Id);

            }
            throw new Exception();
        }
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
        private bool StatuseParcelKnowBool(int idP)// מחזיר לא נכון אם ממתין לאיסוף מחזיר נכון אם בדרך ליעד 
        {//return the statuse of parcel
            DO.Parcel dop = accessIDal.GetParcel(idP);

            //if (dop.Delivered.Date != default)//סופק
            // return StatusParcel.provided;

            if (dop.PichedUp != default)//נאסף
                return true;

            if (dop.Scheduled != default)//שוייך
                return false;
            return false;//נוצר
        }
        
        //איסוף חבילה על ידי רחפן צריך להיות במצב הובלה חישוב מרחק מהמקום עד שמגיעה לאסוף תחבילה הבטריה יורדת באתאם
        public void PickUpPackage(int id)//pick up package by drone
        {
            
            if (id < 0)
                throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");
            bool fal = BlDrone.Any(p => p.Id == id);
            BO.DroneToList drone = BlDrone.Find(p => p.Id == id);
            if (fal == true)//exsist
            {
                if (drone.StatusDrone != BO.StatusDrone.available)
                    throw new InvalidOperationException("The drone is not assigned to any package");
                var parcel = accessIDal.GetParcel(drone.IdParcel);
                if ((parcel.Scheduled == default(DateTime)) || (parcel.PichedUp != default(DateTime)))
                    throw new InvalidOperationException("The package is not ready to pick up");
                try
                {

                    var sender = accessIDal.GetCustomer(parcel.Senderld);
                    double distance = DistanceTo(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, sender.Lattitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
                    drone.StatusBatter = drone.StatusBatter - BatteryConsumption(distance);
                    drone.LocationDrone = new BO.Location//עדכון מיקום למיקום שולח
                    {
                        Latitude = sender.Lattitude,
                        Longitude = sender.Longitude
                    };
                    BlDrone.Remove(BlDrone.Find(p => p.Id == id));
                    BlDrone.Add(drone);
                    accessIDal.UpdetParcel(parcel); //קיבלנו עצם מועתק
                }
                catch (Exception)
                {
                    throw new Exception();//חריגה
                }

            }
            else
                throw new Exception();//חריגה

        }

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
                    ThenBy(parcel => DistanceTo(BlDronepp.LocationDrone.Latitude, BlDronepp.LocationDrone.Latitude, accessIDal.GetCustomer(parcel.Targetld).Lattitude, accessIDal.GetCustomer(parcel.Targetld).Longitude));
                for (int i = 0; i < peoperty.Count(); i++)
                {

                    double farToS = DistanceTo(locationDrone.Latitude, locationDrone.Longitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Longitude);//מרחק ממקום של הרחפן למקום של ההזמנה
                    double farFromSToT = DistanceTo(GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Longitude, GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Longitude);
                    BO.Location location = new BO.Location() { Latitude = GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Latitude, Longitude = GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Longitude };//מיקום של מי שצריך לקבל
                    double fatToStationMinDis = returnMinDistancFromLicationToStation(location);//מרחק מהתחנה הקרובה לרחפן לאחר מישלוח
                    DO.WeightCategories weight1 = peoperty.ElementAt(i).Weight;
                    double battery1 = BatteryConsumption(farFromSToT, weight1) + BatteryConsumption(farToS);//בטריה שמתבזבזת לרחפן ממקום של ולמקום המשלוח ועד שהוא הול
                    double batteryToStation1 = BatteryConsumption(fatToStationMinDis);
                    BO.WeightCategories weightPa = (BO.WeightCategories)peoperty.ElementAt(i).Weight;
                    if ((BlDronepp.StatusBatter - battery1 - batteryToStation1 > 0) && (weightPa <= weight))//המשב בסדר וגם הסוללה תספיק כדי להגיע לתחנה הקרובה אם יצטרך
                    {
                        return peoperty.ElementAt(i);
                    }

                }
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            throw new Exception();//חריגה

        }
        //שיוך חבילה לרחפן
        public void AssignPackageToDrone(int id)
        {
            bool fal = BlDrone.Any(p => p.Id == id);
            BO.DroneToList drone = BlDrone.Find(p => p.Id == id);
            DO.Drone dronidal = accessIDal.GetDrone(id);
            if (fal == false || id < 0)
                throw new ArgumentOutOfRangeException("id", "The drone number is error");
            if (accessIDal.GetAllParcel().Count() == 0)
                throw new Exception("No have parcel to Assign");

            DO.Parcel pp = MIUNParcelByGood(drone.Id);//קיבלתי את המשלוח לפי העדיפות טובה
                                                           //pp.Scheduled = DateTime.Now;//זמן שיוך עכשיו
            drone.StatusDrone = BO.StatusDrone.delivered;//שינוי מצב רחפן
            accessIDal.AssignPackageToDrone(pp.Id, id);//שליחת הרחפן והחבילה לשיכבת הנתונים
            BlDrone.Remove(BlDrone.Find(p => p.Id == id));
            BlDrone.Add(drone);

        }
        public void PackageDeliveryByDrone(int id)//אספקת חבילה ע"י רחפן
        {
            bool fal = BlDrone.Any(p => p.Id == id);
            BO.DroneToList drone = BlDrone.Find(p => p.Id == id);
            DO.Drone dronidal = accessIDal.GetDrone(id);
            DO.Parcel parcel = accessIDal.GetParcel(drone.IdParcel);
            if (fal == false || id < 0)
                throw new ArgumentOutOfRangeException( "The drone number is error");
            if (parcel.Delivered != default)//אספו

                throw new Exception();//להוסיף חריגה
            double distance = DistanceTo(accessIDal.GetCustomer(parcel.Senderld).Lattitude, accessIDal.GetCustomer(parcel.Senderld).Longitude, accessIDal.GetCustomer(parcel.Targetld).Longitude, accessIDal.GetCustomer(parcel.Targetld).Longitude);
            drone.StatusBatter -= BatteryConsumption(distance, parcel.Weight);
            drone.LocationDrone.Latitude = accessIDal.GetCustomer(parcel.Targetld).Lattitude;//שינוי מיקום
            drone.LocationDrone.Longitude = accessIDal.GetCustomer(parcel.Targetld).Longitude;
            parcel.PichedUp = DateTime.Now;//שינוי זמן
            accessIDal.UpdetParcel(parcel);//קיבלנו עצם מועתק
            drone.StatusDrone = BO.StatusDrone.available;//שינוי סטטוס
            BlDrone.Remove(BlDrone.Find(p => p.Id == id));
            BlDrone.Add(drone);

        }
    }
}

    
