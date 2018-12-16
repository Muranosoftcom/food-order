import { DishDto } from "./dto";

class Dish {
	public id: string;
	public name: string;
	public price: number;
	public positiveReviews: number;
	public negativeReviews: number;

	constructor({ id, name, price, negativeReviews = 0, positiveReviews = 0 }: DishDto) {
		this.id = id;
		this.name = name;
		this.price = price;
		this.positiveReviews = positiveReviews;
		this.negativeReviews = negativeReviews;
	}

	public get rating() {
		return this.positiveReviews - this.negativeReviews;
	}
}

export default Dish;
