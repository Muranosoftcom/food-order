import React from "react";
import PropTypes from "prop-types";
import { Button } from "reactstrap";

class DishView extends React.Component {
	static propTypes = {
		onSelect: PropTypes.func.isRequired,
		isSelected: PropTypes.bool.isRequired,
		dish: PropTypes.object.isRequired,
	};

	handleSelect = () => {
		this.props.onSelect(this.props.dish);
	};

	render() {
		const { dish, isSelected } = this.props;

		return (
			<div className="dish-view">
				<span className="dish-view__name">{dish.name}</span>
				<span className="dish-view__price">{dish.price && dish.price}</span>
				<Button color={isSelected ? "danger" : "secondary"} onClick={this.handleSelect}>
					{isSelected ? "-" : "+"}
				</Button>
			</div>
		);
	}
}

export default DishView;
