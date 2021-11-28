using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL
{
  public partial class BL : IBL
    {
        public IDAL.IDal accessIDal;
        public List<Drone> BLDrones;
        public static double Free;
        public static double LightWeight;
        public static double MediumWeight;
        public static double HeavyWeight;
        public static double LoadingPrecents;
        public Random rand = new Random(DateTime.Now.Millisecond);
        public BL()
        {
            accessIDal = new DalObject();
                double[] arr = new accessIDal.RequestPowerConsuptionByDrone();//מה זו הפונקציה  הזו

        }

    }
}
