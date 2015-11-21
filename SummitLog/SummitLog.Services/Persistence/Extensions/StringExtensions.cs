using System;
using SummitLog.Services.Persistence.Enums;

namespace SummitLog.Services.Persistence.Extensions
{
    public static class StringExtensions
    {
        public static string Country(this string input, string cypherIdentifier)
        {
            return $"{input}({cypherIdentifier}:{NodeLabels.Country})";
        }

        public static string CountryWithParam(this string input, string cypherIdentifier, string paramIdentifier)
        {
            return $"{input}({cypherIdentifier}:{NodeLabels.Country} {{{paramIdentifier}}})";
        }
    }
}