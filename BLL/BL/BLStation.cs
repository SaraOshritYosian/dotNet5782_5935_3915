using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public partial class BL 
    {
        private IEnumerable<BO.DroneInCharge> ListDroneInStation(int idS)//return list of drone in charge it halp to station
        {
            int moneDroneInCharge= accessIDal.MoneDroneChargByStationListInt(idS);//כמות הרחפנים שיש תלחנה
            if (moneDroneInCharge > 0)
            {
                List<int> ListDroneId;//vv
                ListDroneId = (List<int>)accessIDal.GetDroneChargByStationListInt(idS);
                List<BO.DroneInCharge> a = new List<BO.DroneInCharge>();
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
                IDAL.DO.Station station = accessIDal.GetStation(id); 

                c.Id = station.Id;
                c.Name = station.Name;
                Location newBo = new Location() { Longitude = station.Longitude, Latitude = station.Latitude };
                c.LocationStation = newBo;
                c.ChargeSlotsFree = station.ChargeSlots; 
                c.DroneInChargeList = ListDroneInStation(id);
            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }
        public void AddStation(Station station)//v
        {
            IDAL.DO.Station station1 = new IDAL.DO.Station() { Id = station.Id, Name = station.Name, ChargeSlots = station.ChargeSlotsFree, Longitude = station.LocationStation.Longitude, Latitude = station.LocationStation.Latitude };
            try
            {
                accessIDal.AddStation(station1);
            }
            catch(IDAL.DO.Excptions) {
                throw new AlreadyExistException();
            }
           
        }
        public void UpdateStation(int idS, int names,int chargeSlote)//v
        {

            IDAL.DO.Station c;
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
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }
      
       

        public BO.StationToList StationToListToPrint(int id)
        {
            
            StationToList c = new StationToList();
            try
            {
                IDAL.DO.Station station = accessIDal.GetStation(id);
                c.Id = station.Id;
                c.Name = station.Name;
                c.ChargeSlotsNotFree = accessIDal.CoutCharge(id);
                c.ChargeSlotsFree = station.ChargeSlots;
             
            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }


        public IEnumerable<int> AvailableStationToChargeListt()//return list station who have available to charge 
        {
            IEnumerable<IDAL.DO.Station> a = accessIDal.GetAllStation();
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots > 0)
                    b.Add(a.ElementAt(i).Id);
               
            }
            return b;

        }
            public IEnumerable <IDAL.DO.Station> AvailableStationToChargeList()//ptint list station who have available to charge 
        {
            
            IEnumerable<IDAL.DO.Station> a = accessIDal.GetAllStation();
            List < IDAL.DO.Station > b= new List<IDAL.DO.Station>();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots > 0)
                    b.Add(a.ElementAt(i));
            }
            return b;
        }

    }
}
