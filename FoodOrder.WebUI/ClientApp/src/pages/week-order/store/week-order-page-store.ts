import { action, computed } from "mobx";

import AppModel from "../../../models/app";
import PageStore from "../../../store/page-store";
import AppStore from "../../../store/app-store";

export default class WeekOrderPageStore extends PageStore {
	constructor(appStore: AppStore, private appModel: AppModel) {
		super(appStore);
	}

	@computed
	public get orders() {
		return this.appModel.orders.myOrders;
	}

	@computed
	public get nextWeekWorkingDays() {
		return this.appModel.calendar.nextWeekWorkingDays;
	}

	@action
	public async fetchOrders() {
		const userId = this.appStore.identity.isAuthenticated ? this.appStore.identity.currentUser!.id : null;
		this.appStore.operationManager.runWithProgress(() => this.appModel.orders.fetchMyOrders(userId));
	}
}
