﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DalApi;
using Dal;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    sealed class DalXml : IDal
    {
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() { }

        string dronsPath = @"DronsXml.xml"; //XElement
        string customerPath = @"CustomerXml.xml";
        string dronschargePath = @"DronsCharcheXml.xml"; //XMLSerializer
        string parcelsPath = @"ParcelXml.xml"; //XMLSerializer
        string stationsPath = @"StationsXml.xml"; //XMLSerializer
        string configPath = @"Config.xml";



        #region Drone
        //CRUD Drone
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> ListDroneInStation(int id)
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            return (IEnumerable<DO.Drone>)(from Drone in ListDrons
                                           select GetStation(id));
    }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> GetAllDrone()
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            return from Drone in ListDrons
                   select Drone;
    }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> GetAllDroneBy(Predicate<DO.Drone> predicate)
    {
        throw new NotImplementedException();//זריקה
    }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdetDrone(int id, Action<DO.Drone> action)
    {
        throw new NotImplementedException();
    }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> DdroneList()
    {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            return ListDrons;
    }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> GetAllStation()
        {
            List<DO.Station> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);

            return from Station in ListDrons
                   select Station;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> GetAllStationBy(Predicate<DO.Station> predicate)
        {
            throw new NotImplementedException();//זריקה
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdetStation(int id, Action<DO.Station> action)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> PparcelList()//return list
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            return ListParcel;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel GetParcelByDrone(int id)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            DO.Parcel per = ListParcel.Find(p => p.Droneld == id&&p.Delivered==default);//לא סופק
            bool fal = ListParcel.Any(p => p.Droneld == id && p.Delivered == default);
            if (fal == false)
                throw new DO.DroneDoesNotExistException($"bad drone id: {id}");
            else
                return per;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> GetAllParcel()
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            return from Parcel in ListParcel
                   select Parcel;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> GetAllParcelBy(Predicate<DO.Parcel> predicate)
        {
            throw new NotImplementedException();//זריקה
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(DO.Parcel parcel)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            XElement aa = XMLTools.LoadListFromXMLElement(configPath);
            int codd = Convert.ToInt32(aa.Element("RowNumbers").Value);
           // int codd = XMLTools.LoadListFromXMLElement(configPath).Element("RowNumbers").Elements().Select(e => Convert.ToInt32(e.Value)).FirstOrDefault();//יש תמספר חבילה

            Parcel p = new Parcel();
            p.Senderld = parcel.Senderld;
            p.Targetld = parcel.Targetld;
            p.Weight = parcel.Weight;
            p.Priority = parcel.Priority;
            p.Droneld = parcel.Droneld;
            p.Requested = parcel.Requested;
            p.Scheduled = parcel.Scheduled;
            p.PichedUp = parcel.PichedUp;
            p.Delivered = parcel.Delivered;           
            p.Id = codd++;//המספר של החבילה

            aa.Element("RowNumbers").Value = codd.ToString();
           // aa.Value = codd.ToString(); 
            
           XMLTools.SaveListToXMLElement(aa, configPath);
            ListParcel.Add(p);
            
            XMLTools.SaveListToXMLSerializer(ListParcel, parcelsPath);
            return p.Id;

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            DO.Parcel per = ListParcel.Find(p => p.Id == id);
            bool fal = ListParcel.Any(p => p.Id == id);

            if (fal == true)//Bימצא{
            {
                ListParcel.Remove(per);
                XMLTools.SaveListToXMLSerializer(ListParcel, parcelsPath);

            }
                
            else
                throw new DO.ParcelDoesNotExistException($"bad Parcel id: {id}");


        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdetParcel(DO.Parcel parcel)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            DO.Parcel per = ListParcel.Find(p => p.Id == parcel.Id);
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdetParcel(int id, Action<DO.Parcel> action)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region DroneCharg

        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.DroneCharge GetDroneChargByDrone(int id)
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            bool fal = ListDronsCharge.Any(p => p.Droneld == id);
            if (fal == true)//Bימצא
                return ListDronsCharge.Find(p => p.Droneld == id);
            else
                throw new DO.DroneChargDoesNotExistException($"bad Drone id: {id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.DroneCharge GetDroneChargByStation(int id)
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            bool fal = ListDronsCharge.Any(p => p.Stationld == id);
            if (fal == true)//Bימצא
                return ListDronsCharge.Find(p => p.Stationld == id);
            else
                throw new DO.StationDoesNotExistException($"bad station id: {id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.DroneCharge> GetAllDroneCharge()
        {
            List<DO.DroneCharge> ListDronsCharge = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronschargePath);
            return from DroneCharge in ListDronsCharge
                   select DroneCharge;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.DroneCharge> GetAllDroneChargeBy(Predicate<DO.DroneCharge> predicate)
        {
            throw new NotImplementedException();//זריקה
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

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
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdetDroneCharge(int id, Action<DO.DroneCharge> action)
        {
            throw new NotImplementedException();
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DO.DroneCharge cs)//מוסיף רחפנים לעמדות טעינה add drone to the charge spot
        {
            SendDroneToCharge(cs.Stationld, cs.Droneld);

        }
        #endregion
        #region Customer

        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<int> IDal.ListTargetParcel(int idta)
        {
            List<int> a = new List<int>();
            foreach (Parcel item in XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath))
            {
                if (item.Targetld == idta)
                {
                    a.Add(item.Id);
                }
            }
            return a;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        IEnumerable<int> IDal.ListSendetParcel(int idse)
        {
            List<int> a = new List<int>();
            foreach (Parcel item in XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath))
            {
                if (item.Senderld == idse)
                {
                    a.Add(item.Id);
                }
            }
            return a;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<DO.Customer> IDal.GetAllCustomer()
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customerPath);
            //if(customersRootElem!=null)
            return (from p in customersRootElem.Elements()
                    select new Customer()
                    {
                        Id = Int32.Parse(p.Element("Id").Value),
                        Name = p.Element("Name").Value,
                        Pone = p.Element("Pone").Value,
                        Longitude =double.Parse(p.Element("Longitude").Value),
                        Lattitude = double.Parse(p.Element("Lattitude").Value),
                        
                        
                    }
                   );

           
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<DO.Customer> IDal.GetAllCustomerBy(Predicate<DO.Customer> predicate)
        {
            XElement customerRootElem = XMLTools.LoadListFromXMLElement(customerPath);

            return from p in customerRootElem.Elements()
                   let p1 = new Customer()
                   {
                       Id = Int32.Parse(p.Element("ID").Value),
                       Name = p.Element("Name").Value,
                       Pone = p.Element("Pone").Value,
                       Longitude = Int32.Parse(p.Element("Longitude").Value),
                       Lattitude = Int32.Parse(p.Element("Lattitude").Value),
                       
                   }
                   where predicate(p1)
                   select p1;
           
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        DO.Customer IDal.GetCustomer(int id)
        {
            XElement customerRootElem = XMLTools.LoadListFromXMLElement(customerPath);

            Customer p = (from per in customerRootElem.Elements()
                        where int.Parse(per.Element("Id").Value) == id
                        select new Customer()
                        {
                            Id = Int32.Parse(per.Element("Id").Value),
                            Name = per.Element("Name").Value,
                            Pone = per.Element("Pone").Value,
                            Lattitude = double.Parse(per.Element("Lattitude").Value),
                            Longitude = double.Parse(per.Element("Longitude").Value),
                            //ListOfPackagesFromTheCustomer=
                            //City = per.Element("City").Value,
                            //BirthDate = DateTime.Parse(per.Element("BirthDate").Value),
                            //PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), per.Element("PersonalStatus").Value),
                            //Duration = TimeSpan.ParseExact(per.Element("Duration").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                        }
                        ).FirstOrDefault();

        //    if (p == null)
              //  throw new DO.CustomerAlreadyExistsException();

            return p;
            
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void IDal.AddCustomer(DO.Customer customer)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customerPath);

            XElement per1 = (from p in customersRootElem.Elements()
                             where int.Parse(p.Element("Id").Value) == customer.Id
                             select p).FirstOrDefault();

            if (per1 != null)
                throw new DO.CustomerAlreadyExistsException();

            XElement customerElem = new XElement("Customer", new XElement("Id", customer.Id),
                                  new XElement("Name", customer.Name),
                                  new XElement("Pone", customer.Pone),
                                  new XElement("Longitude", customer.Longitude.ToString()),
                                   new XElement("Lattitude", customer.Lattitude.ToString())
                                );

            customersRootElem.Add(customerElem);

            XMLTools.SaveListToXMLElement(customersRootElem, customerPath);
           
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void IDal.UpdetCustomer(DO.Customer customer)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customerPath);

            XElement per = (from p in customersRootElem.Elements()
                            where int.Parse(p.Element("Id").Value) == customer.Id
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Element("Id").Value = customer.Id.ToString();
                per.Element("Name").Value = customer.Name;
                per.Element("Pone").Value = customer.Pone;
                per.Element("Longitude").Value = customer.Longitude.ToString();
                per.Element("Lattitude").Value = customer.Lattitude.ToString();
               

                XMLTools.SaveListToXMLElement(customersRootElem, customerPath);
            }
            else
                throw new DO.CustomerAlreadyExistsException();
       

           
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void IDal.UpdetCustomer(int id, Action<DO.Customer> action)//
        {
            throw new NotImplementedException();
        }

        void IDal.DeleteCustomer(int id)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customerPath);

            XElement per = (from p in customersRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == id
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Remove(); //<==>   Remove per from personsRootElem

                XMLTools.SaveListToXMLElement(customersRootElem, customerPath);
            }
            else
                throw new DO.CustomerAlreadyExistsException();
            throw new NotImplementedException();
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<DO.Customer> IDal.CcustomerList()
        {
            throw new NotImplementedException();
        }

        #endregion
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<double> ElectricityUse()
        {
            XElement aa = XMLTools.LoadListFromXMLElement(configPath);
            double[] arr = new double[5];
            arr[0] = Convert.ToInt32(aa.Element("BatteryUsages").Element("FreeBatteryUsing").Value);
            arr[1] = Convert.ToInt32(aa.Element("BatteryUsages").Element("LightBatteryUsing").Value); ;
            arr[2] = Convert.ToInt32(aa.Element("BatteryUsages").Element("MediumBatteryUsing").Value); ;
            arr[3] = Convert.ToInt32(aa.Element("BatteryUsages").Element("HeavyBatteryUsing").Value); ;
            arr[4] = Convert.ToInt32(aa.Element("RowNumbers").Value); ;
            return arr;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        double[] IDal.RequestPowerConsuption()
        {
            return XMLTools.LoadListFromXMLElement(configPath).Element("BatteryUsages").Elements().Select(e => Convert.ToDouble(e.Value)).ToArray();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignPackageToDrone(int idParcel, int idDrone)
        {
            List<DO.Drone> ListDrons = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronsPath);
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            Parcel per1 = ListParcel.Find(p => p.Id == idParcel);
            bool fal1 = ListParcel.Any(p => p.Id == idParcel);
            bool fal2 = ListDrons.Any(p => p.Id == idDrone);
            Drone per2 = ListDrons.Find(p => p.Id == idDrone);
            //לבדוק אם קיים
            if (fal2 == true & fal1 == true)
            {
                per1.Droneld = per2.Id;//הכנסה ת"ז של הרחפן
                per1.Scheduled = DateTime.Now;//עדכון זמן שיוך חבילה
                UpdetParcel(per1);

            }
            else
                throw new Exception("error id");

        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PackageCollectionByDrone(int idParcel)
        {
        List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
        Parcel per1 = ListParcel.Find(p => p.Id == idParcel);
        bool fal1 = ListParcel.Any(p => p.Id == idParcel);
        if (fal1 == true)
        {
            per1.PichedUp = DateTime.Now;
            UpdetParcel(per1);
        }
    }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public  void DeliveryOfPackageToTheCustomer(int idParcel)
        {
            List<DO.Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(parcelsPath);
            Parcel per1 = ListParcel.Find(p => p.Id == idParcel);
        bool fal1 = ListParcel.Any(p => p.Id == idParcel);
        if (fal1 == true)
        {
            per1.Delivered = DateTime.Now;
            UpdetParcel(per1);
        }
    }

    }
}