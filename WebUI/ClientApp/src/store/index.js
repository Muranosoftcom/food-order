import { computed, decorate } from "mobx";

class RootStore {
	constuctor(isAutenticated) {
		this._isAutenticated = isAutenticated;
	}

	user = null;

	get isAuthenticated() {
		return this._isAutenticated;
	}
}

decorate(RootStore, {
	isAuthenticated: computed,
});

export function createStore(isAutenticated) {
	return new RootStore(isAutenticated);
}
