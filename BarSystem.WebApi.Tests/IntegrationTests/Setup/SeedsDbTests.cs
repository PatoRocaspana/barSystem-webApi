using BarSystem.WebApi.Data;
using BarSystem.WebApi.Models;
using BarSystem.WebApi.Models.Enum;
using System;
using System.Collections.Generic;

namespace BarSystem.WebApi.Tests.IntegrationTests
{
    public static class SeedsDbTests
    {
        public static List<Dish> GetSeedingDishes()
        {
            var dishA = new Dish()
            {
                Id = 1,
                Name = "DishA",
                Description = "Description of DishA",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false
            };

            var dishB = new Dish()
            {
                Id = 2,
                Name = "DishB",
                Description = "Description of DishB",
                Price = 200,
                Stock = 20,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(45),
                IsReady = true
            };

            var dishC = new Dish()
            {
                Id = 3,
                Name = "DishC",
                Description = "Description of DishC",
                Price = 200,
                Stock = 20,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(45),
                IsReady = true
            };

            var dishList = new List<Dish>()
            {
                dishA,
                dishB,
                dishC
            };

            return dishList;
        }

        public static List<Drink> GetSeedingDrinks()
        {
            var drinkA = new Drink()
            {
                Id = 1,
                Name = "DrinkA",
                Description = "Description of DrinkA",
                Price = 100,
                Stock = 10,
                Category = DrinkCategory.Beer
            };

            var drinkB = new Drink()
            {
                Id = 2,
                Name = "DrinkB",
                Description = "Description of DrinkB",
                Price = 200,
                Stock = 20,
                Category = DrinkCategory.Soda
            };

            var drinkC = new Drink()
            {
                Id = 3,
                Name = "DrinkC",
                Description = "Description of DrinkC",
                Price = 200,
                Stock = 20,
                Category = DrinkCategory.Soda
            };

            var drinkList = new List<Drink>()
            {
                drinkA,
                drinkB,
                drinkC
            };

            return drinkList;
        }

        public static List<Employee> GetSeedingEmployees()
        {
            var employeeA = new Employee()
            {
                Id = 1,
                Dni = "12345678",
                FirstName = "EmployeeA",
                LastName = "EmployeeALastName",
                Role = Role.Manager
            };

            var employeeB = new Employee()
            {
                Id = 2,
                Dni = "98765432",
                FirstName = "EmployeeB",
                LastName = "EmployeeBLastName",
                Role = Role.Cook
            };

            var employeeC = new Employee()
            {
                Id = 3,
                Dni = "99888777",
                FirstName = "EmployeeC",
                LastName = "EmployeeCLastName",
                Role = Role.Cook
            };

            var employeeList = new List<Employee>()
            {
                employeeA,
                employeeB,
                employeeC
            };

            return employeeList;
        }

        public static List<Table> GetSeedingTables(BarSystemDbContext db)
        {
            var listDish = new List<Dish>();
            listDish.Add(db.Dishes.Find(1));

            var listDrink = new List<Drink>();
            listDrink.Add(db.Drinks.Find(1));

            var employee = db.Employees.Find(1);

            var tableA = new Table()
            {
                Id = 1,
                AmountPeople = 10,
                ExistAdult = true,
                EmployeeId = 1,
                Employee = employee,
                Dishes = listDish,
                Drinks = listDrink
            };

            var tableB = new Table()
            {
                Id = 2,
                AmountPeople = 20,
                ExistAdult = true,
                EmployeeId = 1,
                Employee = employee,
                Dishes = listDish,
                Drinks = listDrink
            };

            var tableC = new Table()
            {
                Id = 33,
                AmountPeople = 30,
                ExistAdult = true,
                EmployeeId = 1,
                Employee = employee,
                Dishes = listDish,
                Drinks = listDrink
            };

            var tableList = new List<Table>()
            {
                tableA,
                tableB,
                tableC
            };

            return tableList;
        }
    }
}
