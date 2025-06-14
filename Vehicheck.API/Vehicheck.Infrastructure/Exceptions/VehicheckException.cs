using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Infrastructure.Exceptions
{
    public abstract class VehicheckException : Exception
    {
        protected VehicheckException(string message) : base(message) { }
        protected VehicheckException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class EntityNotFoundException : VehicheckException
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public EntityNotFoundException(string entityName, object entityId)
            : base($"{entityName} with id '{entityId}' was not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
