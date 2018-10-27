import React from "react";
import "./loader.scss";

const Loader = () => (
	<div className="loader__overlay">
		<div className="loader">
			<div className="loader__plate loader__plate--first" />
			<div className="loader__plate loader__plate--second" />
			<div className="loader__plate loader__plate--third" />
		</div>
	</div>
);

export default Loader;
