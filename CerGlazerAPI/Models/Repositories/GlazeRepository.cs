namespace CerGlazerAPI.Models.Repositories
{
    public static class GlazeRepository
    {
        private static List<Glaze> _glazes = new List<Glaze>
        {
            new Glaze { Id = 1, Name = "Glaze1", IdealConeCategory = "Low" },
            new Glaze { Id = 2, Name = "Glaze2", IdealConeCategory = "Medium" },
            new Glaze { Id = 3, Name = "Glaze3", IdealConeCategory = "High" }
        };

        public static List<Glaze> GetAllGlazes()
        {
            return _glazes;
        }

        public static Glaze GetGlazeById(int id)
        {
            return _glazes.FirstOrDefault(g => g.Id == id) ??
                new Glaze { Id = 0, Name = "Not Found", IdealConeCategory = "N/A" };
        }

    }
}
