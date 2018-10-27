import React, { Component } from "react";
import { Route } from "react-router";

import Layout from "./components/layout";
import Home from "./components/home";
import Order from "./components/order";

class App extends Component {
	componentDidMount() {}

	render() {
		return (
			<Layout>
				<Route exact path="/" component={Home} />
				<Route path="/order" component={Order} />
			</Layout>
		);
	}
}

export default App;
