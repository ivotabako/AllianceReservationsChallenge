namespace AllianceReservations.DAL.Models
{
    public interface IEntityBase<T> where T : IEntity
    {
        string Id { get; set; }
        void Save();
        void Delete();
    }
}