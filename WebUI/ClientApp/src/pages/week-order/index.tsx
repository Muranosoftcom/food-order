import React, { Component } from "react";
import { inject, observer } from "mobx-react";

import visibleOnlyForAuthenticatedUser from "../../hocs/visible-only-for-authenticated-user";
import WeekOrderPageStore from "./store/week-order-page-store";
import MyOrders from "./components/my-orders";

interface Props {
	weekOrderPageStore?: WeekOrderPageStore;
}

@inject("weekOrderPageStore")
@observer
class MyWeekOrder extends Component<Props> {
	public componentDidMount(): void {
		this.props.weekOrderPageStore!.fetchOrders();
	}

	render() {
		const { orders, nextWeekWorkingDays } = this.props.weekOrderPageStore!;

		return (
			<section>
				<h1 className="page-heading">Мой заказ на следующую неделю</h1>
				<MyOrders orders={orders} days={nextWeekWorkingDays} />
			</section>
		);
	}
}

// @ts-ignore
export default visibleOnlyForAuthenticatedUser(MyWeekOrder);
