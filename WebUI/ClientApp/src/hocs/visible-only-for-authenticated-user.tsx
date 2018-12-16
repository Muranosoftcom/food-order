import React from "react";
import { Alert } from "reactstrap";
import { inject, observer } from "mobx-react";
import { Link } from "react-router-dom";

import AppStore from "../store/app-store";

const NotAuthenticatedAlert = () => (
	<Alert className="my-3" color="danger">
		<div>Эта страница доступна только авторизированным пользователям.</div>
		<div>
			Чтобы продолжить выполните{" "}
			<Link className="btn btn-primary btn-sm" to="/login">
				Вход
			</Link>
		</div>
	</Alert>
);

interface InjectedProps {
	appStore?: AppStore;
}

function visibleOnlyForAuthenticatedUser<T extends InjectedProps>(Control: React.ComponentType<T>) {
	return inject("appStore")(observer(
		function (props: T) {
			const { identity } = props.appStore!;

			return identity.isAuthenticated
				? (<Control {...props} />)
				: (<NotAuthenticatedAlert />);
		}
	))
}

export default visibleOnlyForAuthenticatedUser;
