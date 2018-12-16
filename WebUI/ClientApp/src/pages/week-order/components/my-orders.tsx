import React from "react";
import { Table } from "reactstrap";
import { observer } from "mobx-react";

import Day from "../../../domain/day";
import UserOrder from "../../../domain/user-order";
import { translateDayName } from "../../../utils/translate-day-name";
import SupplierBadge from "../../../components/supplier-badge/supplier-badge";
import "./my-orders.scss";

interface Props {
	orders: UserOrder[];
	days: Day[];
}

const MyOrders: React.FC<Props> = ({ orders, days }) => {
	const hasOrders = orders.length > 0;

	return (
		<Table className="my-orders" responsive bordered size="sm">
			<thead>
				<tr>
					{days.map(({ shortName }) => (
						<th key={shortName}>{translateDayName(shortName)}</th>
					))}
				</tr>
			</thead>
			{hasOrders && (
				<tbody>
					<tr>
						{days.map(({ shortDate }) => {
							const userOrder = orders.find(o => o.day.shortDate === shortDate);

							return (
								<td key={shortDate}>
									{userOrder && (
										<div>
											<SupplierBadge supplierName={userOrder.order.supplierName} />

											{userOrder.order.dishes.length > 0 &&
												userOrder.order.dishes.map(dish => (
													<div className="my-orders__dish-view" key={dish.id}>
														{dish.name}
													</div>
												))}
										</div>
									)}
								</td>
							);
						})}
					</tr>
				</tbody>
			)}
		</Table>
	);
};

export default observer(MyOrders);
