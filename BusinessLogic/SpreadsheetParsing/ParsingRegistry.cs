using System;
using Core;

namespace BusinessLogic.SpreadsheetParsing
{
    public static class ParsingRegistry
    {
        public static IParsingStrategy GetParser(FoodProvider provider)
        {
            switch (provider)
            {
                case FoodProvider.Kafe:
                    return new KafeParsingStrategy();
                case FoodProvider.Glagol:
                    return new GlagolParsingStrategy();
                default: 
                    throw new ArgumentException();
            }
        }
    }
}