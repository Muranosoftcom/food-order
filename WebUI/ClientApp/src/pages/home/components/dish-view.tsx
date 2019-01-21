import React, { FC } from "react";
import Dish from "../../../domain/dish";

import "./dish-view.scss";

interface Props {
	dish: Dish;
}

const DishView: FC<Props> = ({ dish }) => {
	return (
		<div className="dish-view">
			<span className="dish-view__name">{dish.name}</span>
		</div>
	);
};

export default DishView;
