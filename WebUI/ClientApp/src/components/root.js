import React from "react";
import PropTypes from "prop-types";
import { withCookies, Cookies as CookiesTypes } from "react-cookie";
import { Provider } from "mobx-react";

import Cookies from "js-cookie";

class Root extends React.Component {
	static propTypes = {
		cookies: PropTypes.instanceOf(CookiesTypes).isRequired,
		createStore: PropTypes.func.isRequired,
	};

	constructor(props) {
		super(props);
		console.log("constructor", Cookies.get());
	}

	render() {
		const { cookies } = this.props;
		const isAuthenticated = cookies && Boolean(cookies.get(".AspNetCore.Cookies"));
		console.log("render", cookies);

		const store = this.props.createStore(isAuthenticated);

		return <Provider rootStore={store}>{this.props.children}</Provider>;
	}
}

export default withCookies(Root);
