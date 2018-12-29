import React from "react";
import { observer, inject } from "mobx-react";
import { withRouter, Redirect, RouteComponentProps } from "react-router-dom";
import { Row, Col } from "reactstrap";

import SocialLogin  from "./components/social-login";
import LoginPageStore from "./store/store";
import { config } from "../../config";

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
			<Row>
				<Col lg={{ size: 6, offset: 3 }} md={{ size: 8, offset: 2 }}>
					<Row>
						<Col>
							<h1 className="page-heading">Для входа в систему</h1>
						</Col>
					</Row>
					<Row>
						<Col>
							<SocialLogin
								provider="google"
								appId={config.googleClientId}
								onLoginSuccess={loginByGoogle}
								onLoginFailure={(user: any, err: any) => console.error(user, err)}
							>
								Login with Google
							</SocialLogin>
						</Col>
					</Row>
				</Col>
			</Row>
		);
	}
}

export default inject("loginPageStore")(withRouter(observer(LoginPage)));
