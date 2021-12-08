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
                                        bL.AddStation(st);//vv
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
                                        bL.AddDrone(dr, id);//v
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
                                        bL.AddCustomer(cs);//vv
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
                                        pr.CustomerInParcelSender.Id = ids;
                                        Console.WriteLine("Enter the Targetld of the Parcel");//לקוח מקבל
                                        int idt;
                                        b = int.TryParse(Console.ReadLine(), out idt);
                                        pr.CustomerInParcelTarget.Id = idt;
                                        Console.WriteLine("Enter the number of Weight of the Parcel: 0-Light, 1-Medium, 2-Heavy");//משקל
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

                                        //צריך להוסיף את הפו וכאן קלטנו ת"ז של שולח ומקבל ומשקל חבילה ועדיפות משלוח בביאל מאתחלים זמנים
                                        // Console.WriteLine("The number of the parcel is: " + id);
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
                                    //לבנות פונקציה מקבלת ת"ז של רחפן ושם ומעדכנת את השם
                                    bL.UpdateDrone(cod, name);//vv
                                    break;
                                case 2://Update station
                                    Console.WriteLine("Enter The id of station");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    int name1;
                                    b = int.TryParse(Console.ReadLine(), out name1);
                                    int slote ;
                                    b = int.TryParse(Console.ReadLine(), out slote);
                                    //מעדכן או כמות טעינות או שם יכול להיות שהכניס נתונים ויכול להיות שלא
                                    bL.UpdateStation(cod, name1, slote);//vv
                                    break;

                                case 3://Update customet
                                    Console.WriteLine("Enter The id of customer");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    name = Console.ReadLine();
                                    string phone=Console.ReadLine();
                                    //מעדכן או טלפון או שם יכול להיות שהכניס נתונים ויכול להיות שלא
                                    bL.UpdateCustomer(cod, name, phone);//vv
                                    break;

                                case 4://Sending a Drone for charging
                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    bL.SendingDroneToCharging(cod);//פונקציה שמקבל ת"ז רחפן ושולחת לטעינה
                                    break;
                                case 5://Release Drone from charging
                                   
                                    Console.WriteLine("Enter The id drone and Charging time");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    TimeSpan time;
                                    b = TimeSpan.TryParse(Console.ReadLine(), out time);
                                    bL.ReleaseDrone(cod, time);//משחררת מטעינה
                                    break;
                                case 6://Assign a package to a Drone

                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    bL.AssignPackageToDrone(cod);//
                                    break;
                                case 7:// Package collection by Drone

                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out cod);
                                    bL.PickUpPackage(cod);//צריך לסדר יש באגים
                                    break;
                                case 8://Package delivery by Drone

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
                                    bL.PrintStationById(option);
                                    break;
                                case 2://drone  תצוגת רחפן
                                    Console.WriteLine("Enter drone Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    bL.PrintDroneById(option);
                                    break;
                                case 3: //customer תצוגת לקוח
                                    Console.WriteLine("Enter customer Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    bL.PrintCustomerById(option);
                                    break;
                                case 4://parcel תצוגת חבילה
                                    Console.WriteLine("Enter parcel Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    bL.PrintParcelById(option);
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
                                    bL.PrintStationList();
                                    break;
                                case 2://drones הצגת רשימת הרחפנים
                                    bL.PrintDroneList();
                                    break;
                                case 3://customers הצגת רשימת הלקוחות
                                    bL.PrintCustomersList();
                                    break;
                                case 4://parcels  הצגת רשימת החבילות 
                                   bL.PrintParcelList();
                                    break;
                                case 5://unconnectedParcel הצגת רשימת חבילות שעוד לא שויכו לרחפן
                                   bL.PrintUnconnectedParceslList();
                                    break;
                                case 6://availableStationToCharge הצגת תחנות-בסיס עם עמדות טעינה פנויות
                                    bL.PrintAvailableStationToChargeList();
                                    break;
                                //case 7://רשימת רחפנים בטעינה
                                //   bL.PrindDroneChargeList();
                                //    break;
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
