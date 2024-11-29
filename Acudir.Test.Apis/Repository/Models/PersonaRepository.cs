using Repository.Interface;
using System.Text.Json;
using Dominio;
namespace Repository.Models
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly string _jsonDB = "Test.json";
        public PersonaRepository()
        {
            _jsonDB = Path.Combine(Directory.GetCurrentDirectory(), "Test.json");
        }

        private List<Persona> LoadData()
        {
            string jsonContent = File.ReadAllText(_jsonDB);
            using var document = JsonDocument.Parse(jsonContent);
            var personasJson = document.RootElement.GetProperty("Personas");
            var database = JsonSerializer.Deserialize<List<Persona>>(personasJson.ToString());
            return database ?? new List<Persona>();
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
                personas = personas.Where(p => int.Parse(p.Edad) == edad).ToList();

            return personas;
        }

        public void Add(Persona persona)
        {
            var personas = LoadData();
            personas.Add(persona);
            SaveData(personas);
        }


        public void Update(Persona persona)
        {
            var personas = LoadData();
            //ya que no tengo un atributo unívoco por el cual buscar a la persona, lo busco por nombre y edad.
            var personaAActualizar = personas.FirstOrDefault(p => p.NombreCompleto == persona.NombreCompleto && p.Edad == persona.Edad);  

            if (personaAActualizar != null)
            {
                personaAActualizar.NombreCompleto = persona.NombreCompleto;
                personaAActualizar.Edad = persona.Edad;
                personaAActualizar.Profesion = persona.Profesion;
                personaAActualizar.Domicilio = persona.Domicilio;
                personaAActualizar.Telefono = persona.Telefono;
                SaveData(personas);
            }
        }
    }
}
