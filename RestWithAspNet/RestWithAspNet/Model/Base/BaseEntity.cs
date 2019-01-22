using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RestWithAspNet.Model.Base
{
    // [DataContract]
    public class BaseEntity
    {
        public long? Id { get; set; }
    }
}
