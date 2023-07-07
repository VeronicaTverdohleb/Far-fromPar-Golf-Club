using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace DataAccess.DAOs;

public class EquipmentDao: IEquipmentDao
{
    private readonly DataContext context;

    public EquipmentDao(DataContext context)
    {
        this.context = context;
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment)
    {
        EntityEntry<Equipment> added = await context.Equipments.AddAsync(equipment);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task UpdateEquipmentAsync(Equipment equipment)
    {
        context.Equipments.Update(equipment);
        await context.SaveChangesAsync();
    }

    public Task<IEnumerable<Equipment>> GetEquipmentsAsync()
    {
      IEnumerable<Equipment> list = context.Equipments.ToList();
        return Task.FromResult(list);
    }

    public async Task<Equipment?> GetEquipmentByNameAsync(string name)
    {
        var found = await context.Equipments
            .AsNoTracking().SingleOrDefaultAsync(e => e.Name == name);
        return found;
       
    }

    public int GetCountOfEquipment(string name)
    {
        IQueryable<Equipment> q = context.Equipments.AsQueryable();
        int count = 0;
        if (name != null)
        {
            count = q.Where(e=>e.Name==name).GroupBy(e => e.Name).Count();
                
        }

        return count;
    }

    public async Task<Equipment?> GetEquipmentByIdAsync(int id)
    {
        Equipment? found = await context.Equipments.AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
        if (found == null)
        {
            throw new Exception($"Equipment with id {id} not found");
        }
        return found;
    }

    public async Task DeleteEquipmentAsync(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            Equipment? existing = await GetEquipmentByNameAsync(name);
            if (existing == null)
            {
                throw new Exception($"Equipment with name {name} not found");
            }

            context.Equipments.Remove(existing);  
        }
       
        await context.SaveChangesAsync();
    }
}