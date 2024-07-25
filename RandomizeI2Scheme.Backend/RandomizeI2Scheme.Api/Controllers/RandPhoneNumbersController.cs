using Microsoft.AspNetCore.Mvc;
using RandPhoneNumbers;

namespace RandScheme.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RandPhoneNumbersController : ControllerBase
{
    private readonly IPhoneNumbersRandomizer _phoneNumbersRandomizer;

    public RandPhoneNumbersController(IPhoneNumbersRandomizer phoneNumbersRandomizer)
    {
        _phoneNumbersRandomizer = phoneNumbersRandomizer;
    }

    [HttpGet]
    public async Task<ActionResult> GetRandomNumber()
    {
        var number = _phoneNumbersRandomizer.GetRanNumber();

        var numberInString = number.CountryCode.ToString() + number.NationalNumber.ToString();

        return Ok(numberInString);
    }

    [HttpGet("{Count}")]
    public async Task<ActionResult> GetRandomNumbers(int Count)
    {
        List<string> phoneNumbers = GetNumList(Count);

        return Ok(phoneNumbers);
    }

    [HttpGet("{CountryCode}/{Count}")]
    public async Task<ActionResult> GetRandomNumbers(string CountryCode, int Count)
    {
        _phoneNumbersRandomizer.DefaultNumberRegionIfNotSpecified = CountryCode.ToUpper();
        List<string> phoneNumbers =  GetNumList(Count);

        return Ok(phoneNumbers);
    }

    [Route("GetSupportedRegions")]
    [HttpGet]
    public IEnumerable<CountryCode> GetSupportedRegions()
    {
        return _phoneNumbersRandomizer.GetSupportedRegions();
    }


    private List<string> GetNumList(int Count)
    {
        List<string> phoneNumbers = new List<string>();
        for (int i = 0; i < Count; i++)
        {
            var number = _phoneNumbersRandomizer.GetRanNumber();
            var numberInString = number.CountryCode.ToString() + number.NationalNumber.ToString();

            phoneNumbers.Add(numberInString);
        }

        return phoneNumbers;
    }
}