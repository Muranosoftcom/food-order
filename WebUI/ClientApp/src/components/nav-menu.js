import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Nav, Navbar, NavItem, NavbarToggler, Collapse, Container } from "reactstrap";

class NavMenu extends Component {
	state = {
		isOpen: false,
	};

	render() {
		const { isAuthenticated } = this.props;

		return (
			<Navbar color="primary" dark expand="md" fixed="top">
				<Container>
					<Link to={"/"} className="navbar-brand">
						Горячие обеды
					</Link>
					<NavbarToggler onClick={this.toggle} />
					<Collapse isOpen={this.state.isOpen} navbar>
						<Nav className="ml-auto" navbar>
							<Link to={"/order"}>
								<NavItem color="white">
									{isAuthenticated ? "Заказать обед" : "Войти чтобы Заказать обед"}
								</NavItem>
							</Link>
						</Nav>
					</Collapse>
				</Container>
			</Navbar>
		);
	}
}

export default NavMenu;
