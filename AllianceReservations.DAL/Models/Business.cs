using AllianceReservations.DAL.Repositories;
using System;

namespace AllianceReservations.DAL.Models
{
    [Serializable]
    public class Business : EntityBase<Business>, IEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }        

        public Business(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Business);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Name.GetHashCode();
            hash = (hash * 7) + Address.GetHashCode();
           
            return hash;
        }

        public bool Equals(Business other)
        {
            if (other == null)
                return false;

            return
                (
                    object.ReferenceEquals(this.Name, other.Name) ||
                    this.Name != null && this.Name.Equals(other.Name)
                ) &&
                (
                    object.ReferenceEquals(this.Address, other.Address) ||
                    this.Address != null && this.Address.Equals(other.Address)
                );
        }
    }
}
