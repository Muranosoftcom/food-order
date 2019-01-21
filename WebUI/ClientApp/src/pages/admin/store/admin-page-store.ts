import ajax from "../../../vendors/ajax";
import PageStore from "../../../store/page-store";
import AppStore from "../../../store/app-store";

class AdminPageStore extends PageStore {
	constructor(appStore: AppStore) {
		super(appStore);
	}

	public async syncMenu() {
		const resp = await this.appStore.operationManager.runWithProgress(() => ajax().post("/api/menu/sync"));
		alert(resp.data.syncResult);
	};
}

export default AdminPageStore;