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
            DalObject.DalObject d6 = new DalObject.DalObject();
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
                                        Console.WriteLine("Enter the number of latitude of the station");
                                        Console.WriteLine("Enter the longitude of the station");
                                       Console.WriteLine("Enter the ChargeSlots of the station");
                                        int id;
                                        b = int.TryParse(Console.ReadLine(), out id);
                                        st.Id = id;
                                        int name;
                                        b = int.TryParse(Console.ReadLine(), out name);
                                        st.Name = name;
                                        double Latitude;
                                        b = double.TryParse(Console.ReadLine(), out Latitude);
                                        st.Latitude = Latitude;
                                        double Longitude;
                                        b = double.TryParse(Console.ReadLine(), out Longitude);
                                        st.Longitude = Longitude;
                                        int ChargeSlots;
                                        b = int.TryParse(Console.ReadLine(), out ChargeSlots);
                                        st.ChargeSlots = ChargeSlots;
                                        d1.AddStation(st);//v
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
                                            dr.Weight = WeightCategories.Light;
                                        if (ch == 1)
                                            dr.Weight = WeightCategories.Medium;
                                        if (ch == 2)
                                            dr.Weight = WeightCategories.Heavy;
                                        d2.AddDrone(dr);//v
                                        break;

                                    }

                                case 3://add customer הוספת לקוח
                                    {
                                        Customer cs = new Customer();
                                        Console.WriteLine("Enter your ID ");
                                        int id;
                                        b = int.TryParse(Console.ReadLine(), out id);
                                        cs.Id =id;
                                        Console.WriteLine("Enter your Name");
                                        string name = Console.ReadLine();
                                        //b = TryParse(Console.ReadLine(), out name);//טעות 
                                        cs.Name = name;
                                        Console.WriteLine("Enter your Pone");
                                        string phone = Console.ReadLine();
                                        //b = stTryParse(Console.ReadLine(), out phone);
                                        cs.Pone =phone;
                                        Console.WriteLine("Enter the Longitude of the home");
                                        double Longitude;
                                        b = double.TryParse(Console.ReadLine(), out Longitude);
                                        cs.Longitude = Longitude;
                                        Console.WriteLine("Enter the latitude of the home");
                                        double Latitude;
                                        b = double.TryParse(Console.ReadLine(), out Latitude);
                                        cs.Lattitude = Latitude;
                                        d3.AddCustomer(cs);//v
                                        break;
                                    }

                                case 4://add parcel הוספת משלוח
                                    {
                                        int ch;
                                        Parcel pr = new Parcel();
                                        Console.WriteLine("Enter the id of the Parcel");
                                        int id;
                                        b = int.TryParse(Console.ReadLine(), out id);
                                        pr.Senderld = id;
                                        Console.WriteLine("Enter the Senderld of the Parcel");
                                        b = int.TryParse(Console.ReadLine(), out id);
                                        pr.Senderld = id;
                                        Console.WriteLine("Enter the Targetld of the Parcel");
                                        int target;
                                        b = int.TryParse(Console.ReadLine(), out target);
                                        pr.Targetld = target;
                                        Console.WriteLine("Enter the number of Weight of the Parcel: 0-Light, 1-Medium, 2-Heavy");
                                        b = int.TryParse(Console.ReadLine(), out ch);
                                        if (ch == 0)
                                            pr.Weight = WeightCategories.Light;
                                        if (ch == 1)
                                            pr.Weight = WeightCategories.Medium;
                                        if (ch == 2)
                                            pr.Weight = WeightCategories.Heavy;
                                        Console.WriteLine("Enter the Priority of the Parcel: 0-simple, 1-quick, 2-emergency");
                                        b = int.TryParse(Console.ReadLine(), out ch);
                                        if (ch == 0)
                                            pr.Priority = Priority.simple;
                                        if (ch == 1)
                                            pr.Priority = Priority.quick;
                                        if (ch == 2)
                                            pr.Priority = Priority.emergency;

                                        pr.Droneld = 0;
                                        pr.Requested = DateTime.Now;
                                        //DateTime.AddMinutes(double(rand));
                                        pr.PichedUp = DateTime.Now;//איזה לעשות כאן
                                        pr.Delivered = DateTime.Now;//איזה שעה עושים כאן?
                                        d4.AddParcel(pr);
                                       // Console.WriteLine("The number of the parcel is: " + id);
                                        break;

                                    }  
                            }
                            
                        }

                        break;
                    case 2://update עדכון
                        {
                            Console.WriteLine("Enter your choise:");
                            Console.WriteLine("1 Assign a package to a Drone");
                            Console.WriteLine("2 Package collection by Drone");
                            Console.WriteLine("3 Delivery of a package to the customer");
                            Console.WriteLine("4 Drone a Drone for charging");
                            Console.WriteLine("5 Release Drone from charging");
                            b = int.TryParse(Console.ReadLine(), out option);
                            switch (option)
                            {
                                case 1://connect שיוך חבילה לרחפן
                                    Console.WriteLine("Enter The id packago and id of the drone");
                                    int codp,codd;
                                    b = int.TryParse(Console.ReadLine(), out codp);
                                    b = int.TryParse(Console.ReadLine(), out codd);
                                    bool a =d6.AssignPackageToDrone(codp, codd);



                                    break;
                                case 2://collect  איסוף חבילה ע"י רחפ ן
                                    Console.WriteLine("Enter The id packago");
                                    b = int.TryParse(Console.ReadLine(), out codp);
                                    d6.PackageCollectionByDrone(codp);
                                    break;
                                case 3://suplly אספקת חבילה ל-לקוח
                                    Console.WriteLine("Enter The id packago");
                                    b = int.TryParse(Console.ReadLine(), out codp);
                                   d6.DeliveryOfPackageToTheCustomer(codp);
                                    break;
                                case 4://charge  שליחת רחפן לטעינה בתחנת -בסיס
                                    DroneCharge drc = new DroneCharge();
                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out codp);
                                    drc.Droneld =codp;
                                    d1.PrintAvailableStationToChargeList();//print charge station 
                                    Console.WriteLine("Enter The id station");
                                    b = int.TryParse(Console.ReadLine(), out codp);
                                    drc.Stationld =codp;
                                    d5.AddDroneCharge(drc);
                                    break;
                                case 5://שיחרור רחפן מטעינה
                                    
                                    Console.WriteLine("Enter The id drone");
                                    b = int.TryParse(Console.ReadLine(), out codp);
                                    d6.ReleaseDroneFromCharging(codp);
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
                                    d1.PrintBaseStation(option);
                                 break;
                                case 2://drone  תצוגת רחפן
                                    Console.WriteLine("Enter drone Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                   d1.PrintDrone(option);
                                    break;
                                case 3: //customer תצוגת לקוח
                                    Console.WriteLine("Enter customer Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                   d1.PrintCustomer(option);
                                    break;
                                case 4://parcel תצוגת חבילה
                                    Console.WriteLine("Enter parcel Id:");
                                    b = int.TryParse(Console.ReadLine(), out option);
                                    d1.PrintParcel(option);
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
                            Console.WriteLine("5 show a list of unconnectedParcel");
                            Console.WriteLine("6 show a list of availableStationToCharge");
                            Console.WriteLine("7 show a list of drones in charging");
                            b = int.TryParse(Console.ReadLine(), out option);
                            switch (option)
                            {
                                case 1://stations  הצגת רשימת תחנות-בסיס 
                                    d1.PrindBaseStationList();
                                    break;
                                case 2://drones הצגת רשימת הרחפנים
                                   d1.PrintDronesList();
                                    break;
                                case 3://customers הצגת רשימת הלקוחות
                                   d1.PrintCustomersList();
                                    break;
                                case 4://parcels  הצגת רשימת החבילות 
                                    d1.PrintParcelsList();
                                    break;
                                case 5://unconnectedParcel הצגת רשימת חבילות שעוד לא שויכו לרחפן
                                    d1.PrintUnconnectedParceslList();
                                    break;
                                case 6://availableStationToCharge הצגת תחנות-בסיס עם עמדות טעינה פנויות
                                   d1.PrintAvailableStationToChargeList();
                                    break;
                                case 7://רשימת רחפנים בטעינה
                                   d1.PrindDroneChargeList();
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
