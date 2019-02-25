import { observable, runInAction } from "mobx";

import DayMenu from "../domain/day-menu";
import UserOrder from "../domain/user-order";
import { DayMenuDto } from "../domain/dto";
import OrderService from "../services/order-service";
import Day from "../domain/day";
import AppModel from "./app";
import MenuService from "../services/menu-service";

class OrderModel {
	constructor(private menuService: MenuService, private orderService: OrderService, private appModel: AppModel) {}

	@observable
	public nextWeekMenu: DayMenu[] = [];
	@observable
	public todayOrders: UserOrder[] = [];
	@observable
	public sharedTodayOrders: UserOrder[] = [];
	@observable
	public myOrders: UserOrder[] = [];

	public orderLunch = async (userOrder: UserOrder) => {
		return this.orderService.orderLunch(userOrder);
	};

	public async fetchWeekMenu() {
		try {
			const weekMenuData = await this.menuService.getWeekMenu();
			const weekMenu = weekMenuData.map((wmd: DayMenuDto) => {
				const day = this.appModel.calendar.getDay(wmd.shortDate) || new Day(wmd.shortDate);
				return new DayMenu(day, wmd.suppliers);
			});

			runInAction(() => {
				this.nextWeekMenu = weekMenu;
			});
		} catch (e) {
			console.log(e.message);
		}
	}

	public async fetchTodayOrders() {
		try {
			let todayOrders = await this.orderService.getTodayOrders();

			runInAction(() => {
				this.todayOrders = todayOrders;
			});
		} catch (e) {
			console.error(e.message);
		}
	}

	public async fetchSharedTodayOrders() {
		try {
			let sharedTodayOrdersData = await this.orderService.getSharedTodayOrders();

			runInAction(() => {
				this.sharedTodayOrders = sharedTodayOrdersData;
			});
		} catch (e) {
			console.error(e.message);
		}
	}

	public async fetchMyOrders(userId: string | null) {
		try {
			let myOrders = await this.orderService.getWeekOrders(userId);

			runInAction(() => {
				this.myOrders = myOrders;
			});
		} catch (e) {
			console.error(e.message);
		}
	}
}

export default OrderModel;