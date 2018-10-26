import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Nav, Navbar, NavItem, NavbarToggler, Collapse, Container } from "reactstrap";

class NavMenu extends Component {
	state = {
		isOpen: false,
	};

	render() {
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
								<NavItem color="light">Заказать обед</NavItem>
							</Link>
							<Link to={"/fetchdata"}>
								<NavItem color="light">Fetch data</NavItem>
							</Link>
						</Nav>
					</Collapse>
				</Container>
			</Navbar>
		);
	}
}

export default NavMenu;
