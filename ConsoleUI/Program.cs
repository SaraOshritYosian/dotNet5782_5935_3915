using System;
using IDAL.DO

namespace ConsoleUI
{
    class Program
    {
     enum menuOption {add, update , show, showLists,exit };//תפריט ראשי
	 enum add { staion,drone, customer,parcel };//תפריט הוספה
	 enum update { connect,collect,supply,charge };//תפריט עדכון
     enum show { station,drone,customer,parcel };//תפריט תצוגה
     enum showLists { station,drones,customers,parcels,unconnectedParcel,valiableStationToCharge };//תפריט הצגת הרשימות


        static void Main(string[] args)
        {
            Console.WriteLine("Enter your choise:")
            switch (menuOption)
            {
                case menuOption.add:
                    switch (add)
                    {
                        case add.staion:
                            break;
                        case add.drone:
                            break;
                        case add.customer:
                            break;
                        case add.parcel:
                            break;
                        default:
                            break;
                    }
                    break;
                case menuOption.update:
                    switch (update)
                    {
                        case update.connect:
                            break;
                        case update.collect:
                            break;
                        case update.supply:
                            break;
                        case update.charge:
                            break;
                        default:
                            break;
                    }
                    break;
                case menuOption.show:
                    switch (show)
                    {
                        case show.station:
                            break;
                        case show.drone:
                            break;
                        case show.customer:
                            break;
                        case show.parcel:
                            break;
                        default:
                            break;
                    }
                    break;
                case menuOption.showLists:
                    switch (showLists)
                    {
                        case showLists.station:
                            break;
                        case showLists.drones:
                            break;
                        case showLists.customers:
                            break;
                        case showLists.parcels:
                            break;
                        case showLists.unconnectedParcel:
                            break;
                        case showLists.valiableStationToCharge:
                            break;
                        default:
                            break;
                    }
                    break;
                case menuOption.exit:
                    break;
                default:
                    break;
            }

        }
    }
}
