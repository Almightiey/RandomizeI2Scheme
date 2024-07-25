using FullnameRandomizer;
using I2Scheme.Application.Shared;
using I2Scheme.Persistece.Models;
using RandPhoneNumbers;
using RegularExpressionsRandomizer;

public class RandomizeManager : IRandomizeManager
{
    private readonly IPhoneNumbersRandomizer _numbersRandomizer;
    private readonly IRegexRandomizer _regexRandomizer;
    private readonly IRandomizeName _nameRandomizer;
    public RandomizeManager(IPhoneNumbersRandomizer numbersRandomizer, IRegexRandomizer regexRandomizer, IRandomizeName nameRandomizer)
    {
        _numbersRandomizer = numbersRandomizer;
        _regexRandomizer = regexRandomizer;
        _nameRandomizer = nameRandomizer;
    }

    public I2scheme FillData(I2scheme model)
    {
        foreach (var icon in model.IconInfos)
        {

            var relationshipsSource = model.RelationshipInfos.Where(relationship => relationship.SourceIconId.Equals(icon.Identifier)).ToArray();
            var relationshipsDest = model.RelationshipInfos.Where(relationship => relationship.DestIconId.Equals(icon.Identifier)).ToArray();

            icon.Identifier = RandomizeField(icon.Identifier);

            foreach (var relationship in relationshipsDest)
                relationship.DestIconId = icon.Identifier;

            foreach (var relationship in relationshipsSource)
                relationship.SourceIconId = icon.Identifier;


            if ((bool)icon.Issamelable)
                icon.Label = icon.Identifier;
            else
                icon.Label = RandomizeField(icon?.Label);

            icon.Type = RandomizeField(icon?.Type);
            foreach (var atribute in icon?.AtributeInfos)
            {
                atribute.Value = RandomizeField(atribute?.Value);
                atribute.Name = RandomizeField(atribute?.Name);
            }


        }

        foreach (var relationship in model?.RelationshipInfos)
        {
            relationship.Identifier = RandomizeField(relationship?.Identifier);
            relationship.Label = RandomizeField(relationship?.Label);

            foreach (var atribute in relationship.AtributeInfos)
            {
                atribute.Value = RandomizeField(atribute?.Value);
                atribute.Name = RandomizeField(atribute?.Name);
            }
        }

        return model;
    }

    private string RandomizeField(string value)
    {
        if (value.Contains(TypeAttributes.phoneNumberDataType.ToString()))
        {
            var number = _numbersRandomizer.GetRanNumber();

            var numberInString = number.CountryCode + number.NationalNumber.ToString();

            return numberInString;
        }

        if (value.Contains(TypeAttributes.regexDataType.ToString()))
        {
            var rgEx = value.Split(TypeAttributes.regexDataType + ": ");
            value = _regexRandomizer.GetData(rgEx[1]); //regexDataType: ^[A-Za-z]{10}
        }

        if (value.Contains(TypeAttributes.nameDataType.ToString()))
        {
            var name = _nameRandomizer.GetFullname();
            return name;
        }


        return value;
    }
}