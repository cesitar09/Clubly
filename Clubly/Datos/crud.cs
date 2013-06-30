using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Datos
{
    class crud
    {
        //public static Datos.Entities context =new Datos.Entities();

        //public static T SeleccionarId<T>(int id) where T : class
        //{
        //    return context.CreateObjectSet<T>().Single(p => p.Id == id);
        //}

        //public static Exception Modificar<T>(T entity) where T: class
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }

        //    var propertiesFromNewEntity = entity.GetType().GetProperties();

        //    // I've a method that return a entity by Id, and all my entities inherits from
        //    // a AbstractEntity that have a property Id.
        //    var currentEntity = SeleccionarId<T>(entity.Id).FirstOrDefault();

        //    if (currentEntity == null)
        //    {
        //        throw new ObjectNotFoundException("The entity was not found. Verify if the Id was passed properly.");
        //    }

        //    var propertiesFromCurrentEntity = currentEntity.GetType().GetProperties();

        //    for (int i = 0; i < propertiesFromCurrentEntity.Length; i++)
        //    {
        //        propertiesFromCurrentEntity.SetValue(propertiesFromNewEntity.GetValue(i), i);
        //    }
        //}
    }
}
