using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static List<Employee> Employees => new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                //Role = Roles.FirstOrDefault(x => x.Name == "Admin"),
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                //Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
                AppliedPromocodesCount = 10
            },
        };

        public static List<Role> Roles => new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Name = "Admin",
                Description = "Администратор",
                EmployeeId=Employees.FirstOrDefault(x => x.FirstName == "Иван").Id
            },
            new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер",
                EmployeeId=Employees.FirstOrDefault(x => x.FirstName == "Петр").Id
                //TODO добавил EmployeeId
            }
        };

        public static List<Preference> Preferences => new List<Preference>()
        {
            new Preference()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Preference()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Preference()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        public static List<Customer> Customers
        {
            get
            {
                var customerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0");
                var customerPreferences = new List<CustomerPreference>()
                {
                    new CustomerPreference
                    {
                        CustomerId=customerId,
                        PreferenceId=Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                    },
                    new CustomerPreference
                    {
                        CustomerId=customerId,
                        PreferenceId=Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                    }
                };
                var customers = new List<Customer>()
                {
                    new Customer()
                    {
                        Id = customerId,
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров",
                        //CustomerPreference=customerPreferences,
                        //TODO: Добавить предзаполненный список предпочтений Done
                    }
                };
                return customers;
            }
        }

        public static List<PromoCode> PromoCodes
        {
            get
            {
                var promocodes = new List<PromoCode>()
                {
                    new PromoCode()
                    {
                        Id = Guid.Parse("2d5c0b24-0f61-4ae3-ad2a-e0ded5153d09"),
                        ServiceInfo = "Сервисная информация",
                        BeginDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(1),
                        PartnerName="Рога и Копыта",
                        CustomerId = Customers.FirstOrDefault().Id,
                        //TODO: 
                    },
                    new PromoCode()
                    {
                        Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                        ServiceInfo = "Сервисная информация",
                        BeginDate = DateTime.Now.AddDays(-1),
                        EndDate = DateTime.Now.AddDays(2),
                        PartnerName="Домик в Деревне",
                        CustomerId = Customers.FirstOrDefault().Id,
                        //TODO: 
                    }
                };
                return promocodes;
            }
        }
    }
}