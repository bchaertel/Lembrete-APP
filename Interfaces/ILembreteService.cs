using LembreteApp.Models;

namespace LembreteApp.Interfaces;

public interface ILembreteService
{
    List<Lembrete> Get();
    Lembrete Get(string id);
    Lembrete Create(Lembrete lembrete);
    void Update(string id, Lembrete lembreteIn);
    void Remove(Lembrete lembreteIn);
    void Remove(string id);
}