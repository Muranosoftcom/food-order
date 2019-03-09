import React from "react";

import Dish from "../../../domain/dish";
import DishSelector from "./dish-selector/dish-selector";

interface Props {
	dishes: Dish[];
	selectedDishes: Dish[];
	canMultiSelect: boolean;
	onSelect(dish: Dish): void;
	onUnselect(dish: Dish): void;
}

class DishCollection extends React.Component<Props> {
	static defaultProps: Pick<Props, "canMultiSelect"> = {
		canMultiSelect: true,
	};

	private get onlySingleSelect() {
		return !this.props.canMultiSelect;
	}

	private get isSomeDishSelected() {
		return this.props.dishes.some(dish => this.props.selectedDishes.includes(dish));
	}

	private handleSelectDish = (dish: Dish) => {
		if (this.onlySingleSelect && this.isSomeDishSelected) {
			alert("Множественный выбор для этого поставщика запрещен!!!");
			return;
		}

		this.props.onSelect(dish);
	};

	public render() {
		const { dishes, selectedDishes, onUnselect } = this.props;

		return dishes.map(dish => {
			const isSelected = selectedDishes.some(({ id }) => id === dish.id);

			return (
				<DishSelector
					key={dish.id}
					dish={dish}
					isSelected={isSelected}
					disabled={this.onlySingleSelect && this.isSomeDishSelected && !isSelected}
					onSelect={this.handleSelectDish}
					onUnselect={onUnselect}
				/>
			);
		});
	}
}
export default DishCollection;
