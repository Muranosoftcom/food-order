import Dish from "./dish";
import { DishCategoryDto } from "./dto";

class DishCategory {
	public id: string;
	public name: string;
	public canMultiSelect: boolean;
	public dishes: Dish[];

	constructor({ id, name, dishes, canMultiSelect = false }: DishCategoryDto) {
		this.id = id;
		this.name = name;
		this.canMultiSelect = canMultiSelect;
		this.dishes = dishes.map(dish => new Dish(dish));
	}
}

export default DishCategory;
