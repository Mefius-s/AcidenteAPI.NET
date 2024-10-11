using Fiap.Api.Acidentes.Data.Contexts;
using Fiap.Api.Acidentes.Models;
using Fiap.Api.Acidentes.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Acidentes.Data.Repository
{
    public class AcidenteRepository : IAcidenteRepository
    {
        private readonly DatabaseContext _context;

        public AcidenteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<AcidenteModel> GetAllReference(long lastRefenrece, int size)
        {
            var acidentes = _context.Acidentes
                .Where(c => c.Id > lastRefenrece)
                .OrderBy(c => c.Id)
                .Take(size)
                .AsNoTracking()
                .ToList();

            return acidentes;
        }

        public IEnumerable<AcidenteModel> GetAll() => _context.Acidentes.ToList();

        public AcidenteModel GetById(long id) => _context.Acidentes.Find(id);

        public void add(AcidenteModel acidente)
        {
            _context.Acidentes.Add(acidente);
            _context.SaveChanges();
        }

        public void Update(AcidenteModel acidente)
        {
            _context.Acidentes.Update(acidente);
            _context.SaveChanges();
        }

        public void Delete(AcidenteModel acidente)
        {
            _context.Acidentes.Remove(acidente);
            _context.SaveChanges();
        }
        
    }
}
