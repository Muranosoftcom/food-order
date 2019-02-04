import ajax from "../../vendors/ajax";

import UserOrder from "../../domain/user-order";

export default class OrderService {
	constructor(private readonly api: string) {
		this.api = `${api}/order`;
	}

	public async getTodayOrders(): Promise<UserOrder[]> {
		return (await ajax().get(`${this.api}/today-orders/`)).data;
	}

	public async getSharedTodayOrders(): Promise<UserOrder[]> {
		return (await ajax().get(`${this.api}/shared-today-orders/`)).data;
	}

	public orderLunch(userOrder: UserOrder) {
		return ajax().post(`${this.api}/order-lunch/`, userOrder);
	}

	public async getWeekOrders(userId: string | null): Promise<UserOrder[]> {
		if (!userId) {
			return []
		}

		return (await ajax().get(`${this.api}/week-orders/`)).data;
	}
}