using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DalApi;


namespace Dal
{
    sealed class DalXml : IDal
    {
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() { }

        string dronsPath = @"DronsXml.xml"; //XElement

        string dronschargePath = @"DronsCharcheXml.xml"; //XMLSerializer
        string parcelsPath = @"ParcelXml.xml"; //XMLSerializer
        string stationsPath = @"StationsXml.xml"; //XMLSerializer
       

        #region Drone
        //CRUD Drone
        public int GetDroneDoSndByParcelId(int idp)//מחזיר את ת"ז של הרחפ ןשמבצע את המשלוח
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            bool fal = ListParcel.Any(p => p.Id == idp);
        if (fal == false)
            throw new DO.ParcelDoesNotExistException($"bad Parcle id: {idp}");
        else
            return ListParcel.Find(p => p.Id == idp).Droneld;

    }
    public IEnumerable<DO.Drone> ListDroneInStation(int id)
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            return (IEnumerable<DO.Drone>)(from Drone in ListDrons
                                           select GetStation(id));
    }


    public DO.Drone GetDrone(int id)
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            DO.Drone drone = ListDrons.Find(p => p.Id == id);
            bool fal = ListDrons.Any(p => p.Id == id);
            if (fal== false)
            throw new DO.DroneDoesNotExistException($"bad drone id: {id}");
        else
            return drone;
    }

    public IEnumerable<DO.Drone> GetAllDrone()
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            return from Drone in ListDrons
                   select Drone;
    }

    public IEnumerable<DO.Drone> GetAllDroneBy(Predicate<DO.Drone> predicate)
    {
        throw new NotImplementedException();//זריקה
    }

    public void AddDrone(DO.Drone drone)
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            if (ListDrons.Any(p => p.Id == drone.Id))
        {
            throw new DO.DroneAlreadyExistException($"bad drone id: {drone.Id}");
        }
            ListDrons.Add(drone);
            XMLTools.SaveListToXMLSerializer(ListDrons, dronsPath);
        }

    public void DeleteDrone(int id)
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            bool fal = ListDrons.Any(p => p.Id == id);
        DO.Drone per = ListDrons.Find(p => p.Id == id);
        
        if (fal == false)
            throw new DO.DroneDoesNotExistException($"bad drone id: {id}");
            else
            {
                ListDrons.Remove(per);
                XMLTools.SaveListToXMLSerializer(ListDrons, dronsPath);
            }
               



    }

    public void UpdetDrone(DO.Drone drone)
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            bool fal = ListDrons.Any(p => p.Id == drone.Id);
            DO.Drone per = ListDrons.Find(p => p.Id == drone.Id);

            if (fal == false)//לא קיים
                throw new DO.DroneDoesNotExistException($"bad drone id: {drone.Id}");
            else
            {
                ListDrons.Remove(per);
                ListDrons.Add(drone);
                XMLTools.SaveListToXMLSerializer(ListDrons, dronsPath);
            }


        }

    public void UpdetDrone(int id, Action<DO.Drone> action)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Drone> DdroneList()
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            return ListDrons;
    }
    public IEnumerable<int> GetListDrone()//מחזיר רשימה של רחפנים רק הת"ז שלהם
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            List<int> a = new List<int>();
        foreach (DO.Drone item in ListDrons)
        {
            a.Add(item.Id);
        }
        return a;
    }
        #endregion
        #region Station
        //CRU
        public IEnumerable<DO.Station> ReturnStationHaveFreeCharde()//מחזיר רשימה של תחנות שיש להם כמות טעינה גדולה מ0
        {
            List<DO.Station> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            List<DO.Station> ss = new List<DO.Station>();
           
            for (int i = 0; i < ListDrons.Count(); i++)
            {
                if (ListDrons[i].ChargeSlots > 0)
                    ss.Add(ListDrons[i]);
            }
            return ss;
        }
        public IEnumerable<DO.Station> SStationList()
        {
            List<DO.Station> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            return ListDrons;
        }

        public DO.Station GetStation(int id)
        {
            List<DO.Station> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            bool fal = ListDrons.Any(p => p.Id == id);
            DO.Station per = ListDrons.Find(p => p.Id == id);
            if (fal == true)//נימצא
            {
                return per;
            }

            else
                throw new DO.StationDoesNotExistException($"bad Station id: {id}");

        }

        public IEnumerable<DO.Station> GetAllStation()
        {
            List<DO.Station> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);

            return from Station in ListDrons
                   select Station;
        }

        public IEnumerable<DO.Station> GetAllStationBy(Predicate<DO.Station> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddStation(DO.Station station)
        {
            List<DO.Station> ListStation = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            if (ListStation.Any(p => p.Id == station.Id))
            {//נימצא
                throw new DO.StationAlreadyExistsException($"bad station id: {station.Id}");
            }
            ListStation.Add(station);
            XMLTools.SaveListToXMLSerializer(ListStation, stationsPath);

        }

        public void DeleteStation(int id)
        {
            List<DO.Station> ListStation = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            DO.Station per = ListStation.Find(p => p.Id == id);
            bool fal = ListStation.Any(p => p.Id == id);
            if (fal == true)//נימצא
            {
                ListStation.Remove(per);
                XMLTools.SaveListToXMLSerializer(ListStation, stationsPath);
            }
                
            else
                throw new DO.StationDoesNotExistException($"bad station id: {id}");

        }

        public void UpdetStation(DO.Station station)
        {
            List<DO.Station> ListStation = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            DO.Station per = ListStation.Find(p => p.Id == station.Id);
            bool fal = ListStation.Any(p => p.Id == station.Id);
            // IDAL.DO.Station per = DataSource.stationsList.Find(p => p.Id == station.Id);
            if (fal == true)//נימצא
            {
                ListStation.Remove(per);//מחיקה
                ListStation.Add(station);//הוספה מעודכן
                XMLTools.SaveListToXMLSerializer(ListStation, stationsPath);
            }

            else
                throw new DO.StationDoesNotExistException($"bad drone id: {station.Id}");

        }

        public void UpdetStation(int id, Action<DO.Station> action)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Parcel
        public IEnumerable<DO.Parcel> PparcelList()//return list
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            return ListParcel;
        }
        public DO.Parcel GetParcel(int id)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            DO.Parcel per = ListParcel.Find(p => p.Id == id);
            bool fal = ListParcel.Any(p => p.Id == id);
            if (fal == false)
                throw new DO.ParcelDoesNotExistException($"bad Parcle id: {id}");
            else
                return per;
        }
        public DO.Parcel GetParcelByDrone(int id)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            DO.Parcel per = ListParcel.Find(p => p.Droneld == id);
            bool fal = ListParcel.Any(p => p.Droneld == id);
            if (fal == false)
                throw new DO.DroneDoesNotExistException($"bad drone id: {id}");
            else
                return per;
        }


        public IEnumerable<DO.Parcel> GetAllParcel()
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            return from Parcel in ListParcel
                   select Parcel;
        }

        public IEnumerable<DO.Parcel> GetAllParcelBy(Predicate<DO.Parcel> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddParcel(DO.Parcel parcel)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            if (ListParcel.Any(p => p.Id == parcel.Id))
            {
                throw new DO.ParcelAlreadyExistsException($"bad parcel id: {parcel.Id}");
            }
            ListParcel.Add(parcel);
            XMLTools.SaveListToXMLSerializer(ListParcel, parcelsPath);

        }


        public void DeleteParcel(int id)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            DO.Parcel per = ListParcel.Find(p => p.Droneld == id);
            bool fal = ListParcel.Any(p => p.Id == id);

            if (fal == true)//Bימצא{
            {
                ListParcel.Remove(per);
                XMLTools.SaveListToXMLSerializer(ListParcel, parcelsPath);

            }
                
            else
                throw new DO.ParcelDoesNotExistException($"bad Parcel id: {id}");


        }

        public void UpdetParcel(DO.Parcel parcel)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            DO.Parcel per = ListParcel.Find(p => p.Droneld == parcel.Id);
            bool fal = ListParcel.Any(p => p.Id == parcel.Id);
            if (fal == true)//Bימצא
            {
                ListParcel.Remove(per);//מחיקה
                ListParcel.Add(parcel);//הוספה מעודכן
                XMLTools.SaveListToXMLSerializer(ListParcel, parcelsPath);
            }

            else
                throw new DO.ParcelDoesNotExistException($"bad parcel id: {parcel.Id}");

        }

        public void UpdetParcel(int id, Action<DO.Parcel> action)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region DroneCharg
      

        public int CoutCharge(int id)//בדיקה כמה עמדות טעינה תפוסים ישש לתחנה מסויימת
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            int mone = 0;
            for (int i = 0; i < ListDronsCharge.Count(); i++)
            {
                if (ListDronsCharge[i].Stationld == id)
                {
                    mone++;
                }

            }
            return mone;
        }

        public DO.DroneCharge GetDroneChargByDrone(int id)
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            bool fal = ListDronsCharge.Any(p => p.Droneld == id);
            if (fal == true)//Bימצא
                return ListDronsCharge.Find(p => p.Droneld == id);
            else
                throw new DO.DroneChargDoesNotExistException($"bad Drone id: {id}");
        }
        public DO.DroneCharge GetDroneChargByStation(int id)
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            bool fal = ListDronsCharge.Any(p => p.Stationld == id);
            if (fal == true)//Bימצא
                return ListDronsCharge.Find(p => p.Stationld == id);
            else
                throw new DO.StationDoesNotExistException($"bad station id: {id}");
        }

        public IEnumerable<int> GetDroneChargByStationListInt(int ids)//מחזיר רשימה של ת"ז של רחפנים שנימצאים בתחנה מסויימת
        {
            List<int> a = new List<int>();
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            for (int i = 0; i < ListDronsCharge.Count(); i++)
            {
                if (ListDronsCharge[i].Stationld == ids)
                {
                    a.Add(ListDronsCharge[i].Droneld);
                }
            }
            return a;
        }
        public int MoneDroneChargByStationListInt(int ids)//מחזיר כמוצ של ת"ז של רחפנים שנימצאים בתחנה מסויימת
        {
            int mone = 0;
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            for (int i = 0; i < ListDronsCharge.Count(); i++)
            {
                if (ListDronsCharge[i].Stationld == ids)//כמה רחפנים יש בטעינה סתחנה מסויימת
                {
                    mone++;
                }
            }
            return mone;
        }

        public IEnumerable<DO.DroneCharge> GetAllDroneCharge()
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            return from DroneCharge in ListDronsCharge
                   select DroneCharge;
        }

        public IEnumerable<DO.DroneCharge> GetAllDroneChargeBy(Predicate<DO.DroneCharge> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void UpdetDroneCharge(DO.DroneCharge droneCharge)
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            bool fal = ListDronsCharge.Any(p => p.Stationld == droneCharge.Stationld);

            if (fal == true)//Bימצא
            {
                ListDronsCharge.Remove(ListDronsCharge.Find(p => p.Stationld == droneCharge.Stationld));//מחיקה

                ListDronsCharge.Add(droneCharge);//הוספה מעודכן
                XMLTools.SaveListToXMLSerializer(ListDronsCharge, dronschargePath);
            }

            else
                throw new DO.DroneChargDoesNotExistException($"bad station id: {droneCharge.Stationld}");

        }

        public void UpdetDroneCharge(int id, Action<DO.DroneCharge> action)
        {
            throw new NotImplementedException();
        }

        public void SendDroneToCharge(int station, int drone)//Drone a skimmer for charging
        {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            List<DO.DroneCharge> ListDroneCahrg = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            bool fal1 = ListStations.Any(p => p.Id == station);
            bool fal2 = ListDrons.Any(p => p.Id == drone);
            DO.Drone drone1 = ListDrons.Find(p => p.Id == drone);
            DO.Station station1 = ListStations.Find(p => p.Id == station);

            if (fal1 == true & fal2 == true)//שתיה  נימצאו
            {
                station1.ChargeSlots--;
                UpdetStation(station1);//update
                ListDroneCahrg.Add(new DO.DroneCharge { Droneld = drone, Stationld = station });//add to list
                XMLTools.SaveListToXMLSerializer(ListDroneCahrg, dronschargePath);
                XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
            }
        }
        public void ReleaseDroneFromCharging(int drone)//Drone a skimmer from charging
        {
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            bool fal2 = ListDronsCharge.Any(p => p.Droneld == drone);
            DO.DroneCharge drone1 = ListDronsCharge.Find(p => p.Droneld == drone);
            if (fal2 == true)
            {
                DO.Station station1 = ListStations.Find(p => p.Id == drone1.Stationld);
                station1.ChargeSlots++;
                UpdetStation(station1);
                ListDronsCharge.Remove(drone1);
                XMLTools.SaveListToXMLSerializer(ListDronsCharge, dronschargePath);
                XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);

            }
        }

        public void AddDroneCharge(DO.DroneCharge cs)//מוסיף רחפנים לעמדות טעינה add drone to the charge spot
        {
            SendDroneToCharge(cs.Stationld, cs.Droneld);

        }
        #endregion
        IEnumerable<int> IDal.ListTargetParcel(int idta)
        {
            throw new NotImplementedException();
        }

        IEnumerable<int> IDal.ListSendetParcel(int idse)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DO.Customer> IDal.GetAllCustomer()
        {
            throw new NotImplementedException();
        }

        IEnumerable<DO.Customer> IDal.GetAllCustomerBy(Predicate<DO.Customer> predicate)
        {
            throw new NotImplementedException();
        }

        DO.Customer IDal.GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        void IDal.AddCustomer(DO.Customer customer)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdetCustomer(DO.Customer customer)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdetCustomer(int id, Action<DO.Customer> action)
        {
            throw new NotImplementedException();
        }

        void IDal.DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DO.Customer> IDal.CcustomerList()
        {
            throw new NotImplementedException();
        }

        double[] IDal.RequestPowerConsuption()
        {

            double[] arr = new double[5];
            //arr[0] = DataSource.Config.Free;//פנוי
            //arr[1] = DataSource.Config.LightWeight;
            //arr[2] = DataSource.Config.MediumWeight;
            //arr[3] = DataSource.Config.HeavyWeight;
            //arr[4] = DataSource.Config.LoadingPrecents;
            return arr;
        }

        IEnumerable<double> IDal.ElectricityUse()
        {
            throw new NotImplementedException();
        }

        void IDal.AssignPackageToDrone(int idParcel, int idDrone)
        {
            throw new NotImplementedException();
        }

        void IDal.PackageCollectionByDrone(int idParcel)
        {
            throw new NotImplementedException();
        }

        void IDal.DeliveryOfPackageToTheCustomer(int idParcel)
        {
            throw new NotImplementedException();
        }

       
    }
}