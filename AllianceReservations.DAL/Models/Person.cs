using System;
using AllianceReservations.DAL.Repositories;

namespace AllianceReservations.DAL.Models
{
    [Serializable]
    public class Person : EntityBase<Person>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }               

        public Person(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Person);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + FirstName.GetHashCode();
            hash = (hash * 7) + LastName.GetHashCode();
            hash = (hash * 7) + Address.GetHashCode();

            return hash;
        }

        public bool Equals(Person other)
        {
            if (other == null)
                return false;

            return
                (
                    object.ReferenceEquals(this.FirstName, other.FirstName) ||
                    this.FirstName != null && this.FirstName.Equals(other.FirstName)
                ) &&
                (
                    object.ReferenceEquals(this.LastName, other.LastName) ||
                    this.LastName != null && this.LastName.Equals(other.LastName)
                ) &&
                (
                    object.ReferenceEquals(this.Address, other.Address) ||
                    this.Address != null && this.Address.Equals(other.Address)
                );
        }
    }
}
