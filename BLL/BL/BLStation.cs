using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;

namespace BL
{
    sealed partial class BL : IBL
    {

        private IEnumerable<DroneInCharge> ListDroneInStation(int idS)//return list of drone in charge it halp to station
        {
            int moneDroneInCharge= accessIDal.MoneDroneChargByStationListInt(idS);//כמות הרחפנים שיש תלחנה
            if (moneDroneInCharge > 0)
            {
                List<int> ListDroneId;//vv
                ListDroneId = (List<int>)accessIDal.GetDroneChargByStationListInt(idS);
                List<DroneInCharge> a = new List<DroneInCharge>();
                for (int i = 0; i < ListDroneId.Count(); i++)
                {
                    DroneInCharge droneInCharge = new DroneInCharge() { Id = ListDroneId[i], StatusBatter = GetDrone(ListDroneId[i]).StatusBatter };
                    a.Add(droneInCharge);
                }
                return a;

            }
            return null;

        }


        public BO.Station GetStation(int id)// v
        {
            BO.Station c = new BO.Station();
            try
            {
                Station station = accessIDal.GetStation(id); 

                c.Id = station.Id;
                c.Name = station.Name;
                Location newBo = new Location() { Longitude = station.Longitude, Latitude = station.Latitude };
                c.LocationStation = newBo;
                c.ChargeSlotsFree = station.ChargeSlots; 
                c.DroneInChargeList = ListDroneInStation(id);
            }
            catch (Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }
        public void AddStation(Station station)//v
        {
            Station station1 = new Station() { Id = station.Id, Name = station.Name, ChargeSlots = station.ChargeSlotsFree, Longitude = station.LocationStation.Longitude, Latitude = station.LocationStation.Latitude };
            try
            {
                accessIDal.AddStation(station1);
            }
            catch(Excptions) {
                throw new AlreadyExistException();
            }
           
        }
        public void UpdateStation(int idS, int names,int chargeSlote)//v
        {

            Station c;
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
                //Console.WriteLine(c.ToString());
                accessIDal.UpdetStation(c);

            }
            catch (Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }
      
       

        public BO.StationToList StationToListToPrint(int id)
        {
            
            StationToList c = new StationToList();
            try
            {
                Station station = accessIDal.GetStation(id);
                c.Id = station.Id;
                c.Name = station.Name;
                c.ChargeSlotsNotFree = accessIDal.CoutCharge(id);
                c.ChargeSlotsFree = station.ChargeSlots;
             
            }
            catch (Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }


        public IEnumerable<int> AvailableStationToChargeListt()//return list station who have available to charge 
        {
            IEnumerable<Station> a = accessIDal.GetAllStation();
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots > 0)
                    b.Add(a.ElementAt(i).Id);
               
            }
            return b;

        }
            public IEnumerable <Station> AvailableStationToChargeList()//ptint list station who have available to charge 
        {
            
            IEnumerable<Station> a = (IEnumerable<Station>)accessIDal.GetAllStation();
            List < Station > b= new List<Station>();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots > 0)
                    b.Add(a.ElementAt(i));
            }
            return b;
        }

    }
}
