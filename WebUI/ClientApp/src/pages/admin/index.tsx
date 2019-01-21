import React, { Component } from "react";
import { Button, Col, Row } from "reactstrap";
import { inject, observer } from "mobx-react";

import AdminPageStore from "./store/admin-page-store";

interface Props {
	adminPageStore?: AdminPageStore;
}

class AdminPage extends Component<Props> {
	public handleSync = () => {
		this.props.adminPageStore!.syncMenu();
	};

	public render() {
		let { adminPageStore } = this.props;
		const { currentUser } = adminPageStore!.appStore.identity;

		return (
			currentUser && currentUser.isAdmin ?
				<>
					<Row>
						<Col>
							<h1 className="page-heading">Заказ обеда на будущую неделю</h1>
						</Col>
					</Row>
					<Row>
						<Col>
							Синхронизация меню от поставщиков из Excel
						</Col>
						<Col>
							<Button onClick={this.handleSync}>Sync menu</Button>
						</Col>
					</Row>
				</>
			: null
		);
	}
}

export default inject("adminPageStore")(observer(AdminPage));
