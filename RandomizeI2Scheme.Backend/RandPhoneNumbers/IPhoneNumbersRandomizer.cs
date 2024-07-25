using PhoneNumbers;

namespace RandPhoneNumbers;

public interface IPhoneNumbersRandomizer
{
    public string DefaultNumberRegionIfNotSpecified { get; set; }

    public PhoneNumber GetRanNumber();
    public IEnumerable<CountryCode> GetSupportedRegions();
}

