﻿using System;
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

        public IEnumerable<BO.DroneInCharge> ListDroneInStation(int idS)//return list of drone in charge it halp to station
        {
            int moneDroneInCharge= accessIDal.MoneDroneChargByStationListInt(idS);//כמות הרחפנים שיש תלחנה
            if (moneDroneInCharge > 0)
            {
                List<int> ListDroneId;//vv
                ListDroneId = (List<int>)accessIDal.GetDroneChargByStationListInt(idS);
                List<BO.DroneInCharge> a = new List<BO.DroneInCharge>();
                for (int i = 0; i < ListDroneId.Count(); i++)
                {
                    BO.DroneInCharge droneInCharge = new BO.DroneInCharge() { Id = ListDroneId[i], StatusBatter = GetDrone(ListDroneId[i]).StatusBatter };
                    a.Add(droneInCharge);
                }
                return a;

            }
            return null;

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station GetStationByDrone(int idd)// v
        {
            BO.Station c = new BO.Station();
            try
            {
                DO.DroneCharge droneCharge = accessIDal.GetDroneChargByDrone(idd);
                DO.Station station = accessIDal.GetStation(droneCharge.Stationld);

                c.Id = station.Id;
                c.Name = station.Name;
                BO.Location newBo = new BO.Location() { Longitude = station.Longitude, Latitude = station.Latitude };
                c.LocationStation = newBo;
                c.ChargeSlotsFree = station.ChargeSlots;
                c.DroneInChargeList = ListDroneInStation(station.Id);
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station GetStation(int id)// v
        {
            BO.Station c = new BO.Station();
            try
            {
                DO.Station station = accessIDal.GetStation(id); 

                c.Id = station.Id;
                c.Name = station.Name;
                BO.Location newBo = new BO.Location() { Longitude = station.Longitude, Latitude = station.Latitude };
                c.LocationStation = newBo;
                c.ChargeSlotsFree = station.ChargeSlots; 
                c.DroneInChargeList = ListDroneInStation(id);
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(BO.Station station)//v
        {
            if (station.LocationStation.Longitude > 32.1027879 || station.LocationStation.Longitude < 32.056227 || station.LocationStation.Latitude < 34.75948 || station.LocationStation.Longitude > 34.8007048)
                throw  new BO.Excptions("We are sorry but you do not live in Tel Aviv");
            DO.Station station1 = new DO.Station() { Id = station.Id, Name = station.Name, ChargeSlots = station.ChargeSlotsFree, Longitude = station.LocationStation.Longitude, Latitude = station.LocationStation.Latitude };
            try
            {
                accessIDal.AddStation(station1);
            }
            catch(BO.Excptions) {
                throw new BO.AlreadyExistException();
            }
           
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int idS, int names,int chargeSlote)//v
        {

            DO.Station c;
            try
            {
               
                c =accessIDal.GetStation(idS);
                //Console.WriteLine(c.ToString());
                if (names !=-1 )
                {
                    c.Name = names;
                }
                if (chargeSlote !=-1 )
                {
                    c.ChargeSlots = chargeSlote-accessIDal.CoutCharge(idS);
                }
                if (c.ChargeSlots <= 0)
                {
                    throw new BO.Excptions("Impossible");
                }
                //Console.WriteLine(c.ToString());
                accessIDal.UpdetStation(c);

            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.StationToList StationToListToPrint(int id)
        {
            
            BO.StationToList c = new BO.StationToList();
            try
            {
                DO.Station station = accessIDal.GetStation(id);
                c.Id = station.Id;
                c.Name = station.Name;
                c.ChargeSlotsNotFree = accessIDal.CoutCharge(id);
                c.ChargeSlotsFree = station.ChargeSlots;
             
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> AvailableStationToChargeListt()//return list station who have available to charge 
        {
            IEnumerable<DO.Station> a = accessIDal.GetAllStation();
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots > 0)
                    b.Add(a.ElementAt(i).Id);
               
            }
            return b;

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable <BO.Station> AvailableStationToChargeList()//ptint list station who have available to charge 
        {
            
            IEnumerable<DO.Station> a = accessIDal.GetAllStation();
            List < BO.Station > b= new List<BO.Station>();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots > 0)
                    b.Add(GetStation( a.ElementAt(i).Id));
            }
            return b;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.StationToList> AvailableStationToChargeListStationToList()//ptint list station who have available to charge 
        {

            IEnumerable<DO.Station> a = accessIDal.GetAllStation();
            List<BO.StationToList> b = new List<BO.StationToList>();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots > 0)
                    b.Add(StationToListToPrint(a.ElementAt(i).Id));
            }
            return b;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.StationToList> GetStations()
        {
            List<BO.StationToList> b = new List<BO.StationToList>();
            IEnumerable<DO.Station> a = accessIDal.GetAllStation();
            for (int i = 0; i < a.Count(); i++)
            { 
                    b.Add(StationToListToPrint(a.ElementAt(i).Id));
            }
            return b;
        }
        

    }
}
