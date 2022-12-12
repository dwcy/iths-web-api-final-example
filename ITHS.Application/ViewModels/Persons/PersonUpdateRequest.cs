namespace ITHS.Application.ViewModels.Users
{
    public class PersonUpdateRequest : PersonBase //rest of the person properties are inherited
    {
        public Guid PersonId { get; set; }
    }
}
