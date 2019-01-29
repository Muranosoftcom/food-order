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
	const hasAnyDishes = dishes.length > 0;

	return (
		<div className="shopping-basket border">
			<div className="shopping-basket__title">Ваш заказ:</div>
			{hasAnyDishes && (
				<div className="shopping-basket__dishes">
					{dishes.map(dish => {
						return <DishSelector key={dish.id} isSelected dish={dish} onUnselect={onUnselectDish} />;
					})}
				</div>
			)}
			{payment > 0 && (
				<div className="shopping-basket__payment check">
					<div className="check__title">На сумму:</div>
					<div className="check__total">
						<div className="check__total-value">
							<span>{payment}</span>
							<span className="money-unit">грн</span>
						</div>
						{overpayment > 0 && (
							<div className="check__overpayment overpayment">
								<span className="overpayment__title">(переплата)</span>
								{overpayment}
								<span className="money-unit overpayment__money-unit">грн</span>
							</div>
						)}
					</div>
				</div>
			)}
			{hasAnyDishes && (
				<div className="text-right">
					<Button color="primary" onClick={onMakeOrder}>
						Заказать
						<ShoppingCart className="shopping-basket__order-button-icon" />
					</Button>
				</div>
			)}
		</div>
	);
}

export default ShoppingBasket;
