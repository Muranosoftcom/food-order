using System;
using FoodOrder.Common;
using FoodOrder.Domain.Enumerations;

namespace FoodOrder.BusinessLogic.SpreadsheetParsing
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