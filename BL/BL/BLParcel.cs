using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    //DataTime.now.addDay(rand.now(-150,50)
    public partial class BL
    {

        public BO.PackageInTransfer GetParcelInTransfer(int idD)// 
        {
            BO.PackageInTransfer bop;
            try
            {
                IDAL.DO.Parcel dop = accessIDal.GetParcelByDrone(idD);//parcel from dalObect

                // IDAL.DO.Drone d = accessIDal.GetDrone(dop.Droneld);//drone from dalObject
                bop = new BO.PackageInTransfer()
                {
                    Id = dop.Id,
                    PackageMode = StatuseParcelKnowBool(dop.Id),
                    PriorityParcel = (Priority)dop.Priority,
                    Weight = (WeightCategories)dop.Weight,
                    CustomerInParcelSender = new CustomerInParcel() { Id = dop.Senderld, Name = GetCustomer(dop.Senderld).Name },
                    CustomerInParcelTarget = new CustomerInParcel() { Id = dop.Targetld, Name = GetCustomer(dop.Targetld).Name },
                    Collection = GetCustomer(dop.Senderld).LocationOfCustomer,
                    DeliveryDestination = GetCustomer(dop.Targetld).LocationOfCustomer,
                    far = DistanceTo(GetCustomer(dop.Senderld).LocationOfCustomer.Latitude, GetCustomer(dop.Senderld).LocationOfCustomer.Longitude, GetCustomer(dop.Targetld).LocationOfCustomer.Latitude, GetCustomer(dop.Targetld).LocationOfCustomer.Longitude)//צריך לחשב את המרחק


                };
            }
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
            p.Scheduled = default;
            p.PichedUp = default;
            p.Delivered = default;

            try
            {
                accessIDal.AddParcel(p);
            }
            catch (IDAL.DO.Excptions ex)
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
                IDAL.DO.Parcel dop = accessIDal.GetParcel(idp);
                bop = new BO.ParcelToLIst()
                {
                    Id = dop.Id,
                    SenderName = GetCustomer(dop.Senderld).Name,
                    TargetName = GetCustomer(dop.Targetld).Name,
                    Priority = (Priority)dop.Priority,
                    Weight = (WeightCategories)dop.Weight,
                    statusParcel = StatuseParcelKnow(dop.Id)

                };
            }

            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bop;

        }

        public void PrintParcelList()//print all parcel
        {
            IEnumerable<IDAL.DO.Parcel> a = accessIDal.GetAllParcel();
            for (int i = 0; i < a.Count(); i++)
            {
                Console.WriteLine(ParcelToListToPrint(a.ElementAt(i).Id));
            }
        }
        public void PrintParcelById(int ids)//print parcel get id of parcel
        {
            Console.WriteLine(GetParcel(ids).ToString());
        }

        public void PrintUnconnectedParceslList()//ptint list parcel that not connection to drone
        {
            IEnumerable<IDAL.DO.Parcel> a = accessIDal.GetAllParcel();
            for (int i = 0; i < a.Count(); i++)
            {
                if (GetParcel((a.ElementAt(i).Id)).Scheduled == default)
                    Console.WriteLine(ParcelToListToPrint(a.ElementAt(i).Id));

            }
        }
        private StatusParcel StatuseParcelKnow(int idP) {//return the statuse of parcel
            IDAL.DO.Parcel dop = accessIDal.GetParcel(idP);

            if (dop.Delivered.Date != default)//סופק
                return StatusParcel.provided;

            if (dop.PichedUp != default)//נאסף
                return StatusParcel.collected;

            if (dop.Scheduled != default)//שוייך
                return StatusParcel.associated;
            return StatusParcel.Created;//נוצר
        }
        private bool StatuseParcelKnowBool(int idP)// מחזיר לא נכון אם ממתין לאיסוף מחזיר נכון אם בדרך ליעד 
        {//return the statuse of parcel
            IDAL.DO.Parcel dop = accessIDal.GetParcel(idP);

            //if (dop.Delivered.Date != default)//סופק
            // return StatusParcel.provided;

            if (dop.PichedUp != default)//נאסף
                return true;

            if (dop.Scheduled != default)//שוייך
                return false;
            return false;//נוצר
        }
        public void PickUpPackage(int id)//pick up package by drone
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");
            for (int i = 0; i < BlDrone.Count; i++)
            {
                if (BlDrone[i].Id == id)
                {
                    DroneToList drone = BlDrone[i];
                    //if (drone.Id != id)
                    //    throw new BadDroneIdException(drone.Id);
                    if (drone.StatusDrone != BO.Enums.StatusDrone.available)
                        throw new InvalidOperationException("The drone is not assigned to any package");
                    var parcel = accessIDal.GetParcel(drone.IdParcel);
                    if ((parcel.Scheduled == default(DateTime)) || (parcel.PichedUp != default(DateTime)))
                        throw new InvalidOperationException("The package is not ready to pick up");

                    try
                    {

                        var sender = accessIDal.GetCustomer(parcel.Senderld);
                        double distance = DistanceTo(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, sender.Lattitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
                        drone.StatusBatter = drone.StatusBatter - BatteryConsumption(distance);
                        drone.LocationDrone = new Location//עדכון מיקום למיקום שולח
                        {
                            Latitude = sender.Lattitude,
                            Longitude = sender.Longitude
                        };
                        BlDrone[i] = drone;
                        parcel.PichedUp = DateTime.Now;
                        accessIDal.DeleteParcel(parcel.Id);//קיבלנו עצם מועתק
                        accessIDal.AddParcel(parcel);
                    }
                    catch (Exception)
                    {
                        throw new Exception();//חריגה
                    }
                }
            }
        }
        
        private IDAL.DO.Parcel MIUNParcelByGood(int idd)
        {
            try
            {

                DroneToList BlDronepp = DroneToLisToPrint(idd);//drone from BL
                Location locationDrone = GetDrone(idd).LocationDrone;//Location drone
                IEnumerable<IDAL.DO.Parcel> aa = accessIDal.GetAllParcel();
                //IDAL.DO.Parcel newGood;
                var peoperty = aa.OrderBy(parcel => parcel.Priority).ThenBy(parcel => parcel.Weight).
                    ThenBy(parcel => DistanceTo(BlDronepp.LocationDrone.Latitude, BlDronepp.LocationDrone.Latitude, accessIDal.GetCustomer(parcel.Targetld).Lattitude, accessIDal.GetCustomer(parcel.Targetld).Longitude));
                for (int i = 0; i < peoperty.Count(); i++)
                {

                    double farToS = DistanceTo(locationDrone.Latitude, locationDrone.Longitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Longitude);//מרחק ממקום של הרחפן למקום של ההזמנה
                    double farFromSToT = DistanceTo(GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Senderld).LocationOfCustomer.Longitude, GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Latitude, GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Longitude);
                    Location location = new Location() { Latitude = GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Latitude, Longitude = GetCustomer(peoperty.ElementAt(i).Targetld).LocationOfCustomer.Longitude };//מיקום של מי שצריך לקבל
                    double fatToStationMinDis = returnMinDistancFromLicationToStation(location);//מרחק מהתחנה הקרובה לרחפן לאחר מישלוח
                    double battery1 = BatteryConsumption(farFromSToT, (WeightCategories)peoperty.ElementAt(i).Weight) + BatteryConsumption(farToS);//בטריה שמתבזבזת לרחפן ממקום של ולמקום המשלוח ועד שהוא הול
                    double batteryToStation1 = BatteryConsumption(fatToStationMinDis);
                    if (BlDronepp.StatusBatter - battery1 - batteryToStation1 > 0)
                    {
                        return peoperty.ElementAt(i);
                    }

                }
            }
            catch (Exception)
            {
                throw new Exception();//חריגה
            }

        }
        //שיוך חבילה לרחפן
        public void AssignPackageToDrone(int id)
        {
            IDAL.DO.Drone dronidal = accessIDal.GetDrone(id);
            if ((id != dronidal.Id) || id < 0)
                throw new ArgumentOutOfRangeException("id", "The drone number is error");
            for (int i = 0; i < BlDrone.Count; i++)
            {
                if (BlDrone[i].Id == id)
                {
                    DroneToList drone = BlDrone[i];
                    IDAL.DO.Parcel pp = MIUNParcelByGood(drone.Id);//קיבלתי את המשלוח לפי העדיפות טובה
                    pp.Scheduled = DateTime.Now;//זמן שיוך עכשיו
                    drone.StatusDrone = StatusDrone.delivered;//שינוי מצב רחפן
                    accessIDal.AssignPackageToDrone(pp.Id, id);//שליחת הרחפן והחבילה לשיכבת הנתונים
                    accessIDal.DeleteParcel(pp.Id);//קיבלנו עצם מועתק
                    accessIDal.AddParcel(pp);
                    BlDrone[i] = drone;//לשנות לאחר שינויים
                }
            }
        }
        public void PackageDeliveryByDrone(int Id)//אספקת חבילה ע"י רחפן
        {
            //רחפן שאסף ולא סיפק את החבילה
            //IDAL.DO.Parcel dop = accessIDal.GetParcel(idP);
            //if(StatuseParcelKnowBool(int idP))
            IEnumerable<IDAL.DO.Parcel> a = accessIDal.GetAllParcel();
           
            for(int i=0;i<a.Count();i++)
            {
                if(a.ElementAt(i).Droneld== Id)
                {
                    IDAL.DO.Parcel parcel = a.ElementAt(i);

                    if(parcel.Delivered!=default)//אספו
                   
                        throw new Exception();
                    
                       for (int j = 0; j < BlDrone.Count; j++)
                        {
                        if (BlDrone[j].Id == Id)
                        {
                            DroneToList drone = BlDrone[j];


                            double distance = DistanceTo(accessIDal.GetCustomer(parcel.Senderld).Lattitude, accessIDal.GetCustomer(parcel.Senderld).Longitude, accessIDal.GetCustomer(parcel.Targetld).Longitude, accessIDal.GetCustomer(parcel.Targetld).Longitude);
                            drone.StatusBatter -= BatteryConsumption(distance, (WeightCategories)parcel.Weight);
                            drone.LocationDrone.Latitude = accessIDal.GetCustomer(parcel.Senderld).Lattitude;
                            drone.LocationDrone.Longitude = accessIDal.GetCustomer(parcel.Senderld).Longitude;
                            parcel.PichedUp = DateTime.Now;
                        }
                            

                        }
                        }

                }
            }
            
        }
       
    }

    
}
