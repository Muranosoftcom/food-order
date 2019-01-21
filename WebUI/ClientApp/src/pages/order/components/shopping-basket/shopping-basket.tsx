import React from "react";
import { Button } from "reactstrap";

import Dish from "../../../../domain/dish";
import DishSelector from "../dish-selector/dish-selector";
import { ReactComponent as ShoppingCart } from "../../../../images/shopping-cart.svg";
import "./shopping-basket.scss";

function calcOverpayment(payment: number, availableMoneyToOrder: number) {
	const overpayment = payment - availableMoneyToOrder;

	return overpayment > 0 ? overpayment : 0;
}

interface Props {
	availableMoneyToOrder: number;
	dishes: Dish[];
	onUnselectDish(dish: Dish): void;
	onMakeOrder(): void;
}

function ShoppingBasket({ dishes, onUnselectDish, onMakeOrder, availableMoneyToOrder = 51 }: Props) {
	const payment = dishes.reduce((acc, dish) => acc + dish.price, 0);
	const overpayment = calcOverpayment(payment, availableMoneyToOrder);

	return (
		<div className="shopping-basket border">
			<h3 className="shopping-basket__title text-center">Сумма заказа</h3>
			<div className="shopping-basket__payment text-center">
				<div className="shopping-basket__payment-value">
					<span>{payment}</span>
					<span className="shopping-basket__payment-money-unit">грн</span>
				</div>
				{overpayment > 0 && (
					<div className="overpayment text-danger">
						<span className="overpayment__title">Переплата</span>
						{overpayment}
						<span className="overpayment__money-unit">грн</span>
					</div>
				)}
			</div>
			<div className="shopping-basket__dishes">
				{dishes.map(dish => {
					return <DishSelector key={dish.id} isSelected dish={dish} onUnselect={onUnselectDish}/>;
				})}
			</div>
			{dishes.length > 0 && (
				<div className="text-right">
					<Button className="shopping-basket__order-button" color="primary" onClick={onMakeOrder}>
						Заказать
						<ShoppingCart className="shopping-basket__order-button-icon"/>
					</Button>
				</div>
			)}
		</div>
	);
}

export default ShoppingBasket;
