import React from "react";
import ReactDOM from "react-dom";
import { HashRouter as Router } from "react-router-dom";

import { configure } from "mobx";

import "./index.scss";
import Root from "./components/root";
import ScrollToTop from "./components/scroll-to-top";
import AppComponent from "./app";
import registerServiceWorker from "./registerServiceWorker";
import { createStore } from "./store";

configure({ enforceActions: "always" });

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");

ReactDOM.render(
	<Root createStore={createStore}>
		<Router basename={baseUrl}>
			<ScrollToTop>
				<AppComponent />
			</ScrollToTop>
		</Router>
	</Root>,
	rootElement,
);

registerServiceWorker();
