using System;
using Core;

namespace BusinessLogic.SpreadsheetParsing
{
    public static class ParsingRegistry
    {
        public static IParsingStrategy GetParser(FoodSupplier supplier)
        {
            switch (supplier)
            {
                case FoodSupplier.Cafe:
                    return new KafeParsingStrategy();
                case FoodSupplier.Glagol:
                    return new GlagolParsingStrategy();
                default: 
                    throw new ArgumentException();
            }
        }
    }
}