import React from "react";
import cn from "classnames";

import Dish from "../../../../domain/dish";
import { ReactComponent as Add } from "../../../../images/add.svg";
import { ReactComponent as Cancel } from "../../../../images/cancel.svg";
import "./dish-selector.scss";

interface Props {
	className: string;
	onSelect(dish: Dish): void;
	onUnselect(dish: Dish): void;
	isSelected: boolean;
	disabled: boolean;
	dish: Dish,
}

class DishSelector extends React.Component<Props> {
	static defaultProps: Pick<Props, "className" | "isSelected" | "disabled" | "onSelect" | "onUnselect"> = {
		className: "",
		isSelected: false,
		disabled: false,
		onSelect() {},
		onUnselect() {},
	};

	private handleClick = () => {
		const { isSelected, dish, onSelect, onUnselect } = this.props;

		if (isSelected) {
			onUnselect(dish);
		} else {
			onSelect(dish);
		}
	};

	render() {
		const { className = "", dish, isSelected, disabled } = this.props;

		return (
			<div
				className={cn("dish-selector", { "dish-selector--disabled": disabled }, className)}
				onClick={this.handleClick}
			>
				<span className="dish-selector__name">{dish.name}</span>
				{dish.price > 0 && (
					<span className="dish-selector__price">
						{dish.price}
						<span className="dish-selector__money-unit">грн</span>
					</span>
				)}
				<span className="dish-selector__button">
					{isSelected ? (
						<Cancel className="dish-selector__icon dish-selector__icon--cancel" />
					) : (
						<Add className="dish-selector__icon dish-selector__icon--add" />
					)}
				</span>
			</div>
		);
	}
}

export default DishSelector;
