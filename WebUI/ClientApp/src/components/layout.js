import React, { Component } from "react";
import PropTypes from "prop-types";
import { Col, Container, Row } from "reactstrap";

import NavMenu from "./nav-menu";

import "./layout.scss";
import { inject, observer } from "mobx-react";

import axios from "axios";

class Layout extends Component {
	static propTypes = {
		rootStore: PropTypes.shape({
			isAuthenticated: PropTypes.bool,
		}).isRequired,
	};

	handleLogin = () => {
		axios.get("/login");
	};

	handleLogout = () => {
		axios.get("/logout");
	};

	render() {
		const { isAuthenticated } = this.props.rootStore;

		return (
			<Container className="layout">
				<Row>
					<Col>
						<NavMenu className="layout__header" isAuthenticated={isAuthenticated} />
					</Col>
				</Row>
				<Row className="layout__body">
					<Col>{this.props.children}</Col>
				</Row>
				<Row>
					{isAuthenticated ? (
						<button className="navbar-brand" onClick={this.handleLogout}>
							log out
						</button>
					) : (
						<button className="navbar-brand" onClick={this.handleLogin}>
							log in
						</button>
					)}
				</Row>
			</Container>
		);
	}
}

export default inject("rootStore")(observer(Layout));
