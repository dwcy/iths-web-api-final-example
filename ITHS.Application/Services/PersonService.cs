﻿using ITHS.Application.ViewModels.Users;
using ITHS.Domain.Entities;
using ITHS.Webapi.Persistance;
using Microsoft.EntityFrameworkCore;

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
        private readonly ITHSDatabaseContext _context;
        public PersonService()
        {
        }

        public PersonResponse GetPerson(Guid id)
        {
            using (var context = new ITHSDatabaseContext())
            {
                var person = context
                    .Persons
                    .Where(p => p.Id == id)
                    .FirstOrDefault();

                return new PersonResponse(person);
            }
        }

        public List<PersonResponse> FindPersonsByFirstName(string firstName)
        {
            using (var context = new ITHSDatabaseContext())
            {
                var persons = context
                    .Persons
                    .Where(person => person.FirstName.Contains(firstName))
                    .Select(person => new PersonResponse(person))
                    .ToList();

                return persons;
            }
        }

        public async Task<List<PersonResponse>> FindPersonsByFirstNameAsync(string firstName)
        {
            using (var context = new ITHSDatabaseContext())
            {
                var persons = await context
                    .Persons
                    .Where(person => person.FirstName.Contains(firstName))
                    .Select(person => new PersonResponse(person))
                    .ToListAsync();

                return persons;
            }
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

    }
}
