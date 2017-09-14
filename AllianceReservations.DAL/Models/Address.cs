using AllianceReservations.DAL.Repositories;
using System;

namespace AllianceReservations.DAL.Models
{
    [Serializable]
    public class Address : EntityBase<Address>, IEntity
    {
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
       
        public Address(string street, string city, string state, string zipCode)
        {
            City = city;
            Street = street;
            State = state;
            ZipCode = zipCode;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Address);
        }

        public override int GetHashCode() 
        {
            int hash = 13;
            hash = (hash * 7) + Street.GetHashCode();
            hash = (hash * 7) + City.GetHashCode();
            hash = (hash * 7) + State.GetHashCode();
            hash = (hash * 7) + ZipCode.GetHashCode();

            return hash;
        }

        public bool Equals(Address other)
        {
            if (other == null)
                return false;

            return 
                (
                    object.ReferenceEquals(this.Street, other.Street) ||
                    this.Street != null && this.Street.Equals(other.Street)
                ) &&
                (
                    object.ReferenceEquals(this.City, other.City) ||
                    this.City != null && this.City.Equals(other.City)
                )
                &&
                (
                    object.ReferenceEquals(this.State, other.State) ||
                    this.State != null && this.State.Equals(other.State)
                )
                &&
                (
                    object.ReferenceEquals(this.ZipCode, other.ZipCode) ||
                    this.ZipCode != null && this.ZipCode.Equals(other.ZipCode)
                );
        }
    }
}
