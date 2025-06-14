using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.PatchHelpers
{
    public static class PatchRequestToEntity
    {
        public static void PatchFrom<TDto,TEntity>(this TEntity entity, TDto dto) 
            where TEntity : BaseEntity
            where TDto : class
        {
            var entityType = entity.GetType();
            var dtoType = dto.GetType();

            var dtoProperties = dtoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //foreach(var property in dtoProperties)
        }
    }
}
