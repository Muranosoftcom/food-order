import { config } from "../config";

/**
 * @private
 * @return {Promise<any>}
 */
const checkLogin = () =>
	new Promise((resolve, reject) => {
		const GoogleAuth = window.gapi.auth2.getAuthInstance();

		if (!GoogleAuth.isSignedIn.get()) {
			return reject(
				console.error({
					type: "check_login",
					description: "Not authenticated",
					error: null,
				}),
			);
		}

		return resolve(GoogleAuth.currentUser.get());
	});

export const onLoad = () =>
	new Promise((resolve, reject) => {
		if(window.gapi) {
			window.gapi.load("auth2", () => {
				window.gapi.auth2
					.init({
						client_id: config.googleClientId,
						fetch_basic_profile: true,
						scope: "profile",
					})
					.then(resolve, error => reject(error));
			});
		}
	});

export const login = () =>
	new Promise((resolve, reject) => {
		const GoogleAuth = window.gapi.auth2.getAuthInstance();

		GoogleAuth.signIn().then(
			() => checkLogin().then(resolve, reject),
			err =>
				reject(
					console.error({
						type: "auth",
						description: "Authentication failed",
						error: err,
					}),
				),
		);
	});

export const logout = () =>
	new Promise((resolve, reject) => {
		const GoogleAuth = window.gapi.auth2.getAuthInstance();

		GoogleAuth.signOut().then(resolve, reject);
	});
