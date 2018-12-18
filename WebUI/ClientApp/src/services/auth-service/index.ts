import axios from "axios";
import jwt from "jsonwebtoken";

import User from "../../domain/user";
import { config } from "../../config";

interface AuthResponse {
	success: boolean;
	token: string;
	message: string;
}

interface UserDto {
	id: string;
	name: string;
	expiresIn: number;
}

const LOCAL_STORAGE_KEY = "food-order-session";

export default class AuthService {
	private readonly domainEndpoint: string = config.backendDomain;
	private expiresAt: number = 0;

	public get isAuthenticated() {
		return new Date().getTime() < this.expiresAt;
	}

	public async login(name: string, password: string) {
		password = this.encryptPassword(password);

		try {
			const { success, token }: AuthResponse = (await axios.post(`${this.domainEndpoint}login`, { name, password })).data;

			if ( success ) {
				let user: UserDto | null = null;

				try {
					user = jwt.verify(token, config.secret) as UserDto;
				} catch (e) {
					console.error(e);
					return null;
				}

				this.setSession(user.expiresIn, token);

				return new User(user.name, user.id);
			} else {
				return null;
			}
		} catch (e) {
			return Promise.resolve(null);
		}
	}

	public async signin(name: string, password: string) {
		password = this.encryptPassword(password);

		try {
			const { success, token }: AuthResponse = (await axios.post(`${this.domainEndpoint}signin`, { name, password })).data;

			if ( success ) {
				let user: UserDto | null = null;

				try {
					user = jwt.verify(token, config.secret) as UserDto;
				} catch (e) {
					console.error(e);
					return null;
				}

				this.setSession(user.expiresIn, token);

				return new User(user.name, user.id);
			} else {
				return null;
			}
		} catch (e) {
			return Promise.resolve(null);
		}
	}

	public logout() {
		localStorage.removeItem(LOCAL_STORAGE_KEY);
	}

	public authenticate() {
		let sessionToken = window.localStorage.getItem(LOCAL_STORAGE_KEY);
		if (!sessionToken) {
			return null;
		}

		return this.getUserFromSession(sessionToken);
	}

	//TODO:  KILLME !!!
	public async getAllUsers() {
		return (await axios(`${this.domainEndpoint}api/users`)).data;
	}

	private getUserFromSession(sessionToken: string): User | null {
		let user = null;

		try {
			user = jwt.verify(sessionToken, config.secret) as UserDto;
		} catch (e) {
			console.error(e);
			return null;
		}

		return new User(user.name, user.id);
	}

	private setSession(expiresIn: number, token: string) {
		// set the time that the id token will expire at
		this.expiresAt = expiresIn * 1000 + new Date().getTime();

		localStorage.setItem(LOCAL_STORAGE_KEY, token);
	}

	private encryptPassword(password: string) {
		return password;
	}
}