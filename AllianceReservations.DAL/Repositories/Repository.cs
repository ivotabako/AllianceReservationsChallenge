using AllianceReservations.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AllianceReservations.DAL.Repositories
{
    [Serializable]
    public class Repository<T> where T : class 
    {
        private Dictionary<string, IEntity> storage = new Dictionary<string, IEntity>();

        public Repository()
        {
            LoadData(GetStorageName());
        }        

        public string CreateAndUpdate(T item)
        {
            var entity = (IEntity)item;

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            if (storage.ContainsKey(entity.Id))
            {
                storage[entity.Id] = entity;
            }
            else
            {
                storage.Add(entity.Id, entity);
            }

            Save(GetStorageName());

            return entity.Id;
        }
       
        public void Delete(string id)
        {
            if (storage.ContainsKey(id))
            {
                storage[id].Id = null;
                storage.Remove(id);
            }

            Save(GetStorageName());
        }

        public T Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default(T);

            return (T)storage[id];
        }        

        private string GetStorageName()
        {            
            Type entityType = typeof(T);

            if (entityType.FullName.Contains(typeof(Person).FullName))
            {
                return "person.bin";
            }
            if (entityType.FullName.Contains(typeof(Business).FullName))
            {
                return "business.bin";
            }
            if (entityType.FullName.Contains(typeof(Address).FullName))
            {
                return "address.bin";
            }

            return string.Empty;
        }

        private void LoadData(string storageName)
        {
            if (!string.IsNullOrEmpty(storageName))
            {
                try
                {
                    storage = Deserialize<Dictionary<string, IEntity>>(File.Open(storageName, FileMode.Open));
                }
                catch (FileNotFoundException)
                {
                    Serialize(storage, File.Open(storageName, FileMode.Create, FileAccess.Write));
                }
                catch (Exception)
                {
                }

            }

        }

        private void Save(string storageName)
        {
            Serialize(storage, File.Open(storageName, FileMode.Create, FileAccess.Write));
        }
       
        public static void Serialize<Object>(Object dictionary, Stream stream)
        {
            try // try to serialize the collection to a file
            {
                using (stream)
                {                    
                    BinaryFormatter bin = new BinaryFormatter();                    
                    bin.Serialize(stream, dictionary);
                }
            }
            catch (IOException)
            {
            }
        }
        
        public static Object Deserialize<Object>(Stream stream) where Object : new()
        {
            Object ret = CreateInstance<Object>();
            try
            {
                using (stream)
                {                    
                    BinaryFormatter bin = new BinaryFormatter();                    
                    ret = (Object)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }
            return ret;
        }
        
        public static Object CreateInstance<Object>() where Object : new()
        {
            return (Object)Activator.CreateInstance(typeof(Object));
        }
    }
}
