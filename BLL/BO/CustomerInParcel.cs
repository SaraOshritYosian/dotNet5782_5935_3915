using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class CustomerInParcel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
            //public override string ToString()
            //{

            //    return base.ToString() + string.Format("the id is:{0,6},\t name is:{1,-3})\n", Id, Name);
            //}
        }
   
}
