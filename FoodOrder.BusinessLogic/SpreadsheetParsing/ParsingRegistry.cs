using System;
using FoodOrder.Common;
using FoodOrder.Domain.Enumerations;

namespace FoodOrder.BusinessLogic.SpreadsheetParsing
{
    public static class ParsingRegistry
    {
        public static IParsingStrategy GetParser(SupplierType supplier) {
            if (supplier.Equals(SupplierType.Cafe)) {
                return new KafeParsingStrategy();
            }
            
            if (supplier.Equals(SupplierType.Glagol)) {
                return new GlagolParsingStrategy();
            }
        
            throw new ArgumentException();
        }
    }
}