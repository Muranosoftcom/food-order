import React from "react";

import "./ellipsis-loader.scss";

function EllipsisLoader() {
	return (
		<div className="ellipsis-loader-overlay">
			<div className="ellipsis-loader-wrapper">
				<div className="ellipsis-loader">
					<div className="ellipsis-loader__circle ellipsis-loader__circle--1" />
					<div className="ellipsis-loader__circle ellipsis-loader__circle--2" />
					<div className="ellipsis-loader__circle ellipsis-loader__circle--3" />
					<div className="ellipsis-loader__circle ellipsis-loader__circle--4" />
				</div>
			</div>
		</div>
	);
}

export default EllipsisLoader;
