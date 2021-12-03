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

        public BO.Station GetStation(int id)//  לא בטוחה שזה טוב
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
                c.DroneInChargeList = (IEnumerable<DroneInCharge>)(IEnumerable<Station>)accessIDal.ListDroneInStation(id);

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


        public void UpdateStation(int idS, int names,int chargeSlote)
        {

            BO.Station c = new BO.Station();
            try
            {
                c = GetStation(idS);
                if (names != )
                {
                    c.Name = names;
                }
                if (chargeSlote != )
                {
                    c.ChargeSlotsFree = chargeSlote-accessIDal.coutCharge(idS);
                }


            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }
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
