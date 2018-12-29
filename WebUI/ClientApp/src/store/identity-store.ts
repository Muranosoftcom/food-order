import { observable, action, computed } from "mobx";

import User from "../domain/user";
import AuthService from "../services/auth-service";

class IdentityStore {
	constructor(private readonly authService: AuthService) {
		const user = authService.authenticate();

		this.auth(user);
	}

	@observable
	public currentUser: User | null = null;

	@computed
	public get isAuthenticated() {
		return this.currentUser !== null;
	};

	@action
	public async loginByGoogle(token: string) {
		const user = await this.authService.loginByGoogle(token);

		return this.auth(user);
	}

	@action
	public logout() {
		this.authService.logout();
		this.currentUser = null;
	}

	@action
	private auth(user: User | null) {
		this.currentUser = user;
	}
}

export default IdentityStore;
