import React from "react";
import { Col, Row } from "reactstrap";
import { inject, observer } from "mobx-react";

import SharedFoodAlert from "./components/shared-food-alert";
import TodayOrders from "./components/today-orders";
import HomePageStore from "./store/home-page-store";

interface Props {
	homePageStore?: HomePageStore;
}

function Home({ homePageStore }: Props) {
	const { sharedTodayOrders, todayOrders, appStore } = homePageStore!;
	const hasSharedTodayOrders = sharedTodayOrders.length > 0;
	const hasTodayOrders = todayOrders.length > 0;
	const currentUserId = appStore.identity.currentUser ? appStore.identity.currentUser.id : null;

	return (
		<>
			{hasSharedTodayOrders && (
				<Row className="mt-3">
					<Col>
						<SharedFoodAlert sharedTodayOrders={sharedTodayOrders}/>
					</Col>
				</Row>
			)}
			{hasTodayOrders && <TodayOrders orders={todayOrders} currentUserId={currentUserId} />}
		</>
	);
}

export default inject("homePageStore")(observer(Home));
