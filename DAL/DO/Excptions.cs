using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;



namespace DAL.DO
{
    [Serializable]
    class Excptions:Exception
    {
        public Excptions() : base() {}
        public Excptions(string message) : base(message) { }
        public Excptions(string message, Exception inner) : base(message, inner) { }
        protected Excptions(SerializationInfo info, StreamingContext context) : base(info, context) {}


    }
}
