import React, { ComponentType } from "react";
import { Redirect, Route, RouteProps } from "react-router-dom";

interface Props extends RouteProps {
	component: ComponentType;
	isAuthenticated?: boolean;
	isPrivate?: boolean;
	isCurrentUserAdmin?: boolean;
	onlyForAdmins?: boolean;
	path: string;
}

const SmartRoute: React.FC<Props> = ({
	component: Component,
	isAuthenticated = false,
	isCurrentUserAdmin = false,
	isPrivate = false,
	onlyForAdmins = false,
	...rest
}) => {
	return (
		<Route
			{...rest}
			render={props => {
				if (onlyForAdmins && !isCurrentUserAdmin) {
					return (
						<Redirect
							to={{
								pathname: "/",
							}}
						/>
					);
				}

				if (isPrivate && !isAuthenticated) {
					return (
						<Redirect
							to={{
								pathname: "/login",
								state: { from: props.location },
							}}
						/>
					);
				}

				return <Component />;
			}}
		/>
	);
};

export default SmartRoute;
