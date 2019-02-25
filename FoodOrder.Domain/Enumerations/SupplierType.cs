using System;

namespace FoodOrder.Domain.Enumerations {
    public class SupplierType {
        private SupplierType(Guid id) {
            Id = id;
        }

        public Guid Id { get; }

        public static SupplierType Cafe { get; } = new SupplierType(Guid.Parse("6f8bdca0-2fb3-4163-884b-b75b1d20a428"));
        public static SupplierType Glagol { get; } = new SupplierType(Guid.Parse("4ad6432a-ed9d-4947-91a6-78756df51a81"));

        public bool Equals(SupplierType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SupplierType) obj);
        }
    }
}