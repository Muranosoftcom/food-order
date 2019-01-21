import { computed } from "mobx";

import AppStore from "../../../store/app-store";
import PageStore from "../../../store/page-store";
import AppViewModel from "../../../models/app";

class HomePageStore extends PageStore {
	constructor(appStore: AppStore, private appModel: AppViewModel) {
		super(appStore);
	}

	@computed
	public get todayOrders() {
		return this.appModel.orders.todayOrders;
	};

	@computed
	public get sharedTodayOrders() {
		return this.appModel.orders.sharedTodayOrders;
	};
}

export default HomePageStore;
