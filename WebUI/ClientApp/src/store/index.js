import { observable, computed, action, decorate } from "mobx";

class RootStore {
	constuctor() {

	}

	user = null;

	get isAuthenticated() {
		return this.user !== null;
	}
}

decorate(RootStore, {
	user: observable,
	isAuthenticated: computed,
});

export default new RootStore();
