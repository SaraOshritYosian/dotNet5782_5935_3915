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

        

        public BO.Station GetStation(int id)// v
        {
            BO.Station c = new BO.Station();
            try
            {
                IDAL.DO.Station station = accessIDal.GetStation(id);
                c.Id = station.Id;
                c.Name = station.Name;
                c.LocationStation.Latitude = station.Latitude;
                c.LocationStation.Longitude = station.Longitude;
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

            BO.Station c = new BO.Station();
            try
            {
                c = GetStation(idS);
                if (names !=-1 )
                {
                    c.Name = names;
                }
                if (chargeSlote !=-1 )
                {
                    c.ChargeSlotsFree = chargeSlote-accessIDal.coutCharge(idS);
                }


            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }
      
        private IEnumerable<BO.DroneInCharge> ListDroneInStation(int idS)//return list of drone in charge it halp to station
        {
            List<int> ListDroneId = new List<int>();//vv
            ListDroneId = (List<int>)accessIDal.GetDroneChargByStationListInt(idS);
            List<BO.DroneInCharge> a = new List<BO.DroneInCharge>();
            for (int i = 0; i < ListDroneId.Count(); i++)
            {
                DroneInCharge droneInCharge = new DroneInCharge() { Id = ListDroneId[i], StatusBatter = GetDrone(ListDroneId[i]).StatusBatter };
                a.Add(droneInCharge);
            }
            return a;

        }

        public BO.StationToList StationToListToPrint(int id)
        {
            BO.StationToList c = new BO.StationToList();
            try
            {
                IDAL.DO.Station station = accessIDal.GetStation(id);
                c.Id = station.Id;
                c.Name = station.Name;
                c.ChargeSlotsNotFree = accessIDal.coutCharge(id);
                c.ChargeSlotsFree = station.ChargeSlots;
             
            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }
        public void PrintStationList()//print all station
        {
            IEnumerable<IDAL.DO.Station> a = accessIDal.GetAllStation();
            for(int i=0;i<a.Count(); i++)
            {
                Console.WriteLine(StationToListToPrint(a.ElementAt(i).Id));
            }
        }
        public void PrintStationById(int ids)//print station get id of station
        {
            Console.WriteLine(GetStation(ids).ToString());
        }
        public void PrintAvailableStationToChargeList()//ptint list station who have available to charge 
        {
            IEnumerable<IDAL.DO.Station> a = accessIDal.GetAllStation();
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).ChargeSlots>0)
                    Console.WriteLine(StationToListToPrint(a.ElementAt(i).Id));

            }
        }

    }
}
