import React, { Component } from "react";
import { Route } from "react-router";

import Layout from "./components/layout";
import Home from "./components/home";
import FetchData from "./components/fetch-data";
import Order from "./components/order";

export default class App extends Component {
	render() {
		return (
			<Layout>
				<Route exact path="/" component={Home} />
				<Route path="/order" component={Order} />
				<Route path="/my-orders" component={Order} />
				<Route path="/fetch-data" component={FetchData} />
			</Layout>
		);
	}
}
