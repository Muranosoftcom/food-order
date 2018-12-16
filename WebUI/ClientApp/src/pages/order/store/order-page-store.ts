import { action, computed, observable } from "mobx";

import AppViewModel from "../../../models/app";
import AppStore from "../../../store/app-store";
import DayMenu from "../../../domain/day-menu";
import Order from "../../../domain/order";
import Day from "../../../domain/day";
import PageStore from "../../../store/page-store";

class OrderPageStore extends PageStore {
	constructor(appStore: AppStore, private appModel: AppViewModel) {
		super(appStore);
	}

	@computed
	public get nextWeekMenu(): DayMenu[] {
		return this.appModel.orders.nextWeekMenu;
	};

	@computed
	public get myOrders() {
		return this.appModel.orders.myOrders;
	}

	@observable
	public selectedDay: Day | null = null;

	@action.bound
	public switchDay = (day: Day) => {
		if (this.selectedDay !== day) {
			this.selectedDay = day;
		}
	};

	@computed
	get activeDay() {
		let weekDay = null;
		if (this.appModel.orders.nextWeekMenu.length > 0) {
			[{ weekDay }] = this.appModel.orders.nextWeekMenu;
		}

		return this.selectedDay || weekDay
	}

	public orderLunch = async (day: Day, order: Order) => {
		if(!this.appStore.identity.isAuthenticated) {
			throw new Error("User must be authenticated to perform this operation");
		}

		try {
			const user = this.appStore.identity.currentUser!;
			const makeOrder = () => this.appModel.orders.orderLunch( {
				day,
				user,
				order,
			});

			await this.appStore.operationManager.runWithProgress(makeOrder);
		} catch (e) {
			console.error(e.message);
		}
	};
}

export default OrderPageStore;