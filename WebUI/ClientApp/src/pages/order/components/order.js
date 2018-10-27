import React from "react";
import { Button, Col, Container, Row } from "reactstrap";
import DishView from "./dish-view";

function trancformDays(day) {
	if (day === "Mon") {
		return "Понедельник";
	}
	if (day === "Tue") {
		return "Вторник";
	}
}

export default class OrderPage extends React.Component {
	state = {
		loading: false,
		weekDay: null,
		selectedDishes: [],
		money: 51,
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

	handleOrder = () => {
		this.props.onOrder(this.state.selectedDishes || []);
	};

	renderDishes(dishes) {
		const { selectedDishes } = this.state;

		return dishes && dishes.length ? (
			<div className="dish">
				{dishes.map(dish => (
					<DishView
						key={dish.id}
						dish={dish}
						isSelected={selectedDishes.some(id => dish.id === id)}
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
							Заказ еды на <span>{trancformDays(weekDay.weekDay)}</span>
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
								<span>51</span>
								<span>грн</span>
							</div>
						</div>
						<div className="basket__list">
							{this.renderDishes(this.state.selectedDishes)}
							<Button disabled={this.state.selectedDishes.length <= 0} className="basket__order-button" color="primary" onClick={this.handleOrder}>
								Заказать
							</Button>
						</div>
					</Col>
				</Row>
			</Container>
		) : null;
	}
}
