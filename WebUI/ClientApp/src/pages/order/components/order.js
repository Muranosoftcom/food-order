import React from "react";
import { Container, Col, Row, Button } from "reactstrap";

export default function() {
	return (
		<Container>
			<Row>
				<Col>
					<h1>Заказ еды на <span>Понедельник</span></h1>
				</Col>
			</Row>
			<Row>
				<Col sm={4} md={8}>
					<section className="provider">
						<div className="provider__title">Столовая №1</div>
						<div className="meal-category">
							<div className="meal-category__title">Первые блюда</div>
							<ul className="meals">
								<li className="meals__item">Food 1</li>
								<li className="meals__item">Food 2</li>
								<li className="meals__item">Food 3</li>
								<li className="meals__item">Food 4</li>
								<li className="meals__item">Food 5</li>
							</ul>

							<div className="meal-category__title">Вторые блюда</div>
							<ul className="meals">
								<li className="meals__item">Food 1</li>
								<li className="meals__item">Food 2</li>
								<li className="meals__item">Food 3</li>
								<li className="meals__item">Food 4</li>
								<li className="meals__item">Food 5</li>
							</ul>
						</div>
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
							<li>Food 1 <Button color="danger">x</Button></li>
							<li>Food 2 <Button color="danger">x</Button></li>
							<li>Food 3 <Button color="danger">x</Button></li>
							<li>Food 4 <Button color="danger">x</Button></li>
						</ul>

						<Button color="primary">Заказать</Button>
					</div>
				</Col>
			</Row>
		</Container>
	);
}
