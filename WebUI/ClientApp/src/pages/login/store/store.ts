import { action, computed } from "mobx";

import PageStore from "../../../store/page-store";

class LoginPageStore extends PageStore {
	@computed
	get isAuthenticated() {
		return this.appStore.identity.isAuthenticated;
	}

	@action.bound
	loginByGoogle(user: any, err: any) {
		if (user && user.token) {
			this.appStore.identity.loginByGoogle(user.token.idToken);
		} else {
			console.error(err);
		}
	}
}

export default LoginPageStore;
