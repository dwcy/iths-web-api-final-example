using ITHS.Domain.Constants;

namespace ITHS.Application.ViewModels.Users
{
    public class PersonCreateRequest : PersonBase //rest of the person properties are inherited
    {
        public SchoolRoles Role { get; set; }
    }
}
