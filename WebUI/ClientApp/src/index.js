import React from "react";
import ReactDOM from "react-dom";
import { HashRouter as Router } from "react-router-dom";

import { configure } from "mobx";
import { Provider } from "mobx-react";

import "./index.scss";
import ScrollToTop from "./components/scroll-to-top";
import AppComponent from "./app";
import registerServiceWorker from "./registerServiceWorker";
import rootStore from "./store";

configure({ enforceActions: "always" });

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");

ReactDOM.render(
	<Provider rootStore={rootStore}>
		<Router basename={baseUrl}>
			<ScrollToTop>
				<AppComponent />
			</ScrollToTop>
		</Router>
	</Provider>,
	rootElement,
);

registerServiceWorker();
