using System;
using IBL.BO;


namespace ConsoleUI_BL
{
    public class Program

    {
        enum menuOption { add = 1, update, show, showLists, exit };//תפריט ראשי
        enum add { staion = 1, drone, customer, parcel };//תפריט הוספה
        enum update { connect = 1, collect, supply, charge, release };//תפריט עדכון
        enum show { station = 1, drone, customer, parcel };//תפריט תצוגה
        enum showLists { station = 1, drones, customers, parcels, unconnectedParcel, availableStationToCharge };//תפריט הצגת הרשימות
        internal static Random rand = new Random(DateTime.Now.Millisecond);//add current time

        static void Main(string[] args)
        {

            IBL.BL bL = new IBL.BL();
            
            int option;
            // int a;
            bool ex = true;
            Console.WriteLine("Press 1 Add option Press 2 Update option Press 3 View option Press 4 View lists Press 5 Exit");
            bool b = int.TryParse(Console.ReadLine(), out option);
            while (ex == true)
            {
                switch (option)//main menu תפריט בחירות ראשי
                {
                    case 1://add הוספה
                        {
                            Console.WriteLine("1 add station");
                            Console.WriteLine("2 add Drone");
                            Console.WriteLine("3 add customer");
                            Console.WriteLine("4 add parcel");
                            b = int.TryParse(Console.ReadLine(), out option);

                            switch (option)
                            {

                                case 1://add staion הוספת תחנה 
                                    {
                                        Station st = new Station();
                                        Console.WriteLine("Enter the ID of the station");
                                        Console.WriteLine("Enter the Name of the station");
                                        Console.WriteLine("Enter the  latitude of the station");
                                        Console.WriteLine("Enter the longitude of the station");
                                        Console.WriteLine("Enter the ChargeSlots of the station");
                                        int id;
                                        b = int.TryParse(Console.ReadLine(), out id);
                                        st.Id = id;
                                        int name;
                                        b = int.TryParse(Console.ReadLine(), out name);
                                        st.Name = name;
                                        double Longitude;
                                        b = double.TryParse(Console.ReadLine(), out Longitude);
                                        Location location = new Location();
                                        location.Longitude = Longitude;
                                        double Latitude;
                                        b = double.TryParse(Console.ReadLine(), out Latitude);
                                        location.Latitude = Latitude;
                                        st.LocationStation = location;
                                        int ChargeSlots;
                                        b = int.TryParse(Console.ReadLine(), out ChargeSlots);
                                        st.ChargeSlotsFree = ChargeSlots;
                                        bL.AddStation(st);//יש
                                        break;
                                    }

                                case 2://add drone הוספת רחפן
                                    {
                                        Drone dr = new Drone();
                                        Console.WriteLine("Enter the ID of the Drone");
                                        int id;
                                        b = int.TryParse(Console.ReadLine(), out id);
                                        dr.Id = id;
                                        Console.WriteLine("Enter the Model of the Drone");
                                        string model = Console.ReadLine();

                                        dr.Model = model;
                                        Console.WriteLine("Enter the number of Weight of the Drone: 0-Light, 1-Medium, 2-Heavy");
                                        int ch;
                                        b = int.TryParse(Console.ReadLine(), out ch);
                                        if (ch == 0)
                                            dr.Weight = IBL.BO.Enums.WeightCategories.Light;
                                        if (ch == 1)
                                            dr.Weight = IBL.BO.Enums.WeightCategories.Medium;
                                        if (ch == 2)
                                            dr.Weight = IBL.BO.Enums.WeightCategories.Heavy;
                                        Console.WriteLine("Enter the number of the station to charge");
                                        //int id;
                                        b = int.TryParse(Console.ReadLine(), out id);
                                        bL.AddDrone(dr, id);//צריך
                                        break;

                                    }

                                case 3://add customer הוספת לקוח
                                    {
                                        Customer cs = new Customer();
                                        Location location = new Location();
                                        Console.WriteLine("Enter your ID ");
                                        int id;
                                        b = int.TryParse(Console.ReadLine(), out id);
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
                                        b = double.TryParse(Console.ReadLine(), out Longitude);
                                        location.Longitude = Longitude;
                                        Console.WriteLine("Enter the latitude of the home");
                                        double Latitude;
                                        b = double.TryParse(Console.ReadLine(), out Latitude);
                                        location.Latitude = Latitude;
                                        cs.LocationOfCustomer = location;
                                        bL.AddCustomer(cs);//יש/
                                        break;
                                    }

                                case 4://add parcel הוספת משלוח
                                    {
                                        int ch;
                                        Parcel pr = new Parcel(); 
                                        Console.WriteLine("Enter the Senderld of the Parcel");//לקוח שולח
                                        int ids;
                                        b = int.TryParse(Console.ReadLine(), out ids);
                                        //pr.Senderld = id;
                                        Console.WriteLine("Enter the Targetld of the Parcel");//לקוח מקבל
                                        int idt;
                                        b = int.TryParse(Console.ReadLine(), out idt);       
                                        Console.WriteLine("Enter the number of Weight of the Parcel: 0-Light, 1-Medium, 2-Heavy");
                                        b = int.TryParse(Console.ReadLine(), out ch);
                                        if (ch == 0)
                                            pr.Weight = IBL.BO.Enums.WeightCategories.Light;
                                        if (ch == 1)
                                            pr.Weight = IBL.BO.Enums.WeightCategories.Medium;
                                        if (ch == 2)
                                            pr.Weight = IBL.BO.Enums.WeightCategories.Heavy;
                                        Console.WriteLine("Enter the Priority of the Parcel: 0-simple, 1-quick, 2-emergency");
                                        b = int.TryParse(Console.ReadLine(), out ch);
                                        if (ch == 0)
                                            pr.Priority = IBL.BO.Enums.Priority.simple;
                                        if (ch == 1)
                                            pr.Priority = IBL.BO.Enums.Priority.quick;
                                        if (ch == 2)
                                            pr.Priority = IBL.BO.Enums.Priority.emergency;

                                        // pr.PichedUp = DateTime.Now;//איזה לעשות כאן
                                        // pr.Delivered = DateTime.Now;//איזה שעה עושים כאן?
                                        // id = d4.AddParcel(pr);
                                        // Console.WriteLine("The number of the parcel is: " + id);
                                        bL.addparcel(pr);//צריך להוסיף את הפו וכאן קלטנו ת"ז של שולח ומקבל ומשקל חבילה ועדיפות משלוח בביאל מאתחלים זמנים
                                        break;

                                    }
                            }

                        }

                        break;
                    case 2://update עדכון
                        {
                            Console.WriteLine("Enter your choise:");
                            Console.WriteLine("1 Update Drone");
                            Console.WriteLine("2 Update customet");
                            Console.WriteLine("3 Sending a Drone for charging");//שליחת רחפן לטעינה
                            Console.WriteLine("4 Release Drone from charging");//שחרור חפן מטעינה
                            Console.WriteLine("5 Assign a package to a Drone");//שיוך חבילה לרחפן
                            Console.WriteLine("6 Package collection by Drone");//איסוף חבילה ע"י רחפן
                            Console.WriteLine("7 Package delivery by Drone");//הספקת חבילה ע"י רחפן
                            b = int.TryParse(Console.ReadLine(), out option);
                            switch (option)
                            {
                                case 1://  Update Drone
                                    Console.WriteLine("Enter The idDrone and The name of the Drone");
                                    int cod;
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    string name = Console.ReadLine();
                                    bL.UpdateDrone(cod, name);//לבנות פונקציה מקבלת ת"ז של רחפן ושם ומעדכנת את השם
                                    break;
                                case 2://Update customet
                                    Console.WriteLine("Enter The id of customer");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    name = Console.ReadLine();
                                    int phone;
                                    b = int.TryParse(Console.ReadLine(), out phone);
                                    bL.UpdateCustomet(cod, phone, name);//מעדכן או טלפון או שם יכול להיות שהכניס נתונים ויכול להיות שלא
                                    break;
                                case 3://Sending a Drone for charging
                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    bL.SendingDroneForCharging(cod);//פונקציה שמקבל ת"ז רחפן ושולחת לטעינה
                                    break;
                                case 4://Release Drone from charging
                                   
                                    Console.WriteLine("Enter The id drone and Charging time");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    int time;
                                    b = int.TryParse(Console.ReadLine(), out time);
                                    bL.ReleaseDronefromcharging(cod, time);//משחררת מטעינה
                                    break;
                                case 5://Assign a package to a Drone

                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    bL.AssignPackageToDrone(cod);//
                                    break;
                                case 6:// Package collection by Drone

                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    bL.PackageCollectionByDrone(cod);
                                    break;
                                case 7://Package delivery by Drone

                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    bL.PackageDeliveryByDrone(cod);
                                    break;

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

                            b = int.TryParse(Console.ReadLine(), out option);

                            switch (option)
                            {
                                case 1://station תצוגת תחנת-בסיס 
                                    Console.WriteLine("Enter station Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    bL.PrintBaseStation(option);
                                    break;
                                case 2://drone  תצוגת רחפן
                                    Console.WriteLine("Enter drone Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    bL.PrintDrone(option);
                                    break;
                                case 3: //customer תצוגת לקוח
                                    Console.WriteLine("Enter customer Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    bL.PrintCustomer(option);
                                    break;
                                case 4://parcel תצוגת חבילה
                                    Console.WriteLine("Enter parcel Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    bL.PrintParcel(option);
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
                            
                            b = int.TryParse(Console.ReadLine(), out option);
                            switch (option)
                            {
                                case 1://stations  הצגת רשימת תחנות-בסיס 
                                    bL.PrindBaseStationList();
                                    break;
                                case 2://drones הצגת רשימת הרחפנים
                                    bL.PrintDronesList();
                                    break;
                                case 3://customers הצגת רשימת הלקוחות
                                    bL.PrintCustomersList();
                                    break;
                                case 4://parcels  הצגת רשימת החבילות 
                                   bL.PrintParcelsList();
                                    break;
                                case 5://unconnectedParcel הצגת רשימת חבילות שעוד לא שויכו לרחפן
                                   b.PrintUnconnectedParceslList();
                                    break;
                                case 6://availableStationToCharge הצגת תחנות-בסיס עם עמדות טעינה פנויות
                                    bL.PrintAvailableStationToChargeList();
                                    break;
                                //case 7://רשימת רחפנים בטעינה
                                //   bL.PrindDroneChargeList();
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
                if (ex != false)
                {
                    Console.WriteLine("Press 1 Add option Press 2 Update option Press 3 View option Press 4 View lists Press 5 Exit");
                    b = int.TryParse(Console.ReadLine(), out option);
                }

            }

        }
    }
        
}
