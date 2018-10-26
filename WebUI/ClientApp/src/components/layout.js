import React, { Component } from "react";
import { Col, Container, Row } from "reactstrap";

import NavMenu from "./nav-menu";

class Layout extends Component {
	render() {
		return (
			<Container>
				<Row>
					<Col>
						<NavMenu />
					</Col>
				</Row>
				<Row>
					<Col>{this.props.children}</Col>
				</Row>
			</Container>
		);
	}
}

export default Layout;
