import React from "react";

import "./user.scss";

interface Props {
	name: string;
	pictureUrl: string;
}

const User: React.FC<Props> = ({ name, pictureUrl }) => {
	return (
		<div className="user">
			<img className="user__logo" alt="name" src={pictureUrl} />
			<span className="user__name">{name}</span>
		</div>
	);
};

export default User;
