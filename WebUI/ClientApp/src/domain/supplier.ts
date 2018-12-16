import DishCategory from "./dish-category";
import Dish from "./dish";
import { SupplierDto } from "./dto";

class Supplier {
	public id: string;
	public name: string;
	public availableMoneyToOrder: number;
	public categories: DishCategory[];

	constructor({ supplierId, supplierName, availableMoneyToOrder, canMultiSelect = true, categories }: SupplierDto) {
		this.id = supplierId;
		this.name = supplierName;
		this.availableMoneyToOrder = availableMoneyToOrder;
		this.categories = categories.map(category => new DishCategory({ ...category, canMultiSelect }));
	}

	get allDishes() {
		return this.categories.reduce((allDishes: Dish[], category) => {
			return [...allDishes, ...category.dishes];
		}, []);
	}
}

export default Supplier;
