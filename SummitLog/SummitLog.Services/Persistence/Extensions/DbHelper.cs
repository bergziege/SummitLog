using System;
using SummitLog.Services.Persistence.Enums;

namespace SummitLog.Services.Persistence.Extensions
{
    public static class DbHelper
    {

        public const string CountryIdentifier = "c";
        public const string CountryParamIdentifier = "country";
        public const string AreaIdentifier = "a";
        public const string AreaParamIdentifier = "area";
        public const string SummitGroupIdentifier = "sg";
        public const string SummitGroupParamIdentifier = "summitGroup";
        public const string SummitIdentifier = "s";
        public const string RouteIdentifier = "r";
        public const string RouteParamIdentifier = "route";
        public const string VariationIdentifier = "v";
        public const string VariationParamIdentifier = "param";
        public const string LogEntryIdentifier = "le";
        public const string LogEntryParamIdentifier = "logEntry";
        public const string DifficultyLevelScaleIdentifier = "dls";
        public const string DifficultyLevelScaleParamIdentifier = "difficultyLevelScale";
        public const string DifficultyLevelIdentifier = "dl";
        public const string DifficultyLevelParamIdentifier = "difficultyLevel";


        private static string NodeTemplate =  "{0}({1}:{2})";
        private static string NodeWithParameterTemplate = "{0}({1}:{2} {{{3}}})";

        public static string Country(this string input, string cypherIdentifier = "")
        {
            return String.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.Country);
        }

        public static string Area(this string input, string cypherIdentifier = "")
        {
            return String.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.Area);
        }
        
        public static string SummitGroup(this string input, string cypherIdentifier = "")
        {
            return string.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.SummitGroup);
        }

        public static string Summit(this string input, string cypherIdentifier = "")
        {
            return string.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.Summit);
        }

        public static string Route(this string input, string cypherIdentifier = "")
        {
            return string.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.Route);
        }

        public static string Variation(this string input, string cypherIditifier = "")
        {
            return string.Format(NodeTemplate, input, cypherIditifier, NodeLabels.Variation);
        }

        public static string LogEntry(this string input, string cypherIdentifier = "")
        {
            return string.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.LogEntry);
        }

        public static string DifficultyLevelScale(this string input, string cypherIdentifier = "")
        {
            return string.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.DifficultyLevelScale);
        }

        public static string DifficultyLevel(this string input, string cypherIdentifier = "")
        {
            return String.Format(NodeTemplate, input, cypherIdentifier, NodeLabels.DifficultyLevel);
        }

        public static string CountryWithParam(this string input, string paramIdentifier = CountryParamIdentifier, string cypherIdentifier = CountryIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.Country, paramIdentifier);
        }

        public static string AreaWithParam(this string input, string paramIdentifier = AreaParamIdentifier, string cypherIdentifier = AreaIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.Area, paramIdentifier);
        }

        public static string SummitGroupWithParam(this string input, string paramIdentifier = SummitGroupParamIdentifier, string cypherIdentifier = SummitGroupIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.SummitGroup, paramIdentifier);
        }

        public static string SummitWithParam(this string input, string paramIdentifier = SummitGroupParamIdentifier, string cypherIdentifier = SummitIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.Summit, paramIdentifier);
        }

        public static string RouteWithParam(this string input, string paramIdentifier = RouteParamIdentifier, string cypherIdentifier = RouteIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.Route, paramIdentifier);
        }

        public static string VariationWithParam(this string input, string paramIdentifier = VariationParamIdentifier, string cypherIdentifier = VariationIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.Variation, paramIdentifier);
        }

        public static string LogEntryWithParam(this string input, string paramIdentifier = LogEntryParamIdentifier, string cypherIdentifier = LogEntryIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.LogEntry, paramIdentifier);
        }

        public static string DifficultyLevelScaleWithParam(this string input, string paramIdentifier = DifficultyLevelScaleParamIdentifier, string cypherIdentifier = DifficultyLevelScaleIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.DifficultyLevelScale, paramIdentifier);
        }

        public static string DifficultyLevelWithParam(this string input, string paramIdentifier = DifficultyLevelParamIdentifier, string cypherIdentifier = DifficultyLevelIdentifier)
        {
            return String.Format(NodeWithParameterTemplate, input, cypherIdentifier, NodeLabels.DifficultyLevel, paramIdentifier);
        }

        public static string Has(this string input, string cypherIdentifier = "")
        {
            return $"{input}-[{cypherIdentifier}:HAS]->";
        }

        public static string AnyOutboundRelationAs(this string input, string relationIdentifier)
        {
            return $"{input}-[{relationIdentifier}]->";
        }
    }
}