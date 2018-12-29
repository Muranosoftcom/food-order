import axios from "axios";
import jwt from "jsonwebtoken";

import User from "../../domain/user";
import { config } from "../../config";

interface AuthResponse {
	success: boolean;
	token: string;
	message: string;
}

interface UserPayload {
	id: string;
	fullName: string;
	exp: number;
	isAdmin: string;
	pictureUrl: string;
}

const LOCAL_STORAGE_KEY = "food-order-session";

export default class AuthService {
	public async loginByGoogle(googleIdToken: string) {
		try {
			const { token }: AuthResponse = (await axios({
				method: "post",
				url: "/api/auth/google-token-signin",
				data: JSON.stringify(googleIdToken),
				headers: {
					"Content-Type": "application/json"
				}
			})).data;

			let user: User | null = this.getUserFromToken(token);

			if (!user) {
				return null;
			}

			this.setSession(token);

			return user;
		} catch (e) {
			return Promise.resolve(null);
		}
	}

	public logout() {
		localStorage.removeItem(LOCAL_STORAGE_KEY);
	}

	public authenticate() {
		let sessionToken = window.localStorage.getItem(LOCAL_STORAGE_KEY);

		return sessionToken ? this.getUserFromToken(sessionToken) : null;
	}

	private getUserFromToken(sessionToken: string): User | null {
		let user: UserPayload | null = this.parseToken(sessionToken);

		if (!user) {
			return null;
		}

		return new User(user.fullName, user.id, user.isAdmin === "true", user.pictureUrl);
	}

	private parseToken(token: string) {
		try {
			return jwt.verify(token, config.secret) as UserPayload;
		} catch (e) {
			console.error(e);
			return null;
		}
	}

	private setSession(token: string) {
		localStorage.setItem(LOCAL_STORAGE_KEY, token);
	}
}