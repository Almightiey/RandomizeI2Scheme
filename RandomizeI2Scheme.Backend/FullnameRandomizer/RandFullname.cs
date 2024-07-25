using System.Text.Json;

namespace FullnameRandomizer
{
    public class RandFullname : IRandomizeName
    {
        private List<string> _maleNames;
        private List<string> _femaleNames = new List<string>();
        private List<string> _surnames = new List<string>();
        private readonly Random _random = new Random();

        public RandFullname()
        {
            InitCounties();
        }

        public void InitCounties(Country country = Country.KZ)
        {
            switch (country)
            {
                case Country.KZ:
                    _femaleNames = InitJson(@"KazFemNames.json");
                    _maleNames = InitJson(@"KazMaleNames.json");
                    _surnames = InitJson(@"KazSurNames.json");
                    break;
            }
        }

        public List<string> InitJson(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + fileName;
            Names names = new Names();

            if (File.Exists(path))
                using (StreamReader reader = new StreamReader(path))
                {
                    string text = reader.ReadToEnd();
                    names = JsonSerializer.Deserialize<Names>(text);
                    return names.List;
                }

            throw new NullReferenceException();
        }

        public string GetName(Gender gender = Gender.Male)
        {
            if (gender == Gender.Female)
                return _femaleNames[_random.Next(0, _femaleNames.Count)];

            return _maleNames[_random.Next(0, _maleNames.Count)];
        }

        public string GetSurname(Gender gender = Gender.Male)
        {
            if (gender == Gender.Female)
                return _surnames[_random.Next(0, _surnames.Count)] + 'а';

            return _surnames[_random.Next(0, _surnames.Count)];
        }
        public string GetPatronymic(Gender gender = Gender.Male)
        {
            if (gender == Gender.Female)
                return _maleNames[_random.Next(0, _maleNames.Count)] + "кызы";

            return _maleNames[_random.Next(0, _maleNames.Count)] + "улы";
        }
        public string GetFullname(Gender gender = Gender.Male, Country country = Country.KZ)
        {
            var name = GetName(gender);
            var surname = GetSurname(gender);
            var patronomic = GetPatronymic(gender);
            return $"{surname} {name} {patronomic}";
        }

        public List<string> GetSupportedCountries()
        {
            var countries = Enum.GetValues(typeof(Country));
            List<string> result = new List<string>();
            foreach (var country in countries)
            {
                result.Add(country.ToString());
            }
            return result;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Country
    {
        KZ = 0,
    }
    public class Names
    {
        public List<string> List { get; set; } = new List<string>();
    }

}
