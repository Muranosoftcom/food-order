import React from "react";
import { Container, Col, Row } from "reactstrap";

export default function() {
	return (
		<Container>
			<Row>
				<Col>
					<h1>Заказ еды на сегодня</h1>
				</Col>
			</Row>
			<Row>
				<Col>
					<section className="provider">
						<div className="provider__title">Name</div>
						<div className="meal-category">
							<div className="meal-category__title">Первые блюда</div>
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
			</Row>
		</Container>
	);
}
