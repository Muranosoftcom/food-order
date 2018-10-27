import React from "react";
import ReactDOM from "react-dom";

import "./index.scss";
import App from "./components/order";
import { getWeekMenu } from "./web-api";

const rootElement = document.getElementById("root");

ReactDOM.render(<App onLoadData={getWeekMenu} />, rootElement);
