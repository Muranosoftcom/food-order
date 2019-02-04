import { observable, action, runInAction, computed } from "mobx";

export default class OperationManager {
	@observable
	pendingTasksCount: number = 0;

	@computed
	get hasPendingTasks() {
		return this.pendingTasksCount > 0;
	}

	@action
	async runWithProgress<T>(operation: () => Promise<T>) {
		this.pendingTasksCount++;

		return operation().finally(() => {
			runInAction(() => {
				this.pendingTasksCount--;
			});
		});
	}
}
