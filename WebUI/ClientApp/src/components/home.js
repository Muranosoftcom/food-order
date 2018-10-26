import React, { Component } from "react";
import axios from "axios";

class Home extends Component {
	handleClick = () => {
		axios.get("api/food");
	};

	render() {
		return (
			<div>
				<h1>После регистрации вы можете заказать обед!</h1>
				<button onClick={this.handleClick}>Click Me</button>
			</div>
		);
	}
}

export default Home;
