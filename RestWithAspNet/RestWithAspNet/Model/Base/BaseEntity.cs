using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RestWithAspNet.Model.Base
{
   // [DataContract]
    public class BaseEntity
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }

    }
}
