import { observable, action, computed, runInAction } from "mobx";

import { UserDto } from "../domain/dto";
import User from "../domain/user";
import AuthService from "../services/auth-service";
import CryptoService from "../services/crypto-service";

class IdentityStore {
	constructor(private readonly authService: AuthService, private readonly cryptoService: CryptoService) {
		const user = authService.authenticate();

		this.auth(user);
		this.fetchAllUsers();
	}

	@observable
	public users: UserDto[] = [];

	@action
	private async fetchAllUsers() {
		try {
			let users = await this.authService.getAllUsers();

			runInAction(() => {
				this.users = users;
			});
		} catch (e) {
			console.error(e.message);
		}
	}

	@observable
	public currentUser: User | null = null;

	@computed
	public get isAuthenticated() {
		return this.currentUser !== null;
	};

	@action
	public async login(name: string, password: string) {
		const user = await this.authService.login(name, password);

		return this.auth(user);
	}

	@action
	public async signIn(name: string, password: string) {
		password = await this.cryptoService.encodePassword(password);
		const user = await this.authService.signin(name, password);

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
