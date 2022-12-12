using ITHS.Application.ViewModels.Users;
using ITHS.Domain.Entities;
using ITHS.Domain.Interfaces.Repositories;
using ITHS.Webapi.Persistance;

namespace ITHS.Application.Services
{
    public interface IPersonService
    {
        PersonResponse GetPerson(Guid id);
        List<PersonResponse> FindPersonsByFirstName(string firstName);
        void AddPerson(PersonCreateRequest person);
        Task<List<PersonResponse>> FindPersonsByFirstNameAsync(string firstName);
    }

    public class PersonService : IPersonService
    {
        private readonly IPersonsRepository _personRepository;
        
        public PersonService(IPersonsRepository personRepository)
        {
            _personRepository = personRepository;
        }   

        public async Task<List<PersonResponse>> FindPersonsByFirstNameAsync(string firstName)
        {          
            var persons = await _personRepository.FindPersonsByFirstName(firstName);
            
            var personResponses = new List<PersonResponse>();
            foreach (var person in persons)
            {
                personResponses.Add(new PersonResponse(person));
            }

            return personResponses;
        }

  
        public void AddPerson(PersonCreateRequest person)
        {
            using (var context = new ITHSDatabaseContext())
            {
                var role = context.Roles.Where(role => role.RoleName == person.Role).FirstOrDefault();

                context.Persons.Add(new Person()
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = person.EmailAddress,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    PhoneNumber = person.PhoneNumber,
                    RoleId = role.Id,
                    Role = role
                });

                context.SaveChanges();
            }
        }

        public PersonResponse GetPerson(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<PersonResponse> FindPersonsByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }
    }
}
