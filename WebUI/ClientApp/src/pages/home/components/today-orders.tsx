import React, { FC } from "react";
import { Table } from "reactstrap";
import { inject, observer } from "mobx-react";
import cn from "classnames";

import UserOrder from "../../../domain/user-order";
import DishView from "./dish-view";
import SupplierBadge from "../../../components/supplier-badge/supplier-badge";
import "./today-orders.scss";

interface Props {
	currentUserId: string | null;
	orders: UserOrder[];
}

const TodayOrders: FC<Props> = ({ orders, currentUserId }) => {
	return orders.length > 0 ? (
		<section className="mt-4">
			<h1 className="my-2 page-heading">Перечень всех заказанных обедов на сегодня</h1>
			<Table
				className={cn("today-orders", { "today-orders--authorized": Boolean(currentUserId) })}
				responsive
				bordered
				size="sm"
			>
				<thead>
					<tr>
						<th>Имя</th>
						<th>Детали заказа</th>
					</tr>
				</thead>
				<tbody>
					{orders.map(({ user, order }) => {
						const classNames = cn("today-orders__cell", {
							"today-orders__cell--active": user.id === currentUserId,
						});

						return (
							<tr key={user.fullName}>
								<td className={classNames}>{user.fullName}</td>
								<td className={classNames}>
									{order && (
										<div className="today-orders__order-details">
											<div className="today-orders__dishes">
												{order.dishes.length > 0 &&
													order.dishes.map(dish => <DishView key={dish.id} dish={dish} />)}
											</div>
											<div className="today-orders__supplier">
												<SupplierBadge supplierName={order.supplierName} />
											</div>
										</div>
									)}
								</td>
							</tr>
						);
					})}
				</tbody>
			</Table>
		</section>
	) : null;
};

export default inject("appStore")(observer(TodayOrders));
