import Dish from "./dish";

export default interface Order {
	supplierName: string;
	dishes: Dish[];
}