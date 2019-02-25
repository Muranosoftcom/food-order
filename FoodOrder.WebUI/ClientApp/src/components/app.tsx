import React, { Component, Suspense } from "react";
import { Container } from "reactstrap";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { inject, observer } from "mobx-react";

import { config } from "../config";
import Route from "./smart-route";
import Header from "./header/header";
import Footer from "./footer/footer";
import SmartLoader from "./loaders/smart-loader";
import Loader from "./loaders/ellipsis-loader";

import AppStore from "../store/app-store";
import DevTools from "./dev-tools";

const Home = React.lazy(() => import("../pages/home/index"));
const Order = React.lazy(() => import("../pages/order/index"));
const MyOrder = React.lazy(() => import("../pages/my-order/index"));
const MyWeekOrder = React.lazy(() => import("../pages/week-order/index"));
const Login = React.lazy(() => import("../pages/login/index"));
const Logout = React.lazy(() => import("../pages/logout/logout"));
const AdminPage = React.lazy(() => import("../pages/admin/index"));
const MenuEditorPage = React.lazy(() => import("../pages/admin/menu-editor/index"));

interface Props extends RouteComponentProps {
	appStore?: AppStore;
}

const EmptyLoader = () => (<div />);

class App extends Component<Props> {
	componentDidMount() {
		this.props.appStore!.startTimeWatcher();
	}

	componentWillUnmount() {
		this.props.appStore!.stopTimeWatcher();
	}

	render() {
		const { appStore } = this.props;
		const { isAuthenticated, isCurrentUserAdmin } = appStore!.identity;

		return (
			<>
				<Header className="layout__header fixed-top"/>
				<main className="layout__main">
					<Container>
						<Route
							exact
							path="/"
							component={() => (
								<Suspense fallback={<Loader/>}>
									<Home/>
								</Suspense>
							)}
						/>
						<Route
							path="/order/"
							isPrivate
							isAuthenticated={isAuthenticated}
							component={() => (
								<Suspense fallback={<Loader/>}>
									<Order/>
								</Suspense>
							)}
						/>
						<Route
							path="/my-order/"
							isPrivate
							isAuthenticated={isAuthenticated}
							component={() => (
								<Suspense fallback={<Loader/>}>
									<MyOrder/>
								</Suspense>
							)}
						/>
						<Route
							path="/week-order/"
							isPrivate
							isAuthenticated={isAuthenticated}
							component={() => (
								<Suspense fallback={<Loader/>}>
									<MyWeekOrder/>
								</Suspense>
							)}
						/>
						<Route
							path="/login/"
							component={() => (
								<Suspense fallback={<EmptyLoader />}>
									<Login/>
								</Suspense>
							)}
						/>
						<Route
							path="/logout/"
							isAuthenticated={isAuthenticated}
							component={() => (
								<Suspense fallback={<EmptyLoader />}>
									<Logout/>
								</Suspense>
							)}
						/>
						<Route
							exact
							path="/admin/"
							isPrivate
							isAuthenticated={isAuthenticated}
							isCurrentUserAdmin={isCurrentUserAdmin}
							component={() => (
								<Suspense fallback={<Loader/>}>
									<AdminPage />
								</Suspense>
							)}
						/>
						<Route
							exact
							path="/admin/menu-editor/"
							isPrivate
							isAuthenticated={isAuthenticated}
							isCurrentUserAdmin={isCurrentUserAdmin}
							component={() => (
								<Suspense fallback={<Loader/>}>
									<MenuEditorPage />
								</Suspense>
							)}
						/>
					</Container>
				</main>
				<Footer className="layout__footer"/>
				<SmartLoader/>
				{!config.isProduction && <DevTools />}
			</>
		);
	}
}

export default inject("appStore")(withRouter(observer(App)));
