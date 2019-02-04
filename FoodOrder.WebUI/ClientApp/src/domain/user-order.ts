import User from "./user";
import Day from "./day";
import Order from "./order";

export default interface UserOrder {
	user: User;
	day: Day;
	order: Order;
}