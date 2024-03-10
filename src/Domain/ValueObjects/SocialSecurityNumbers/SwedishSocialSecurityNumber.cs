﻿using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects.SocialSecurityNumbers;

public class SwedishSocialSecurityNumber
{
    public struct Options
    {
        public Options()
        {
            AllowInterimNumber = false;
            AllowCoordinationNumber = true;
        }

        public bool AllowCoordinationNumber { get; set; } = true;

        public bool AllowInterimNumber { get; set; } = false;
    }
    
    public DateTime Date { get; private set; }

    public int Age => CalculateAge();

    public string Separator => Age >= 100 ? "+" : "-";

    public string Century { get; }

    public string FullYear { get; }

    public string Year { get; }

    public string Month { get; }

    public string Day { get; }

    public string Numbers { get; }

    public string ControlNumber { get; }

    public bool IsCoordinationNumber { get; }

    public bool IsMale { get; }
    
    /// <summary>
    /// Create a new SwedishSocialSecurityNumber object from a string.
    ///
    /// In case options is not passed, they will default to accept any personal and coordination numbers.
    /// </summary>
    /// <exception cref="SwedishSocialSecurityNumberException">On parse error.</exception>
    /// <param name="ssn">Personal identity number - as string - to create from.</param>
    /// <param name="options">Options object.</param>
    public SwedishSocialSecurityNumber(string ssn, Options? options = null)
    {
        options ??= new Options();
        if (ssn.Length > 13 || ssn.Length < 10)
        {
            var state = ssn.Length < 10 ? "short" : "long";
            throw new SwedishSocialSecurityNumberException($"Input string too {state}");
        }


        if (options.Value.AllowInterimNumber == false && Regex.IsMatch(ssn.Trim(), InterimTest))
        {
            throw new SwedishSocialSecurityNumberException(
                $"{ssn} contains non-integer characters and options are set to not allow interim numbers"
            );
        }

        MatchCollection matches;
        try
        {
            matches = Regex.Matches(ssn);
            if (matches.Count < 1 || matches[0].Groups.Count < 7)
            {
                throw new Exception(); // Just to enter the catch, as it is invalid.
            }
        }
        catch
        {
            throw new SwedishSocialSecurityNumberException("Failed to parse personal identity number. Invalid input.");
        }

        var groups = matches[0].Groups;
        string century;
        var decade = groups["decade"].Value;

        if (!string.IsNullOrEmpty(groups["century"].Value))
        {
            century = groups["century"].Value;
        }
        else
        {
            var born = DateTime.Now.Year - int.Parse(decade);
            if (groups["separator"].Value.Length != 0 && groups["separator"].Value == "+")
            {
                born -= 100;
            }

            century = born.ToString().Substring(0, 2);
        }

        // Create date time object.
        var day = int.Parse(groups["day"].Value);
        if (options.Value.AllowCoordinationNumber)
        {
            IsCoordinationNumber = day > 60;
            day = IsCoordinationNumber ? day - 60 : day;
        }
        else if (day > 60)
        {
            throw new SwedishSocialSecurityNumberException("Invalid personal identity number.");
        }

        var realDay = day;
        Century = century;
        Year = decade;
        FullYear = century + decade;
        Month = groups["month"].Value;
        Day = groups["day"].Value;
        Numbers = groups["numbers"].Value + groups["control"].Value;
        ControlNumber = groups["control"].Value;

        IsMale = int.Parse(Numbers[2].ToString()) % 2 == 1;

        // Try parse date-time to make sure it's actually a real date.
        if (!DateTime.TryParse($"{FullYear}-{Month:00}-{realDay:00}", out _))
        {
            throw new SwedishSocialSecurityNumberException("Invalid personal identity number.");
        }

        var num = groups["numbers"].Value;
        if (options.Value.AllowInterimNumber)
        {
             num = Regex.Replace(num, InterimTest, "1");
        }

        if (Luhn($"{Year}{Month}{Day}{num}") != int.Parse(ControlNumber))
        {
            throw new SwedishSocialSecurityNumberException("Invalid personal identity number.");
        }

        Date = new DateTime(int.Parse($"{FullYear}"), int.Parse(Month), realDay, 0, 0, 0);
    }
    
    /// <summary>
    /// Format the personal identity number into a valid string (YYMMDD-/+XXXX)
    /// If longFormat is true, it will include the century (YYYYMMDD-/+XXXX)
    /// </summary>
    /// <param name="longFormat">If century should be included.</param>
    /// <param name="ignoreSeparator">Whether the separator should be ignored.</param>
    /// <returns>Formatted personal identity number.</returns>
    public string Format(bool longFormat = false, bool ignoreSeparator = false)
    {
        return ignoreSeparator ? $"{(longFormat ? FullYear : Year)}{Month}{Day}{Numbers}" : $"{(longFormat ? FullYear : Year)}{Month}{Day}{Separator}{Numbers}";
    }

    /// <summary>
    /// Parse a personal identity number - as string - into a SwedishSocialSecurityNumber object.
    ///
    /// In case options is not passed, they will default to accept any personal identity and coordination numbers.
    /// </summary>
    /// <param name="ssn">Personal identity number to parse.</param>
    /// <param name="options">Options to use during parsing.</param>
    /// <exception cref="SwedishSocialSecurityNumberException">On invalid personal identity number.</exception>
    /// <returns>Personal identity number as a Personnummer object.</returns>
    public static SwedishSocialSecurityNumber Parse(string ssn, Options? options = null)
    {
        return new SwedishSocialSecurityNumber(ssn, options);
    }

    /// <summary>
    /// Test a personal identity number to see if it is valid or not.
    /// </summary>
    /// <param name="ssn">Personal identity number to test.</param>
    /// <param name="options">Options to use during parsing.</param>
    /// <returns>True if valid, else false.</returns>
    public static bool Valid(string ssn, Options? options = null)
    {
        options ??= new Options();

        try
        {
            Parse(ssn, options);
            return true;
        }
        catch(SwedishSocialSecurityNumberException)
        {
            return false;
        }
    }
    
    private static readonly Regex Regex = new(@"^(?'century'\d{2}){0,1}(?'decade'\d{2})(?'month'\d{2})(?'day'\d{2})(?'separator'[\+\-]?)(?'numbers'((?!000)\d{3}|[TRSUWXJKLMN]\d{2}))(?'control'\d)$");
    private const string InterimTest = "(?![-+])\\D";
    
    private static int Luhn(string value)
    {
        // Luhm algorithm doubles every other number in the value.
        // To get the correct checksum digit we aught to append a 0 on the sequence.
        // If the result becomes a two digit number, subtract 9 from the value.
        // If the total sum is not a 0, the last checksum value should be subtracted from 10.
        // The resulting value is the check value that we use as control number.

        // The value passed is a string, so we aught to get the actual integer value from each char (i.e., subtract '0' which is 48).
        var t   = value.ToCharArray().Select(d => d - 48).ToArray();
        var sum = 0;
        for (var i = 0; i < t.Length; i++)
        {
            var temp = t[i];
            temp *= 2 - (i % 2);
            if (temp > 9)
            {
                temp -= 9;
            }
            sum += temp;
        }

        return ((int)Math.Ceiling(sum / 10.0)) * 10 - sum;
    }
    
    private int CalculateAge()
    {
        var years = DateTime.Now.Year - Date.Year;
        var dateOfBirth = Date.AddYears(years);
        if (dateOfBirth.Date > DateTime.Now.Date)
            years--;
        return years;
    }
}