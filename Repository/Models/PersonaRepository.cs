using Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
namespace Repository.Models
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly string _jsonDB;
        public PersonaRepository()
        {
            _jsonDB = Path.Combine("Acudir.Test.Apis\\Test.json");
        }

        private List<Persona> LoadData()
        {
            string jsonContent = File.ReadAllText(_jsonDB);
            var database = JsonSerializer.Deserialize<Dictionary<string, List<Persona>>>(jsonContent);
            return database?["Personas"] ?? new List<Persona>();
        }

        private void SaveData(List<Persona> personas)
        {
            var database = new Dictionary<string, List<Persona>> { { "Personas", personas } };
            string jsonContent = JsonSerializer.Serialize(database, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonDB, jsonContent);
        }

        public List<Persona> GetAll(string? nombre = null, int? edad = null)
        {
            var personas = LoadData();

            if (!string.IsNullOrEmpty(nombre))
                personas = personas.Where(p => p.NombreCompleto.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();

            if (edad.HasValue)
                personas = personas.Where(p => p.Edad == edad.Value).ToList();

            return personas;
        }

        public void Add(Persona persona)
        {
            throw new NotImplementedException();
        }

        public void Update(Persona persona)
        {
            throw new NotImplementedException();
        }
    }
}
