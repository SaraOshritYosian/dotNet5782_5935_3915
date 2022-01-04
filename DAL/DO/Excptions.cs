using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using IDAL;
namespace IDAL.DO
{
    [Serializable]
    public class Excptions : Exception
    {
        public Excptions() : base() { }
        public Excptions(string message) : base(message) { }
        public Excptions(string message, Exception inner) : base(message, inner) { }
        protected Excptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
        //BadDronIdException

    }
    [Serializable]
    class DoesNotExistException : Exception
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
    [Serializable]
    class StationDoesNotExistException : Exception
    {
        public StationDoesNotExistException() : base() { }
        public StationDoesNotExistException(string message) : base(message) { }
        public StationDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected StationDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Station Does Not Exist";
        }
    }
    [Serializable]
    class StationAlreadyExistsException : Exception
    {
        public StationAlreadyExistsException() : base() { }
        public StationAlreadyExistsException(string message) : base(message) { }
        public StationAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected StationAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Station Already Exists";
        }
    }
    [Serializable]
    class ParcelDoesNotExistException : Exception
    {
        public ParcelDoesNotExistException() : base() { }
        public ParcelDoesNotExistException(string message) : base(message) { }
        public ParcelDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected ParcelDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Parcel Does Not Exist";
        }
    }
    [Serializable]
    class ParcelAlreadyExistsException : Exception
    {
        public ParcelAlreadyExistsException() : base() { }
        public ParcelAlreadyExistsException(string message) : base(message) { }
        public ParcelAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected ParcelAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Parcel Already Exists";
        }
    }
    [Serializable]
    class DroneChargDoesNotExistException : Exception
    {
        public DroneChargDoesNotExistException() : base() { }
        public DroneChargDoesNotExistException(string message) : base(message) { }
        public DroneChargDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected DroneChargDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Drone Charge Does Not Exist";
        }
    }
    [Serializable]
    class CsustomerDoesNotExistException : Exception
    {
        public CsustomerDoesNotExistException() : base() { }
        public CsustomerDoesNotExistException(string message) : base(message) { }
        public CsustomerDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected CsustomerDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Customer Does Not Exist";
        }
    }
    [Serializable]
    class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException() : base() { }
        public CustomerAlreadyExistsException(string message) : base(message) { }
        public CustomerAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected CustomerAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Customer Already Exists";
        }
    }
}
