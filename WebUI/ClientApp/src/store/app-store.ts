import { computed } from "mobx";

import OperationManager from "./opearation-manager";
import IdentityStore from "./identity-store";
import CalendarStore from "./calendar-store";
import AppViewModel from "../models/app";

interface Params {
	appModel: AppViewModel;
	identityStore: IdentityStore;
}

export default class AppStore {
	constructor({ appModel, identityStore }: Params ) {
		this.appModel = appModel;
		this.calendar = new CalendarStore(appModel);
		this.identity = identityStore;
		this.operationManager = new OperationManager();

		this.operationManager.runWithProgress(() => this.appModel.init())
	}

	public appModel: AppViewModel;
	public identity: IdentityStore;
	public calendar: CalendarStore;
	public operationManager: OperationManager;

	@computed
	public get isLoading() {
		return this.operationManager.hasPendingTasks;
	};

	@computed
	public get isOrderAllowed() {
		return this.appModel.isOrderAllowed;
	};

	startTimeWatcher() {
		this.appModel.timeWatcher.start();
	}

	stopTimeWatcher() {
		this.appModel.timeWatcher.stop();
	}
}