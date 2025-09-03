

namespace ECommercePlatform.Core.Entities
{
    public class SystemSetting : BaseEntity
    {
        public string KeyName { get; set; } = string.Empty;

        public string? Value { get; set; }

        public string DataType { get; set; } = "string";

        public string? Description { get; set; }
    }
}
