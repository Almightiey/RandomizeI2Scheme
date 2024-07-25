namespace FullnameRandomizer;

public interface IRandomizeName
{
    string GetFullname(Gender gender = Gender.Male, Country country = Country.KZ);
    List<string> GetSupportedCountries();
    string GetPatronymic(Gender gender = Gender.Male);
    string GetSurname(Gender gender = Gender.Male);
    string GetName(Gender gender = Gender.Male);
}
