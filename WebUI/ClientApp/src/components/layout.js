import React, { Component } from "react";
import PropTypes from "prop-types";
import { Col, Container, Row } from "reactstrap";
import { withCookies, Cookies } from "react-cookie";

import NavMenu from "./nav-menu";
import { Link } from "react-router-dom";
import "./layout.scss";

class Layout extends Component {
	static propTypes = {
		cookies: PropTypes.instanceOf(Cookies).isRequired,
	};

	render() {
		const { cookies } = this.props;
		const isAuthenticated = cookies && Boolean(cookies.get(".AspNetCore.Cookies"));
		console.log(cookies);

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
						<a href="/logout" className="navbar-brand">
							log out
						</a>
					) : (
						<a href="/login" className="navbar-brand">
							log in
						</a>
					)}
				</Row>
			</Container>
		);
	}
}

export default withCookies(Layout);
