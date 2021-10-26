using System;
using IDAL.DO

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
            int option;

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
                            break;
                        case 2://add drone הוספת רחפן

                            break;
                        case 3://add customer הוספת לקוח
                            break;
                        case 4://add parcel הוספת משלוח
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
