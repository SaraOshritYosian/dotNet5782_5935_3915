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
        //BadDronIdException

    }
    [Serializable] 
    class DoesNotExistException:Exception
    {
        public DoesNotExistException() : base() { }
        public DoesNotExistException(string message) : base(message) { }
        public DoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "The id does not exist";
        }
    }
    [Serializable]
    class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base() { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
        protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Already exist";
        }
    }//
    class DroneDoesNotExistException : Exception
    {
        public DroneDoesNotExistException() : base() { }
        public DroneDoesNotExistException(string message) : base(message) { }
        public DroneDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected DroneDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Drone does not exist";
        }
    }

    [Serializable]
    class DroneAlreadyExistException : Exception
    {
        public DroneAlreadyExistException() : base() { }
        public DroneAlreadyExistException(string message) : base(message) { }
        public DroneAlreadyExistException(string message, Exception inner) : base(message, inner) { }
        protected DroneAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Drone Already Exist";
        }
    }
}
