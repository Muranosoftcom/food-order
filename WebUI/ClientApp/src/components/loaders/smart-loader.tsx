import React from "react";
import { inject, observer } from "mobx-react";

import Loader from "./ellipsis-loader";
import AppStore from "../../store/app-store";

interface Props {
	appStore?: AppStore;
}

function SmartLoader({ appStore }: Props) {
	const { hasPendingTasks } = appStore!.operationManager;

	return hasPendingTasks ? <Loader /> : null;
}

export default inject("appStore")(observer(SmartLoader));
