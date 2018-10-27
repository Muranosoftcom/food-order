import React from "react";
import { Table } from "reactstrap";

class OrdersTable extends React.Component {
	render() {
		const { data } = this.props;

		return (
			<Table responsive className="">
				<thead>
					<tr>
						<th>Имя сотрудника</th>
						<th>Наименование заказа</th>
					</tr>
				</thead>
				<tbody>
					{!data || !data.length
						? null
						: data.map(item => (
								<tr key={item.userName}>
									<td>{item.userName}</td>
									<td>
										<ul>
											{item.dishes &&
												item.dishes.map(dish => <li key={dish.name}>{dish.name}</li>)}
										</ul>
									</td>
								</tr>
						  ))}
				</tbody>
			</Table>
		);
	}
}

export default OrdersTable;
