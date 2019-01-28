import React from "react";
import { observer, inject } from "mobx-react";
import { withRouter, Redirect, RouteComponentProps } from "react-router-dom";
import { Row, Col } from "reactstrap";

import { GoogleSigninButton } from "../../components/google-signin-button";
import LogoImageUrl from "../../images/food-logo-icon.png";

import LoginPageStore from "./store/store";
import "./index.scss";

interface Props extends RouteComponentProps {
	loginPageStore?: LoginPageStore;
}

class LoginPage extends React.Component<Props> {
	private get redirectPath() {
		const { from } = this.props.location.state || { from: { pathname: "/" } };
		return from;
	}

	public render() {
		const { isAuthenticated, loginByGoogle } = this.props.loginPageStore!;

		return isAuthenticated ? (
			<Redirect to={this.redirectPath} />
		) : (
			<Row className="login-page">
				<Col className="login-page__content" lg={{ size: 6, offset: 3 }} md={{ size: 8, offset: 2 }}>
					<Row>
						<Col className="login-page__brand-info brand-info">
							<img className="brand-info__logo" src={LogoImageUrl} alt="Food Order App Logo"/>
						</Col>
					</Row>
					<Row>
						<Col className="login-page__signin-button-wrapper">
							<GoogleSigninButton
								className="login-page__signin-button"
								onLoginSuccess={loginByGoogle}
								onLoginFailure={error => console.error(error)}
							/>
						</Col>
					</Row>
				</Col>
			</Row>
		);
	}
}

export default inject("loginPageStore")(withRouter(observer(LoginPage)));
