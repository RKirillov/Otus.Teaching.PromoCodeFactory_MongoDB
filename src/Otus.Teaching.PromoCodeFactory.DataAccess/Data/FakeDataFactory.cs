using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions.Equivalency;
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
                RoleId = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),// только для HasData
                //Role = Roles.FirstOrDefault(x => x.Name == "Admin"),// только для AddRange
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                RoleId = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),// только для HasData
                //Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),// только для AddRange
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
                //Employee=Employees.FirstOrDefault(x => x.FirstName == "Иван").Id
            },
            new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер",
                //EmployeeId=Employees.FirstOrDefault(x => x.FirstName == "Петр").Id
                //TODO добавил EmployeeId
            }
        };

        public static List<Preference> Preferences
        {
            get
            {
            var preferences = new List<Preference>()
            {
                    new Preference()
                {
                        Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                        //PromoCodeId = Guid.Parse("2d5c0b24-0f61-4ae3-ad2a-e0ded5153d09"),
                        Name = "Театр",
                        //CustomerPreference=new List <CustomerPreference>(),
                    },
                    new Preference()
                {
                        Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                        //PromoCodeId = Guid.Parse("33867eef-321b-4b88-a4e6-e57f77e3e57a"),
                        Name = "Семья",
                        //CustomerPreference=new List<CustomerPreference>(),
                    },
                    new Preference()
                {
                        Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                        //PromoCodeId = Guid.Parse("fd49d9a9-609f-4376-bfb4-14b157aae3a9"),
                        Name = "Дети",
                        //CustomerPreference=new List < CustomerPreference >(),
                    }
                    };
                return preferences;
            }
        }

        public static List<Customer> Customers
        {
            get
            {
                var customerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0");
                var customers = new List<Customer>()
                {
                    new Customer()
                    {
                        Id = customerId,
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров",
/*                        Preferences = new List<CustomerPreference>() 
                        {
                            new CustomerPreference()
                            {
                                CustomerId = customerId,
                                PreferenceId = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84")
                            },
                            new CustomerPreference()
                            {
                                CustomerId = customerId,
                                PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c")
                            }
                        }*/
                    }
                };
                return customers;
            }
        }
        //TODO чето тут не то
        public static List<CustomerPreference> CustomerPreferences => new List<CustomerPreference>()
        {

                    new CustomerPreference
                    {
                        CustomerId=Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                    },
                    new CustomerPreference
                    {
                        CustomerId=Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),//Customers.FirstOrDefault().Id
                        PreferenceId = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                    },
                    new CustomerPreference
                    {
                        CustomerId=Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),//Customers.FirstOrDefault().Id
                        PreferenceId = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                    }

        };

        public static List<PromoCode> PromoCodes => new List<PromoCode>()
        {
                    new PromoCode()
                    {
                        Id = Guid.Parse("2d5c0b24-0f61-4ae3-ad2a-e0ded5153d09"),
                        Code1 = "OSEN2024",
                        ServiceInfo = "Сервисная информация1",
                        BeginDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(1),
                        PartnerName="Рога и Копыта",
                        CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PartnerManagerId=Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                        PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                        //TODO: 
                    },
                    new PromoCode()
                    {
                        Id = Guid.Parse("33867eef-321b-4b88-a4e6-e57f77e3e57a"),
                        Code1 = "LETO2024",
                        ServiceInfo = "Сервисная информация2",
                        BeginDate = DateTime.Now.AddDays(-1),
                        EndDate = DateTime.Now.AddDays(2),
                        PartnerName="Домик в Деревне",
                        CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PartnerManagerId=Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                        PreferenceId = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                        //TODO: 
                    },
                    new PromoCode()
                    {
                        Id = Guid.Parse("fd49d9a9-609f-4376-bfb4-14b157aae3a9"),
                        Code1 = "ZIMA2024",
                        ServiceInfo = "Сервисная информация3",
                        BeginDate = DateTime.Now.AddDays(-3),
                        EndDate = DateTime.Now.AddDays(4),
                        PartnerName="Интел",
                        CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PartnerManagerId=Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                        //TODO: 
                    }
        };
    }
}
