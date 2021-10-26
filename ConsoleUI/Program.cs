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


        static void Main(string[] args)
        {
            DalObject.DalObject d1 = new DalObject.DalObject();//station
            DalObject.DalObject d2 = new DalObject.DalObject();//drone
            DalObject.DalObject d3 = new DalObject.DalObject();//customer
            DalObject.DalObject d4 = new DalObject.DalObject();//parcel
            int option;
            int a;
            Console.WriteLine("Enter your choise:");
            bool b = int.TryParse(Console.ReadLine(), out option);
            switch (option)//main menu תפריט בחירות ראשי
            {
                case 1://add הוספה
                    Console.WriteLine("Enter your choise:");
                    b = int.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {

                        case 1://add staion הוספת תחנה 
                            Station st=new Station();
                            Console.WriteLine("Enter the ID of the station");
                            a = int.Parse(Console.ReadLine(),out a);
                            st.Id = a;
                            Console.WriteLine("Enter the Name of the station");
                            st.Name=Console.Read();
                            Console.WriteLine("Enter the number of ChargeSlots of the station");
                            st.ChargeSlots=Console.Read();
                            Console.WriteLine("Enter the longitude of the station");
                            st.Longitude=Console.Read();
                            Console.WriteLine("Enter the latitude of the station");
                            st.Latitude=Console.Read();
                            d1.AddStation(st);

                            break;
                        case 2://add drone הוספת רחפן
                            Drone dr = new Drone();
                            Console.WriteLine("Enter the ID of the Drone");
                            a = int.Parse(Console.ReadLine(), out a);
                            dr.Id = a;
                            Console.WriteLine("Enter the Model of the Drone");
                            dr.Model = Console.ReadLine();
                            Console.WriteLine("Enter the number of StatusDrone of the Drone");
                            dr.StatusDrone = Console.Read();
                            Console.WriteLine("Enter the number of Weight of the Drone");
                            dr.Weight = Console.Read();
                            Console.WriteLine("Enter the Battery of the Drone");
                            dr.Battery = Console.Read();
                            d2.AddDrone(dr);
                            break;
                        case 3://add customer הוספת לקוח
                            Customer cs = new Customer();
                            Console.WriteLine("Enter your ID ");
                            a = int.Parse(Console.ReadLine(), out a);
                            cs.Id = a;
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
                            Console.WriteLine("Enter the Senderld of the Parcel");
                            pr.Senderld = Console.Read();
                            Console.WriteLine("Enter the Targetld of the Parcel");
                            pr.Targetld = Console.Read();
                            Console.WriteLine("Enter the Weight of the Parcel");
                            pr.Weight = Console.Read();
                            Console.WriteLine("Enter the Priority of the Parcel");
                            pr.Priority = Console.Read();
                            Console.WriteLine("Enter the Droneld of the Parcel");
                            pr.Droneld = Console.Read();
                            d4.AddParcel(pr);
                            break;
                        default:
                            break;
                    }
                    break;
                case 2://update עדכון
                    Console.WriteLine("Enter your choise:");
                    b = int.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 1://connect שיוך חבילה לרחפן
                            break;
                        case 2://collect  איסוף חבילה ע"י רחפ ן
                            break;
                        case 3://suplly אספקת חבילה ל-לקוח
                            break;
                        case 4://charge  שליחת רחפן לטעינה בתחנת -בסיס
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
                    break;
                default:
                    break;
            }

        }
    }
}
