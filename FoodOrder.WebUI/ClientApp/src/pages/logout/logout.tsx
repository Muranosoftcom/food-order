import React from "react";
import { inject, observer } from "mobx-react";
import { Link } from "react-router-dom";
import { Col, Row, Button } from "reactstrap";

import AppStore from "../../store/app-store";
import { logout } from "../../vendors/google-api-auth";

interface Props {
	appStore?: AppStore;
}

@inject("appStore")
@observer
class LogoutPage extends React.Component<Props> {
	componentDidMount() {
		this.handleLogout();
	}

	private handleLogout = async () => {
		await logout();
		this.props.appStore!.identity.logout();
	};

	render() {
		const { isAuthenticated } = this.props.appStore!.identity;

		return (
			<Row tag="section" className="mt-4">
				{isAuthenticated ? (
					<Col>
						<Button onClick={this.handleLogout}>Выйти</Button>
					</Col>
				) : (
					<Col lg={{ size: 6, offset: 3 }} md={{ size: 8, offset: 2 }}>
						<div>Мы будем скучать по тебе :)</div>
						<div>
							<Link to="/">Вернуться на главную</Link>
						</div>
					</Col>
				)}
			</Row>
		);
	}
}

export default LogoutPage;
