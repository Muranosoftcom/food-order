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
			<li className="dish__item">
				dish.name <span>{dish.price}</span>
				<Button color={isSelected ? "success" : "secondary"} onClick={this.handleSelect}>
					{isSelected ? "-" : "+"}
				</Button>
			</li>
		);
	}
}

export default DishView;
