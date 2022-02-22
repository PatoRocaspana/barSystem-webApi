using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarSystem.WebApi.Data
{
    public class TableRepository : BaseRepository<Table>, ITableRepository
    {
        public TableRepository(BarSystemDbContext dbContext) : base(dbContext) { }

        protected override void UpdateEntity(Table existingTable, Table newTable)
        {
            existingTable.Waiter = newTable.Waiter;
            existingTable.AmountPeople = newTable.AmountPeople;
            existingTable.ExistAdult = newTable.ExistAdult;
            existingTable.Drinks = newTable.Drinks;
            existingTable.Dishes = newTable.Dishes;
        }
    }
}
