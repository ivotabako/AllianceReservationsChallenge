using AllianceReservations.DAL.Repositories;
using System;

namespace AllianceReservations.DAL.Models
{
    [Serializable]
    public abstract class EntityBase<T> : IEntityBase<T>
        where T : IEntity
    {
        public string Id { get; set; }

        private Repository<EntityBase<T>> _repositry = new Repository<EntityBase<T>>();

        public void Save()
        {     
            Id = _repositry.CreateAndUpdate(this);
        }

        public void Delete()
        {
            if (!string.IsNullOrEmpty(Id))
                _repositry.Delete(Id);
        }

        public static T Find(string id)
        {
            var repository = new Repository<IEntityBase<T>>();
            return (T)repository.Get(id);
        }
    }
}
