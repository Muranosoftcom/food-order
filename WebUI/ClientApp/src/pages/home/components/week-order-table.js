import React from "react";
import { Table } from "reactstrap";
import "./week-order-table.scss";

class WeekOrderTable extends React.Component {
	renderSupplier(supplier) {
		return [
			<li key={supplier.supplierId}>
				<span>
					<strong>{supplier.supplierName}</strong>
				</span>
			</li>,
			...supplier.categories.map(category => [
				<li key={category.id}>{category.name}</li>,
				...category.dishes.map(dish => (
					<li key={dish.id}>
						<span>{dish.name}</span>
						<span>{dish.price}</span>
					</li>
				)),
			]),
		];
	}

	render() {
		const { weekDaysOrders } = this.props;

		return (
			<Table responsive className="week-order-table">
				<thead>
					<tr>
						<th className="week-order-table__user-name">Имя сотрудника</th>
						<th>Наименование заказа</th>
					</tr>
				</thead>
				<tbody>
					{!weekDaysOrders || !weekDaysOrders.length
						? null
						: weekDaysOrders.map(weekDaysOrder => (
								<tr key={weekDaysOrder.userName}>
									<td>{weekDaysOrder.userName}</td>
									<td>
										<ul>
											{weekDaysOrder.suppliers &&
												weekDaysOrder.suppliers.map(supplier => this.renderSupplier(supplier))}
										</ul>
									</td>
								</tr>
						  ))}
				</tbody>
			</Table>
		);
	}
}

export default WeekOrderTable;
