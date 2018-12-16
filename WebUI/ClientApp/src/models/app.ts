import { computed } from "mobx";

import TimeWatcher from "./time-watcher";
import { config } from "../config";
import CalendarService from "../services/calendar-service";
import CalendarModel from "./calendar";
import OrderModel from "./order";
import MenuService from "../services/menu-service";
import OrderService from "../services/order-service";

interface Params {
	calendarService: CalendarService;
	menuService: MenuService;
	orderService: OrderService;
}

class AppViewModel {
	constructor({ calendarService, menuService, orderService }: Params) {
		this.timeWatcher = new TimeWatcher(config.timeUpdateInterval);
		this.calendar = new CalendarModel(calendarService);
		this.orders = new OrderModel(menuService, orderService, this);
	}

	public timeWatcher: TimeWatcher;
	public calendar: CalendarModel;
	public orders: OrderModel;

	public async init() {
		await this.calendar.fetchDays();

		return Promise.all([
			this.orders.fetchWeekMenu(),
			this.orders.fetchTodayOrders(),
			this.orders.fetchSharedTodayOrders(),
		])
	}

	@computed
	public get isOrderAllowed() {
		return this.timeWatcher.currentTime.isBetween(this.timeWatcher.currentThursday, this.timeWatcher.currentSunday);
	}
}

export default AppViewModel
