import React from "react";
import { observer, inject } from "mobx-react";
import { withRouter, Redirect, RouteComponentProps } from "react-router-dom";
import { Row, Col, Button } from "reactstrap";

import RegisterNewAccountForm from "./components/register-new-account-form";
import UsersSelectForm from "./components/login-form";
import LoginPageStore from "./store/store";

interface Props extends RouteComponentProps {
	loginPageStore?: LoginPageStore;
}

class LoginPage extends React.Component<Props> {
	get _redirectPath() {
		const { from } = this.props.location.state || { from: { pathname: "/" /* isRegisterNew: false */ } };
		return from;
	}

	render() {
		const { onShowRegisterNewForm, isAuthenticated, showLoginForm, loginForm, signInForm, showSignInForm } = this.props.loginPageStore!;

		return isAuthenticated ? (
			<Redirect to={this._redirectPath} />
		) : (
			<Row>
				<Col lg={{ size: 6, offset: 3 }} md={{ size: 8, offset: 2 }}>
					<Row>
						<Col>
							<h1 className="page-heading">Для входа в систему</h1>
						</Col>
					</Row>
					<Row>
						<Col className="d-flex align-items-baseline">
							<span>выберите пользователя или </span>
							<Button color="link" onClick={onShowRegisterNewForm}>создайте нового</Button>
						</Col>
					</Row>
					{showLoginForm && <Row className="mt-3">
						<Col>
							<UsersSelectForm form={loginForm!}/>
						</Col>
					</Row>}
					{showSignInForm &&
						<Row className="mt-2">
							<Col>
								<RegisterNewAccountForm form={signInForm} />
							</Col>
						</Row>
					}
				</Col>
			</Row>
		);
	}
}

export default inject("loginPageStore")(withRouter(observer(LoginPage)));
