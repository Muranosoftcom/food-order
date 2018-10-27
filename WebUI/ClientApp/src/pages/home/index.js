import React from "react";
import ReactDOM from "react-dom";

import "./index.scss";
import App from "./components/app";

const rootElement = document.getElementById("root");

function load() {
	return new Promise(resolve => {
		setTimeout(resolve, 1000);
	});
}

ReactDOM.render(<App onLoadData={load} />, rootElement);
