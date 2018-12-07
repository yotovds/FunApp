using System;

namespace FunApp.Data.Common
{
    public abstract class BaseModel<T>
    {
        // TODO: check if the class is abstract -> is there some problem with setters
        public int Id { get; set; }
    }
}
