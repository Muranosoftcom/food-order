import React from "react";
import { Link } from "react-router-dom";
import {
	Container,
	Navbar,
	NavbarToggler,
	Collapse,
	Nav,
	NavItem,
	UncontrolledDropdown,
	DropdownToggle,
	DropdownMenu, DropdownItem
} from "reactstrap";
import { observer, inject } from "mobx-react";
import { action, observable } from "mobx";

import User from "./user/user";
import AppStore from "../../store/app-store";
import MyOrderStore from "../../store/my-order-store";
import "./header.scss";

interface Props {
	appStore?: AppStore;
	myOrderStore?: MyOrderStore;
	className: string;
}

@inject(({ appStore, myOrderStore }) => ({
	appStore: appStore as AppStore,
	myOrderStore: myOrderStore as MyOrderStore,
}))
@observer
class Header extends React.Component<Props> {
	@observable
	private isOpen: boolean = false;

	private toggle = action(() => {
		this.isOpen = !this.isOpen;
	});

	private closeMenu = action(() => {
		this.isOpen = false;
	})

	public render() {
		const { className } = this.props;
		const { identity } = this.props.appStore!;
		const { canOrder } = this.props.myOrderStore!;

		return (
			<header className={`header ${className}`}>
				<Container className="px-0">
					<Navbar dark expand="md">
						<Link className="navbar-brand" to="/">
							Горячие обеды
						</Link>
						<NavbarToggler onClick={this.toggle} />
						<Collapse isOpen={this.isOpen} navbar>
							<Nav className="ml-auto mr-auto" navbar>
								{canOrder &&
									<NavItem>
										<Link className="nav-link" to="/order/" onClick={this.closeMenu}>
											Заказать обед
										</Link>
									</NavItem>
								}
								<NavItem>
									<Link className="nav-link" to="/week-order/" onClick={this.closeMenu}>
										Заказ на неделю
									</Link>
								</NavItem>
								<NavItem>
									<Link className="nav-link" to="/my-order/" onClick={this.closeMenu}>
										Мой обед
									</Link>
								</NavItem>
							</Nav>
							<Nav navbar>
								{!identity.isAuthenticated && (
									<NavItem>
										<Link className="nav-link" to="/login/" onClick={this.closeMenu}>
											Вход
										</Link>
									</NavItem>
								)}
								{identity.isAuthenticated && (
									<UncontrolledDropdown nav inNavbar>
										<DropdownToggle nav caret>
											<User
												name={identity.currentUser!.fullName}
												pictureUrl={identity.currentUser!.pictureUrl}
											/>
										</DropdownToggle>
										<DropdownMenu right>
											<DropdownItem disabled onClick={this.closeMenu}>
												Профиль
											</DropdownItem>
											{identity.isCurrentUserAdmin && (
												<Link className="dropdown-item" to="/admin/" onClick={this.closeMenu}>
													Admin
												</Link>
											)}
											<DropdownItem divider />
											<Link className="dropdown-item" to="/logout/" onClick={this.closeMenu}>
												Выход
											</Link>
										</DropdownMenu>
									</UncontrolledDropdown>
								)}
							</Nav>
						</Collapse>
					</Navbar>
				</Container>
			</header>
		);
	}
}

export default Header;
