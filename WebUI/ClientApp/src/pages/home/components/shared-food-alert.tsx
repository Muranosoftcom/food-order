import React from "react";
import { Table, Alert, Badge, Collapse } from "reactstrap";

import { ReactComponent as ChevronDown } from "../../../images/chevron-down.svg";
import TakeFoodButton from "./take-food-button";
import DishView from "./dish-view";
import Dish from "../../../domain/dish";
import UserOrder from "../../../domain/user-order";

interface Props {
	sharedTodayOrders: UserOrder[];
}

interface State {
	expanded: boolean;
}

class SharedFoodAlert extends React.Component<Props, State> {
	state: State = {
		expanded: false,
	};

	private toggle = () => {
		this.setState(prevState => ({
			expanded: !prevState.expanded,
		}));
	};

	public render() {
		const { sharedTodayOrders } = this.props;
		const orderCount = sharedTodayOrders.length;

		return (
			<Alert color="warning">
				<div>Внимание! Сегодня есть еда в общем доступе!</div>
				<div>
					Количество расшаренных обедов{" "}
					<Badge color="light" pill>
						{orderCount}
					</Badge>
				</div>
				<div>
					<span className="alert-link caret" onClick={this.toggle}>
						Подробнее
						<ChevronDown />
					</span>
				</div>
				<Collapse isOpen={this.state.expanded}>
					<Table className="mt-4" bordered responsive size="sm">
						<thead>
							<tr>
								<th>Имя</th>
								<th>Заказ</th>
								<th />
							</tr>
						</thead>
						<tbody>
							{sharedTodayOrders.map(({ user, order }) => (
								<tr key={user.fullName}>
									<td>{user.fullName}</td>
									<td>
										{order && (
											<>
												<div className="today-orders-table__supplier-name px-2">
													<strong>{order.supplierName}</strong>
												</div>
												{order.dishes.length > 0 &&
													order.dishes.map((dish: Dish) => <DishView key={dish.id} dish={dish} />)}
											</>
										)}
									</td>
									<td className="text-right">
										<TakeFoodButton name={user.fullName} />
									</td>
								</tr>
							))}
						</tbody>
					</Table>
				</Collapse>
			</Alert>
		);
	}
}

export default SharedFoodAlert;
