using food_heaven_backend.Shared.Domain.Model.Entities;

namespace food_heaven_backend.Security.Domain.Entities;

public class User : BaseEntity
{
    public String Username { get; set; }
    public String PasswordHashed { get; set; }

    public String Role { get; set; }
}