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

        public BO.PackageInTransfer GetParcelInTransfer(int idD)//  וצריך לחשב מרחק
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
                    far =//צריך לחשב את המרחק
                   
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

        
        //public void PickUpPackage(int id)//איסוף חבילה על ידי רחפן
        //{
        //    if (id < 0)
        //        throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");

        //    DroneToList drone = BlDrone.Find(d => d.Id == id);
        //    if (drone == default(DroneToList))
        //        throw new ArgumentException("Drone with the given ID number doesn't exist");

        //    if (drone.StatusDrone != StatusDrone.delivered)
        //        throw new InvalidOperationException("The drone is not assigned to any package");

        //    var parcel = id.GetParcel(drone.IdParcel);
        //    if (parcel.Scheduled == default(DateTime) || parcel.PickedUp != default(DateTime))//ביקשתי את החריגה הזאת
        //        throw new InvalidOperationException("The package is not ready to pick up");

        //    try
        //    {

        //        var sender = accessIDal.GetCustomer(parcel.Senderld);
        //        double distance = DistanceTo(drone.LocationDrone.Latitude, sender.Lattitude,
        //            drone.LocationDrone.Longitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
        //        drone.LocationDrone = new Location//עדכון מיקום למיקום שולח
        //        {
        //            Latitude = sender.Lattitude,
        //            Longitude = sender.Longitude
        //        };
        //        drone.StatusBatter -= BatteryConsumption(distance, drone.Weight);
        //        accessIDal.PackageCollectionByDrone(parcel.Id);

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception();
        //    }
        //}

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
                if(GetParcel((a.ElementAt(i).Id)).Scheduled==default)
                    Console.WriteLine(ParcelToListToPrint(a.ElementAt(i).Id));

            }
        }
        private StatusParcel StatuseParcelKnow(int idP) {//return the statuse of parcel
            IDAL.DO.Parcel dop = accessIDal.GetParcel(idP);

            if (dop.Delivered.Date !=default)//סופק
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
                        double distance = Distance(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, sender.Lattitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
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
    }

    
}
