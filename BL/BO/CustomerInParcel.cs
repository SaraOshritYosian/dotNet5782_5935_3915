using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerInParcel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Pone { get; set; }
        public override string ToString()
        {

            return base.ToString() + string.Format("the id is:{0,6},\t name is:{1,-3})\n", Id, Name);
        }
    }
}
