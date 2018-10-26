import React, { Component } from "react";

class FetchData extends Component {
	constructor(props) {
		super(props);
		this.state = { ordersByUser: [], loading: true };

		fetch("api/SampleData/WeatherForecasts")
			.then(response => response.json())
			.then(data => {
				this.setState({ forecasts: data, loading: false });
			});
	}

	static renderAllOrdersTable(ordersByUser) {
		return (
			<table className="table">
				<thead>
					<tr>
						<th>Пользователь</th>
						<th>Дни недели</th>
					</tr>
				</thead>
				<tbody>
					{ordersByUser.map(orderByUser => (
						<tr key={orderByUser.name}>
							<td>{orderByUser.name}</td>
							<td>{orderByUser.order}</td>
						</tr>
					))}
				</tbody>
			</table>
		);
	}

	render() {
		let contents = this.state.loading ? (
			<p>
				<em>Loading...</em>
			</p>
		) : (
			FetchData.renderAllOrdersTable(this.state.forecasts)
		);

		return (
			<div>
				<h1>Все заказы на сегодня</h1>
				{contents}
			</div>
		);
	}
}

export default FetchData;
