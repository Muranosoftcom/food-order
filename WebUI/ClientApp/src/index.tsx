import React from "react";
import ReactDOM from "react-dom";
import { configure } from "mobx";
import { Provider } from "mobx-react";
import { BrowserRouter } from "react-router-dom";

import ScrollToTop from "./components/scroll-to-top";
import App from "./components/app";
import { createStores } from "./store";
import { createServices } from "./services";
import { config } from "./config";
import "./vendors/index";
import "./index.scss";

configure({ enforceActions: "always" });

const services = createServices(`${config.backendDomain}api`);
const stores = createStores(services);

const rootElement = document.getElementById("root");

ReactDOM.render(
	<Provider {...stores}>
		<BrowserRouter>
			<ScrollToTop>
				<App />
			</ScrollToTop>
		</BrowserRouter>
	</Provider>,
	rootElement,
);
