import React from "react";
import { Container, Col, Row, Button } from "reactstrap";

export default class OrderPage extends React.Component {
	state = {
		loading: false,
		weekDay: null,
	};

	async componentDidMount() {
		this.setState({
			loading: true,
		});

		try {
			const data = await this.props.onLoadData();
			console.log(data);

			this.setState({
				weekDay: data.weekDays[0],
				loading: false,
			});
		} catch (e) {
			console.error(e.message());
			this.setState({
				loading: false,
			});
		}
	}

	renderDishes(dishes) {
		return dishes && dishes.length ? (
			<ul className="meals">
				<li className="meals__item">Food 1</li>
				<li className="meals__item">Food 2</li>
				<li className="meals__item">Food 3</li>
				<li className="meals__item">Food 4</li>
				<li className="meals__item">Food 5</li>
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
									<ul>
										<li>
											Food 1 <Button color="danger">x</Button>
										</li>
										<li>
											Food 2 <Button color="danger">x</Button>
										</li>
										<li>
											Food 3 <Button color="danger">x</Button>
										</li>
										<li>
											Food 4 <Button color="danger">x</Button>
										</li>
									</ul>

									<Button color="primary">Заказать</Button>
								</div>
							</Col>
						</Row>
					))}
			</Container>
		) : null;
	}
}
