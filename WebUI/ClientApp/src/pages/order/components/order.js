import React from "react";
import { Button, Col, Container, Row } from "reactstrap";
import DishView from "./dish-view";

export default class OrderPage extends React.Component {
	state = {
		loading: false,
		weekDay: null,
		selectedDishes: [],
	};

	async componentDidMount() {
		this.setState({
			loading: true,
		});

		try {
			const data = await this.props.onLoadData();
			console.log(data);
			console.log(data.weekDays);

			const [weekDay] = data.weekDays;

			this.setState({
				weekDay,
				loading: false,
			});
		} catch (e) {
			// console.error(e);
			this.setState({
				loading: false,
			});
		}
	}

	handleToggle = dish => {
		this.setState(prevState => {
			return prevState.selectedDishes.some(selectedDish => dish.id === selectedDish.id)
				? {
						selectedDishes: prevState.selectedDishes.filter(selectedDish => selectedDish.id !== dish.id),
				  }
				: {
						selectedDishes: [...prevState.selectedDishes, dish],
				  };
		});
	};

	renderDishes(dishes) {
		const { selectedDishes } = this.state;

		return dishes && dishes.length ? (
			<ul className="dish">
				{dishes.map(dish => (
					<DishView
						key={dish.id}
						dish={dish}
						isSelected={selectedDishes.some(id => dish.id === id)}
						onSelect={this.handleToggle}
					/>
				))}
			</ul>
		) : null;
	}

	render() {
		const { weekDay } = this.state;

		return weekDay ? (
			<Container>
				<Row>
					<Col>
						<h1>
							Заказ еды на <span>{weekDay.name}</span>
						</h1>
					</Col>
				</Row>
				{weekDay.suppliers &&
					weekDay.suppliers.map(supplier => (
						<Row key={supplier.supplierId}>
							<Col sm={4} md={8}>
								<section className="provider">
									<div className="provider__title">{supplier.supplierName}</div>
									{supplier.categories.map(category => (
										<div key={category.id} className="meal-category">
											<div className="meal-category__title">{category.name}</div>
											{this.renderDishes(category.dishes)}
										</div>
									))}
								</section>
							</Col>
							<Col sm={4} md={4}>
								<div className="price-container">
									<div className="price-container__title">Доступная сумма</div>
									<div className="price-container__title">
										<span>51</span>
										<span>грн</span>
									</div>
								</div>
								<div className="basket">
									{this.renderDishes(this.state.selectedDishes)}
									<Button color="primary">Заказать</Button>
								</div>
							</Col>
						</Row>
					))}
			</Container>
		) : null;
	}
}
