import React, { Component } from "react";
import cn from "classnames";

import { login } from "../../vendors/google-api-auth";
import { ReactComponent as GoogleSigninLogo } from "../../images/google-signin.svg";
import "./index.scss";

interface Props {
	className?: string;
	onLoginSuccess?(user: any): void;
	onLoginFailure?(user: any): void;
	onLogoutSuccess?(): void;
}

export class GoogleSigninButton extends Component<Props> {
	private handleLogin = () => {
		const { onLoginSuccess, onLoginFailure } = this.props;

		login().then(googleUser => {
			const authResponse = googleUser.getAuthResponse(true);
			const tokenId = authResponse["id_token"];

			if (onLoginSuccess) {
				onLoginSuccess(tokenId);
			}
		}).catch(onLoginFailure);
	};

	render() {
		const { className } = this.props;
		const classNames = cn("google-button", className);

		return (
			<button
				className={classNames}
				onClick={this.handleLogin}
			>
				<GoogleSigninLogo className="google-button__icon"/>
				<span className="google-button__text">Sign in with Google</span>
			</button>
		);
	}
}
