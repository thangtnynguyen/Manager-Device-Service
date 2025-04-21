using System.ComponentModel.DataAnnotations;

namespace Manager_Device_Service.Core.Data
{
    public abstract class EntityBase<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public int? CreatedBy { get; set; }

        public string? CreatedName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public string? UpdatedName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }

}
