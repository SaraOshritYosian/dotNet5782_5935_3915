using System;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {
        
     enum menuOption {add=1, update , show, showLists,exit };//תפריט ראשי
	 enum add { staion=1,drone, customer,parcel };//תפריט הוספה
	 enum update { connect=1,collect,supply,charge, release };//תפריט עדכון
     enum show { station=1,drone,customer,parcel };//תפריט תצוגה
     enum showLists { station=1,drones,customers,parcels,unconnectedParcel,availableStationToCharge };//תפריט הצגת הרשימות
     internal static Random rand = new Random(DateTime.Now.Millisecond);//add current time

        static void Main(string[] args)
        {

            DalObject.DalObject d1 = new DalObject.DalObject();//station
            DalObject.DalObject d2 = new DalObject.DalObject();//drone
            DalObject.DalObject d3 = new DalObject.DalObject();//customer
            DalObject.DalObject d4 = new DalObject.DalObject();//parcel
            DalObject.DalObject d5 = new DalObject.DalObject();//DroneCharge
            int option;
            int a;
            bool ex = true;
            Console.WriteLine("הקש 1 לאפשרות הוספה הקש שתיים לאפשרות עדכון הקש 3 לאפשרות תצוגה הקש 4 לאפשרות הצגת רשימות הקש 5 ליציאה\n");
            bool b = int.TryParse(Console.ReadLine(), out option);
            while (ex == true )
            {
                switch (option)//main menu תפריט בחירות ראשי
                {
                    case 1://add הוספה
                        Console.WriteLine("1 add station\n");
                        Console.WriteLine("2 add Drone\n");
                        Console.WriteLine("3 add customer\n");
                        Console.WriteLine("4 add parcel\n");
                        b = int.TryParse(Console.ReadLine(), out option);
                        switch (option)
                        {

                            case 1://add staion הוספת תחנה 
                                Station st = new Station();
                                Console.WriteLine("Enter the ID of the station");
                                st.Id = Console.Read();
                                Console.WriteLine("Enter the Name of the station");
                                st.Name = Console.Read();
                                Console.WriteLine("Enter the number of latitude of the station");
                                st.Latitude = Console.Read();
                                Console.WriteLine("Enter the longitude of the station");
                                st.Longitude = Console.Read();
                                Console.WriteLine("Enter the ChargeSlots of the station");
                                st.ChargeSlots = Console.Read();
                                d1.AddStation(st);

                                break;
                            case 2://add drone הוספת רחפן
                                Drone dr = new Drone();
                                Console.WriteLine("Enter the ID of the Drone");
                                dr.Id = Console.Read();
                                Console.WriteLine("Enter the Model of the Drone");
                                dr.Model = Console.ReadLine();
                                Console.WriteLine("Enter the number of Weight of the Drone: 0-Light, 1-Medium, 2-Heavy");
                                int ch = Console.Read();
                                if (ch == 0)
                                    dr.Weight = IDAL.WeightCategories.Light;
                                if (ch == 1)
                                    dr.Weight = IDAL.WeightCategories.Medium;
                                if (ch == 2)
                                    dr.Weight = IDAL.WeightCategories.Heavy;
                                Console.WriteLine("Enter the number of StatusDrone of the Drone: 0-available, 1-InMaintenance, 2-delivered");
                                ch = Console.Read();
                                if (ch == 0)
                                    dr.StatusDrone = IDAL.Status.available;
                                if (ch == 1)
                                    dr.StatusDrone = IDAL.Status.InMaintenance;
                                if (ch == 2)
                                    dr.StatusDrone = IDAL.Status.delivered;

                                Console.WriteLine("Enter the Battery of the Drone");
                                dr.Battery = Console.Read();
                                d2.AddDrone(dr);
                                break;
                            case 3://add customer הוספת לקוח
                                Customer cs = new Customer();
                                Console.WriteLine("Enter your ID "); 
                                cs.Id = Console.Read();
                                Console.WriteLine("Enter your Name");
                                cs.Name = Console.ReadLine();

                                Console.WriteLine("Enter your Pone");
                                cs.Pone = Console.ReadLine();
                                Console.WriteLine("Enter the Longitude of the home");
                                cs.Longitude = Console.Read();
                                Console.WriteLine("Enter the latitude of the home");
                                cs.Lattitude = Console.Read();
                                d3.AddCustomer(cs);
                                break;
                            case 4://add parcel הוספת משלוח
                                Parcel pr = new Parcel();
                                Console.WriteLine("Enter the id of the Parcel");
                                pr.Senderld = Console.Read();
                                Console.WriteLine("Enter the Senderld of the Parcel");
                                pr.Senderld = Console.Read();
                                Console.WriteLine("Enter the Targetld of the Parcel");
                                pr.Targetld = Console.Read();
                                Console.WriteLine("Enter the number of Weight of the Parcel: 0-Light, 1-Medium, 2-Heavy");
                                ch = Console.Read();
                                if (ch == 0)
                                    pr.Weight = IDAL.WeightCategories.Light;
                                if (ch == 1)
                                    pr.Weight = IDAL.WeightCategories.Medium;
                                if (ch == 2)
                                    pr.Weight = IDAL.WeightCategories.Heavy;
                                Console.WriteLine("Enter the Priority of the Parcel: 0-simple, 1-quick, 2-emergency");
                                ch = Console.Read();
                                if (ch == 0)
                                    pr.Priority = IDAL.Priority.simple;
                                if (ch == 1)
                                    pr.Priority = IDAL.Priority.quick;
                                if (ch == 2)
                                    pr.Priority = IDAL.Priority.emergency;
                                
                                pr.Droneld =0;
                                pr.Requested = DateTime.Now;
                                DateTime.AddMinutes(rand);
                                pr.PichedUp = DateTime.Now;//איזה לעשות כאן
                                pr.Delivered = DateTime.Now;//איזה שעה עושים כאן?
                                int id=d4.AddParcel(pr);
                                Console.WriteLine("The number of the parcel is: " + id);
                                break;
                        }
                        break;
                    case 2://update עדכון
                        Console.WriteLine("Enter your choise:");
                        Console.WriteLine("1 Assign a package to a Drone\n");
                        Console.WriteLine("2 Package collection by Drone\n");
                        Console.WriteLine("3 Delivery of a package to the customer\n");
                        Console.WriteLine("4 Drone a Drone for charging\n");
                        Console.WriteLine("5 Release Drone from charging\n");
                        b = int.TryParse(Console.ReadLine(), out option);
                        switch (option)
                        {
                            case 1://connect שיוך חבילה לרחפן
                                Console.WriteLine("Enter The id packago");
                                int cod = Console.Read();
                                
                                

                                break;
                            case 2://collect  איסוף חבילה ע"י רחפ ן
                                Console.WriteLine("Enter The id packago");
                                 cod = Console.Read();
                                break;
                            case 3://suplly אספקת חבילה ל-לקוח
                                Console.WriteLine("Enter The id packago");
                                 cod = Console.Read();
                                break;
                            case 4://charge  שליחת רחפן לטעינה בתחנת -בסיס
                                Console.WriteLine("Enter The id drone");
                                 cod = Console.Read();

                                break;
                            case 5://שיחרור רחפן מטעינה
                                DroneCharge drc = new DroneCharge();
                                Console.WriteLine("Enter The id drone");
                                drc.Droneld= Console.Read();
                                Console.WriteLine("Enter The id station");
                                drc.Stationld= Console.Read();
                                d5.AddDroneCharge(drc);
                                break;

                            default:
                                break;
                        }
                        break;
                    case 3://show תצוגה
                        Console.WriteLine("Enter your choise:");
                        b = int.TryParse(Console.ReadLine(), out option);
                        switch (option)
                        {
                            case 1://station תצוגת תחנת-בסיס 
                                break;
                            case 2://drone  תצוגת רחפן
                                break;
                            case 3: //customer תצוגת לקוח
                                break;
                            case 4://parcel תצוגת חבילה
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4://showLists תצוגת רשימות
                        Console.WriteLine("Enter your choise:");
                        b = int.TryParse(Console.ReadLine(), out option);
                        switch (option)
                        {
                            case 1://stations  הצגת רשימת תחנות-בסיס 
                                break;
                            case 2://drones הצגת רשימת הרחפנים
                                break;
                            case 3://customers הצגת רשימת הלקוחות
                                break;
                            case 4://parcels  הצגת רשימת החבילות 
                                break;
                            case 5://unconnectedParcel הצגת רשימת חבילות שעוד לא שויכו לרחפן
                                break;
                            case 6://availableStationToCharge הצגת תחנות-בסיס עם עמדות טעינה פנויות
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5://exit יציאה
                        ex = false;
                        break;
                }
                Console.WriteLine("הקש 1 לאפשרות הוספה הקש שתיים לאפשרות עדכון הקש 3 לאפשרות תצוגה הקש 4 לאפשרות הצגת רשימות הקש 5 ליציאה\n");
                 b = int.TryParse(Console.ReadLine(), out option);
            }

        }
    }
}
