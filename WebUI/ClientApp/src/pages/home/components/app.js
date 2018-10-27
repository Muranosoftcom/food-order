import React, { Fragment } from "react";
import Loader from "../../../common/components/loader";
import AllTableData from "../components/all-orders-table";

class App extends React.Component {
	state = {
		loading: false,
		data: [],
	};

	async componentDidMount() {
		this.setState({
			loading: true,
		});

		const data = await this.props.onLoadData();

		this.setState({
			data,
			loading: false,
		});
	}

	renderData() {
		return this.state.data ? <AllTableData data={this.state.data} /> : null;
	}

	render() {
		return (
			<Fragment>
				<div className="layout__body">Заказ обедов на сегодня:</div>
				{this.state.loading ? <Loader /> : this.renderData()}
			</Fragment>
		);
	}
}

export default App;
