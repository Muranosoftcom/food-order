import React from "react";

class Table extends React.Component {
	render() {
		const { data } = this.props;

		return (
			<table>
				<tbody>
					<tr>
						<th>Имя сотрудника</th>
						<th>Наименование заказа</th>
					</tr>
					{data &&
						data.map(item => (
							<tr key={item.userName}>
								<td>{item.userName}</td>
								<td>
									<ul>
										{item.dishes && item.dishes.map(dish => <li key={dish.name}>{dish.name}</li>)}
									</ul>
								</td>
							</tr>
						))}
				</tbody>
			</table>
		);
	}
}

export default Table;
