import React from "react";
import { Button } from "reactstrap";
import { withRouter, RouteComponentProps } from "react-router-dom";

interface Props {
	name: string;
	history: any;
}

class TakeFoodButton extends React.Component<Props & RouteComponentProps<{}>> {
	handleClick = () => {
		alert(
			"Вы забронировали обед пользователя " +
				this.props.name +
				"\nПожалуйста подтвердите ваши намерения на странице заказа.",
		);
		this.props.history.push("/my-order");
	};

	render() {
		return (
			<Button color="primary" onClick={this.handleClick}>
				Забрать себе
			</Button>
		);
	}
}

export default withRouter(TakeFoodButton);
