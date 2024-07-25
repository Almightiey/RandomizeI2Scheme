using PhoneNumbers;
using System.Text.Json;

namespace RandPhoneNumbers
{
    public class RandPhoneNumbers : IPhoneNumbersRandomizer
    {
        public string DefaultNumberRegionIfNotSpecified { get; set; }
        private readonly PhoneNumberUtil _phoneUtil;
        private readonly string fileName = "CountryCodes.json";

        private Random rnd = new Random();
        public RandPhoneNumbers()
        {
            _phoneUtil = PhoneNumberUtil.GetInstance();
        }

        public RandPhoneNumbers(string CountyCode)
        {
            DefaultNumberRegionIfNotSpecified = CountyCode;
            _phoneUtil = PhoneNumberUtil.GetInstance();
        }

        public PhoneNumber GetRanNumber()
        {

            if (string.IsNullOrWhiteSpace(DefaultNumberRegionIfNotSpecified))
                DefaultNumberRegionIfNotSpecified = "KZ";

            var exampleNumberProto = _phoneUtil.GetExampleNumber(DefaultNumberRegionIfNotSpecified);

            bool valid = false;
            PhoneNumber parsedNumber = new PhoneNumber();
            while (!valid)
            {
                string str = "";
                for (int i = 0; i < exampleNumberProto.NationalNumber.ToString().Length; i++)
                    str += rnd.Next(0, 9).ToString();

                parsedNumber = _phoneUtil.Parse(exampleNumberProto.CountryCode + str.ToString(), DefaultNumberRegionIfNotSpecified);
                valid = _phoneUtil.IsValidNumber(parsedNumber);
            }
            return parsedNumber;

        }

        public IEnumerable<CountryCode> GetSupportedRegions()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + fileName;
            List<CountryCode> regions = new List<CountryCode>();

            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                regions = JsonSerializer.Deserialize<List<CountryCode>>(json);
            }


            var phoneUtilRegions = _phoneUtil.GetSupportedRegions();

            var regionsCode = regions.Select(region => region.Code);

            var SupportedRegions = phoneUtilRegions.Intersect(regionsCode);

            var SupportedCountryCodeRegions = regions.Where(region => SupportedRegions.Contains(region.Code));

            return SupportedCountryCodeRegions;
        }
    }
}
