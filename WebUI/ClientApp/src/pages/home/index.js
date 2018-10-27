import React from "react";
import ReactDOM from "react-dom";

import "./index.scss";
import App from "./components/app";
import { getWeekOrder } from "./web-api";

const rootElement = document.getElementById("root");

ReactDOM.render(<App onLoadData={getWeekOrder} />, rootElement);
