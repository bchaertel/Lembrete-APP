using LembreteApp.Interfaces;
using LembreteApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LembreteApp.Services
{
    public class LembreteService : ILembreteService
    {
        private readonly IMongoCollection<Lembrete> _lembretes;

        public LembreteService(IMongoClient client)
        {
            var database = client.GetDatabase("LembreteApp");
            _lembretes = database.GetCollection<Lembrete>("Lembretes");
        }

        public async Task<List<Lembrete>> GetAsync()
        {
            return await _lembretes.Find(lembrete => true).ToListAsync();
        }

        public async Task<Lembrete> GetAsync(string id)
        {
            return await _lembretes.Find<Lembrete>(lembrete => lembrete.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Lembrete> CreateAsync(Lembrete lembrete)
        {
            await _lembretes.InsertOneAsync(lembrete);
            return lembrete;
        }

        public async Task UpdateAsync(string id, Lembrete lembreteIn)
        {
            await _lembretes.ReplaceOneAsync(lembrete => lembrete.Id == id, lembreteIn);
        }

        public async Task RemoveAsync(string id)
        {
            await _lembretes.DeleteOneAsync(lembrete => lembrete.Id == id);
        }

        // Métodos síncronos para atender à interface ILembreteService
        public List<Lembrete> Get()
        {
            return _lembretes.Find(lembrete => true).ToList();
        }

        public Lembrete Get(string id)
        {
            return _lembretes.Find<Lembrete>(lembrete => lembrete.Id == id).FirstOrDefault();
        }

        public Lembrete Create(Lembrete lembrete)
        {
            _lembretes.InsertOne(lembrete);
            return lembrete;
        }

        public void Update(string id, Lembrete lembreteIn)
        {
            _lembretes.ReplaceOne(lembrete => lembrete.Id == id, lembreteIn);
        }

        public void Remove(string id)
        {
            _lembretes.DeleteOne(lembrete => lembrete.Id == id);
        }

        public void Remove(Lembrete lembreteIn)
        {
            _lembretes.DeleteOne(lembrete => lembrete.Id == lembreteIn.Id);
        }
    }
}
