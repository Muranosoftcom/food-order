import { config } from "../config";

export function onLoad() {
	const googleAuthParams = {
		client_id: config.googleClientId,
		fetch_basic_profile: true,
		scope: "profile",
	};

	return new Promise((resolve, reject) => {
		window.gapi.load("auth2", () => {
			window.gapi.auth2
				.init(googleAuthParams)
				.then(authResponse => resolve(authResponse), error => reject(error));
		});

		// {
		// Listen for sign-in state changes.
		// window.gapi.auth2.getAuthInstance().isSignedIn.listen(updateSigninStatus);
		// Handle the initial sign-in state.
		// updateSigninStatus(window.gapi.auth2.getAuthInstance().isSignedIn.get());
		// }
		// );
	});
}

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
