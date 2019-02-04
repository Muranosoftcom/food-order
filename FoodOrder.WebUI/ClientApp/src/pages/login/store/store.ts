import { action, computed } from "mobx";

import PageStore from "../../../store/page-store";

class LoginPageStore extends PageStore {
	@computed
	get isAuthenticated() {
		return this.appStore.identity.isAuthenticated;
	}

	@action.bound
	loginByGoogle(idToken: string) {
		if (idToken) {
			this.appStore.identity.loginByGoogle(idToken);
		}
	}
}

export default LoginPageStore;
