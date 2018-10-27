import React from "react";
import Loader from "../../../common/components/loader";
import AllTableData from "../components/week-order-table";
import { Container, Col, Row } from "reactstrap";

class App extends React.Component {
	state = {
		loading: false,
		weekDaysOrder: [],
	};

	async componentDidMount() {
		this.setState({
			loading: true,
		});

		try {
			const data = await this.props.onLoadData();
			console.log(data);

			this.setState({
				weekDaysOrder: data.weekDays,
				loading: false,
			});
		} catch (e) {
			console.error(e.message());
			this.setState({
				loading: false,
			});
		}
	}

	renderData() {
		return this.state.weekDaysOrder ? <AllTableData weekDaysOrders={this.state.weekDaysOrder} /> : null;
	}

	render() {
		return (
			<Container className="home-page">
				<Row>
					<Col>
						<h1 className="home-page__title">Заказ обедов на сегодня</h1>
					</Col>
				</Row>
				{this.state.loading ? <Loader /> : this.renderData()}
			</Container>
		);
	}
}

export default App;
