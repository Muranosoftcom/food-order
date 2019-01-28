import { observable, runInAction } from "mobx";

import ajax from "../../../vendors/ajax";
import PageStore from "../../../store/page-store";
import AppStore from "../../../store/app-store";

class AdminPageStore extends PageStore {
	constructor(appStore: AppStore) {
		super(appStore);
	}

	@observable
	isSyncEnabled: boolean = true;

	public async syncMenu() {
		runInAction(() => {
			this.isSyncEnabled = false;
		});

		const resp = await this.appStore.operationManager.runWithProgress(() => ajax().post("/api/menu/sync"));

		alert(resp.data.syncResult);

		runInAction(() => {
			this.isSyncEnabled = true;
		});
	};
}

export default AdminPageStore;