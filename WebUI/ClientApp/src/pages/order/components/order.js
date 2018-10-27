import React from "react";
import { Button, Col, Container, Row, Alert } from "reactstrap";
import DishView from "./dish-view";

function trancformDays(day) {
	if (day === "Mon") {
		return "Понедельник";
	}
	if (day === "Tue") {
		return "Вторник";
	}

	if (day === "Wed") {
		return "Среда";
	}

	if (day === "Thu") {
		return "Четверг";
	}

	if (day === "Fri") {
		return "Пятница";
	}

	return "Выходной";
}

export default class OrderPage extends React.Component {
	state = {
		loading: false,
		weekDay: null,
		selectedDishes: [],
		moneyCounter: 51,
	};

	async componentDidMount() {
		this.setState({
			loading: true,
		});

		try {
			const data = await this.props.onLoadData();
			const [weekDay] = data.weekDays;

			console.log(data.weekDays);

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

	get money() {
		return (
			this.state.moneyCounter -
			this.state.selectedDishes.reduce((acc, dish) => {
				return acc + dish.price;
			}, 0)
		);
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

	handleOrder = async () => {
		await this.props.onOrder(this.state.selectedDishes || []);
		this.setState({ selectedDishes: [] });
	};

	renderDishes(dishes, inBasket = false) {
		const { selectedDishes } = this.state;

		return dishes && dishes.length ? (
			<div className="dish">
				{dishes.map(dish => (
					<DishView
						key={dish.id}
						dish={dish}
						isSelected={inBasket /*selectedDishes.some(id => dish.id === id)*/}
						onSelect={this.handleToggle}
					/>
				))}
			</div>
		) : null;
	}

	render() {
		const { weekDay } = this.state;

		return weekDay ? (
			<Container>
				<Row>
					<Col>
						<h1 className="order-page__title">
							Заказ еды на <span className="order-page__week-name">{trancformDays(weekDay.weekDay)}</span>
						</h1>
					</Col>
				</Row>
				<Row>
					<Col md={8}>
						{weekDay.suppliers &&
							weekDay.suppliers.map(supplier => (
								<div key={supplier.supplierId}>
									<section className="provider">
										<div className="provider__title">{supplier.supplierName}</div>
										{supplier.categories.map(category => (
											<div key={category.id} className="meal-category">
												<div className="meal-category__title">{category.name}</div>
												{this.renderDishes(category.dishes)}
											</div>
										))}
									</section>
								</div>
							))}
					</Col>
					<Col sm={4} md={4} className="basket">
						<div className="price-container">
							<div className="price-container__title">Доступная сумма для заказа</div>
							<div className="price-container__price">
								<span>{this.money}</span>
								<span>грн</span>
							</div>
						</div>
						<div className="basket__list">
							{this.renderDishes(this.state.selectedDishes, true)}
							<Button
								disabled={this.state.selectedDishes.length <= 0}
								className="basket__order-button"
								color="primary"
								onClick={this.handleOrder}
							>
								Заказать
							</Button>
						</div>
					</Col>
				</Row>
			</Container>
		) : (
			<Container>
				<Row>
					<Col>
						<Alert color="warning">
							Заказ обеда пока не доступен! Ожидается обновление меню от поставщика...
						</Alert>
					</Col>
				</Row>
			</Container>
		);
	}
}
