using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;


namespace ConsoleUI_BL
{
    public class Program

    {//
        enum menuOption { add = 1, update, show, showLists, exit };//תפריט ראשי
        enum add { staion = 1, drone, customer, parcel };//תפריט הוספה
        enum update { udrone = 1, ustaione, ucustomer, charge, release, Assign, collection, PackageDeliveryByDrone };//תפריט עדכון
        enum show { station = 1, drone, customer, parcel };//תפריט תצוגה
        enum showLists { station = 1, drones, customers, parcels, unconnectedParcel, availableStationToCharge };//תפריט הצגת הרשימות
        internal static Random rand = new Random(DateTime.Now.Millisecond);//add current time

        static void Main(string[] args)
        {
            List<int> bee;
            IBL.BL bL = new IBL.BL();
            //IDAL.DO idal = new IDAL.DO();
            //DalObject.DalObject d1=new DalObject.DalObject();
            int option;
            bool ex = true;
            Console.WriteLine("Press 1 Add option Press 2 Update option Press 3 View option Press 4 View lists Press 5 Exit");
            int.TryParse(Console.ReadLine(), out option);



            while (ex == true)
            {

                try
                {
                    switch (option)//main menu תפריט בחירות ראשי
                    {
                        case 1://add הוספה
                            {
                                Console.WriteLine("1 add station");
                                Console.WriteLine("2 add Drone");
                                Console.WriteLine("3 add customer");
                                Console.WriteLine("4 add parcel");
                                int.TryParse(Console.ReadLine(), out option);

                                switch (option)
                                {

                                    case 1://add staion הוספת תחנה 
                                        {
                                            Station st = new Station();
                                            Console.WriteLine("Enter the ID of the station");
                                            int id;
                                            int.TryParse(Console.ReadLine(), out id);
                                            st.Id = id;
                                            int name;
                                            Console.WriteLine("Enter the Name of the station");
                                            int.TryParse(Console.ReadLine(), out name);
                                            st.Name = name;
                                            double Longitude;
                                            Console.WriteLine("Enter the  latitude of the station");
                                            double.TryParse(Console.ReadLine(), out Longitude);
                                            Location location = new Location();
                                            location.Longitude = Longitude;
                                            double Latitude;
                                            Console.WriteLine("Enter the longitude of the station");
                                            double.TryParse(Console.ReadLine(), out Latitude);
                                            location.Latitude = Latitude;
                                            st.LocationStation = location;
                                            int ChargeSlots;
                                            Console.WriteLine("Enter the ChargeSlots of the station");
                                            int.TryParse(Console.ReadLine(), out ChargeSlots);
                                            st.ChargeSlotsFree = ChargeSlots;
                                            bL.AddStation(st);//vv
                                            break;
                                        }

                                    case 2://add drone הוספת רחפן
                                        {
                                            Drone dr = new Drone();
                                            Console.WriteLine("Enter the ID of the Drone");
                                            int id;
                                            int.TryParse(Console.ReadLine(), out id);
                                            dr.Id = id;
                                            Console.WriteLine("Enter the Model of the Drone");
                                            string model = Console.ReadLine();

                                            dr.Model = model;
                                            Console.WriteLine("Enter the number of Weight of the Drone: 0-Light, 1-Medium, 2-Heavy");
                                            int ch;
                                            int.TryParse(Console.ReadLine(), out ch);
                                            if (ch == 0)
                                                dr.Weight = IBL.BO.Enums.WeightCategories.Light;
                                            if (ch == 1)
                                                dr.Weight = IBL.BO.Enums.WeightCategories.Medium;
                                            if (ch == 2)
                                                dr.Weight = IBL.BO.Enums.WeightCategories.Heavy;
                                            Console.WriteLine("Enter the number of the station to charge");
                                            //int id;
                                            int.TryParse(Console.ReadLine(), out id);
                                            bL.AddDrone(dr, id);//v
                                            break;
                                        }

                                    case 3://add customer הוספת לקוח
                                        {
                                            Customer cs = new Customer();
                                            Location location = new Location();
                                            Console.WriteLine("Enter your ID ");
                                            int id;
                                            int.TryParse(Console.ReadLine(), out id);
                                            cs.Id = id;
                                            Console.WriteLine("Enter your Name");
                                            string name = Console.ReadLine();
                                            cs.Name = name;
                                            Console.WriteLine("Enter your Pone");
                                            string phone = Console.ReadLine();
                                            //b = stTryParse(Console.ReadLine(), out phone);
                                            cs.Pone = phone;
                                            Console.WriteLine("Enter the Longitude of the home");
                                            double Longitude;
                                            double.TryParse(Console.ReadLine(), out Longitude);
                                            location.Longitude = Longitude;
                                            Console.WriteLine("Enter the latitude of the home");
                                            double Latitude;
                                            double.TryParse(Console.ReadLine(), out Latitude);
                                            location.Latitude = Latitude;
                                            cs.LocationOfCustomer = location;
                                           bL.AddCustomer(cs);//vv
                                            break;
                                        }

                                    case 4://add parcel הוספת משלוח
                                        {
                                            int ch;
                                            Parcel pr = new Parcel();
                                           
                                            Console.WriteLine("Enter the Senderld of the Parcel");//לקוח שולח
                                            int ids;
                                            int.TryParse(Console.ReadLine(), out ids);
                                            //pr.Senderld = id;
                                            pr.CustomerInParcelSender = new CustomerInParcel { Id = ids, Name = bL.GetCustomer(ids).Name };
                                            Console.WriteLine("Enter the Targetld of the Parcel");//לקוח מקבל
                                            int idt;
                                            int.TryParse(Console.ReadLine(), out idt);
                                            pr.CustomerInParcelTarget= new CustomerInParcel { Id = idt, Name = bL.GetCustomer(idt).Name };
                                            Console.WriteLine("Enter the number of Weight of the Parcel: 0-Light, 1-Medium, 2-Heavy");//משקל
                                            int.TryParse(Console.ReadLine(), out ch);
                                            if (ch == 0)
                                                pr.Weight = Enums.WeightCategories.Light;
                                            if (ch == 1)
                                                pr.Weight = Enums.WeightCategories.Medium;
                                            if (ch == 2)
                                                pr.Weight = Enums.WeightCategories.Heavy;
                                            Console.WriteLine("Enter the Priority of the Parcel: 0-simple, 1-quick, 2-emergency");
                                            int.TryParse(Console.ReadLine(), out ch);
                                            if (ch == 0)
                                                pr.Priority = Enums.Priority.simple;
                                            if (ch == 1)
                                                pr.Priority = Enums.Priority.quick;
                                            if (ch == 2)
                                                pr.Priority =Enums.Priority.emergency;

                                            //צריך להוסיף את הפו וכאן קלטנו ת"ז של שולח ומקבל ומשקל חבילה ועדיפות משלוח בביאל מאתחלים זמנים
                                                bL.AddParcel(pr);//vv
                                            break;

                                        }
                                }

                            }

                            break;
                        case 2://update עדכון
                            {
                                Console.WriteLine("Enter your choise:");
                                Console.WriteLine("1 Update Drone");
                                Console.WriteLine("2 Update station");
                                Console.WriteLine("3 Update customet");
                                Console.WriteLine("4 Sending a Drone for charging");//שליחת רחפן לטעינה
                                Console.WriteLine("5 Release Drone from charging");//שחרור חפן מטעינה
                                Console.WriteLine("6 Assign a package to a Drone");//שיוך חבילה לרחפן
                                Console.WriteLine("7 Package collection by Drone");//איסוף חבילה ע"י רחפן
                                Console.WriteLine("8 Package delivery by Drone");//הספקת חבילה ע"י רחפן
                                int.TryParse(Console.ReadLine(), out option);
                                switch (option)
                                {
                                    case 1://  Update Drone
                                        Console.WriteLine("Enter The idDrone and The name of the Drone");
                                        int cod;
                                        int.TryParse(Console.ReadLine(), out cod);
                                        string name = Console.ReadLine();
                                        //לבנות פונקציה מקבלת ת"ז של רחפן ושם ומעדכנת את השם
                                        bL.UpdateDrone(cod, name);//vv
                                        break;
                                    case 2://Update station
                                        Console.WriteLine("Enter The id of station");
                                        int.TryParse(Console.ReadLine(), out cod);
                                        Console.WriteLine("Enter The name of station");
                                        int name1;
                                        int.TryParse(Console.ReadLine(), out name1);
                                        Console.WriteLine("Enter The slote of station");
                                        int slote;
                                        int.TryParse(Console.ReadLine(), out slote);
                                        //מעדכן או כמות טעינות או שם יכול להיות שהכניס נתונים ויכול להיות שלא
                                        bL.UpdateStation(cod, name1, slote);//vv 
                                        break;

                                    case 3://Update customet
                                        Console.WriteLine("Enter The id of customer");
                                        int.TryParse(Console.ReadLine(), out cod);
                                        Console.WriteLine("Enter The new name of customer");
                                        name = Console.ReadLine();
                                        Console.WriteLine("Enter The new phone of customer");
                                        string phone = Console.ReadLine();
                                        //מעדכן או טלפון או שם יכול להיות שהכניס נתונים ויכול להיות שלא
                                        bL.UpdateCustomer(cod, name, phone);//vv
                                        break;

                                    case 4://Sending a Drone for charging
                                        Console.WriteLine("Enter The id drone");
                                        int.TryParse(Console.ReadLine(), out cod);
                                        bL.SendingDroneToCharging(cod);//פונקציה שמקבל ת"ז רחפן ושולחת לטעינה
                                        break;
                                    case 5://Release Drone from charging

                                        Console.WriteLine("Enter The id drone and Charging time");
                                        int.TryParse(Console.ReadLine(), out cod);
                                        TimeSpan time;
                                        TimeSpan.TryParse(Console.ReadLine(), out time);
                                        bL.ReleaseDrone(cod, time);//משחררת מטעינ
                                        break;
                                    case 6://Assign a package to a Drone

                                        Console.WriteLine("Enter The id drone");
                                        int.TryParse(Console.ReadLine(), out cod);
                                        bL.AssignPackageToDrone(cod);//
                                        break;
                                    case 7:// Package collection by Drone

                                        Console.WriteLine("Enter The id drone");
                                        int.TryParse(Console.ReadLine(), out cod);
                                        bL.PickUpPackage(cod);//
                                        break;
                                    case 8://Package delivery by Drone
                                        {
                                            Console.WriteLine("Enter The id drone");
                                            int.TryParse(Console.ReadLine(), out cod);
                                            bL.PackageDeliveryByDrone(cod);
                                            break;
                                        }
                                }
                                break;
                            }

                        case 3://show תצוגה
                            {
                                Console.WriteLine("Enter your choise:");
                                Console.WriteLine("1 show a station");
                                Console.WriteLine("2 show a drone");
                                Console.WriteLine("3 show a customer");
                                Console.WriteLine("4 show a parcel");

                                int.TryParse(Console.ReadLine(), out option);

                                switch (option)
                                {
                                    case 1://station תצוגת תחנת-בסיס 
                                        Console.WriteLine("Enter station Id:");
                                        int.TryParse(Console.ReadLine(), out option);
                                        Console.WriteLine(bL.StationToListToPrint(option));

                                        break;
                                    case 2://drone  תצוגת רחפן
                                        Console.WriteLine("Enter drone Id:");
                                        int.TryParse(Console.ReadLine(), out option);
                                        Console.WriteLine(bL.DroneToLisToPrint(option));
                                        break;
                                    case 3: //customer תצוגת לקוח
                                        Console.WriteLine("Enter customer Id:");
                                        int.TryParse(Console.ReadLine(), out option);
                                        Console.WriteLine(bL.CostumerToListToPrint(option));
                                        break;
                                    case 4://parcel תצוגת חבילה
                                        Console.WriteLine("Enter parcel Id:");
                                        int.TryParse(Console.ReadLine(), out option);
                                         Console.WriteLine(bL.ParcelToListToPrint(option));
                                        
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }


                        case 4://showLists תצוגת רשימות
                            {
                                Console.WriteLine("Enter your choise:");
                                Console.WriteLine("1 show a list of stations");
                                Console.WriteLine("2 show a list of drones");
                                Console.WriteLine("3 show a list of customers");
                                Console.WriteLine("4 show a list of parcels");
                                Console.WriteLine("5 show a list of unconnectedParcel");//חבילות שלא שוייכו
                                Console.WriteLine("6 show a list of availableStationToCharge");//תחנות כם עמדות פנויות

                                int.TryParse(Console.ReadLine(), out option);
                                switch (option)
                                {
                                    case 1:
                                        {//stations  הצגת רשימת תחנות-בסיס  
                                         // Console.WriteLine(d1.GetAllStation().Count());
                                            foreach (IDAL.DO.Station station in bL.accessIDal.GetAllStation())
                                                Console.WriteLine(bL.StationToListToPrint(station.Id).ToString());
                                        }
                                        break;
                                    case 2://drones הצגת רשימת הרחפנים
                                        foreach (IDAL.DO.Drone drone in bL.accessIDal.GetAllDrone())
                                            Console.WriteLine(bL.DroneToLisToPrint(drone.Id).ToString());
                                        break;
                                    case 3://customers הצגת רשימת הלקוחות
                                        foreach (IDAL.DO.Customer customer in bL.accessIDal.GetAllCustomer())
                                            Console.WriteLine(bL.CostumerToListToPrint(customer.Id).ToString());
                                        break;
                                    case 4://parcels  הצגת רשימת החבילות 
                                        foreach (IDAL.DO.Parcel parcel in bL.accessIDal.GetAllParcel())
                                            Console.WriteLine(bL.ParcelToListToPrint(parcel.Id).ToString());
                                        break;
                                    case 5://unconnectedParcel הצגת רשימת חבילות שעוד לא שויכו לרחפן
                                        foreach (IDAL.DO.Parcel parcel in bL.accessIDal.GetAllParcel())
                                            Console.WriteLine(bL.PrintUnconnectedParceslList(parcel.Id).ToString());
                                        break;
                                    case 6:
                                        {//availableStationToCharge הצגת תחנות-בסיס עם עמדות טעינה פנויות
                                            bee = (List<int>)bL.AvailableStationToChargeListt();
                                            for (int i = 0; i < bee.Count; i++)
                                            {
                                                Console.WriteLine(bL.StationToListToPrint(bee.ElementAt(i)).ToString());
                                            }
                                        }
                                        break;
                                    case 7:
                                        foreach (IDAL.DO.Drone drone in bL.accessIDal.PrindDroneChargeList())
                                            Console.WriteLine(bL.GetDrone(drone.Id));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }

                        case 5:
                            {//exit יציאה
                                ex = false;
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }



                if (ex != false)
                {
                    Console.WriteLine("Press 1 Add option Press 2 Update option Press 3 View option Press 4 View lists Press 5 Exit");
                    int.TryParse(Console.ReadLine(), out option);
                }
            }
            
                


        }
    }
        
}
