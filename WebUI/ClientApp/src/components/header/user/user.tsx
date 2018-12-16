import React from "react";

import { ReactComponent as UserLogo } from "../../../images/user-logo.svg";
import "./user.scss";

interface Props {
	name: string;
}

const User: React.FC<Props> = ({ name }) => {
	return (
		<div className="user">
			<UserLogo className="user__logo" />
			<span className="user__name">{name}</span>
		</div>
	);
};

export default User;
