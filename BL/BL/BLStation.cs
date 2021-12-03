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
        public void AddStation(Station station)
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

        public void UpdateStation(int idS, int names,int chargeSlote)
        {

            IDAL.DO.Station station1 = new IDAL.DO.Station() { Id = station.Id, Name = station.Name, ChargeSlots = station.ChargeSlotsFree, Longitude = station.LocationStation.Longitude, Latitude = station.LocationStation.Latitude };
            try
            {
                accessIDal.AddStation(station1);
            }
            catch (IDAL.DO.Excptions)
            {
                throw new AlreadyExistException();
            }
        }
        //public IEnumerable<BO.Station> StationList()
        //{
        //    return from item in accessIDal.sStationList()
        //           select getStation(item.Id);
        //}
        public void PrindBaseStationList()
        // הצגת רשימת תחנות-בסיס show base-station list
        {
            foreach (Station station in accessIDal.DataSource.stationsList/*Drone dr in DataSource.dronsList*/)
            {
                Console.WriteLine(station.ToString());
            }

        }


    }
}
