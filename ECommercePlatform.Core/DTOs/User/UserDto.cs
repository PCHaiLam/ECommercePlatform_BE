namespace ECommercePlatform.Core.DTOs.User
{
    /// <summary>
    /// User information DTO
    /// </summary>
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public bool EmailVerified { get; set; }
    }
}
