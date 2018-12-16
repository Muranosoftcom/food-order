import React from "react";
import { inject, observer } from "mobx-react";

import AppStore from "../../store/app-store";
import "./index.scss";

interface Props {
	appStore?: AppStore;
}

@inject("appStore")
@observer
class DevTools extends React.Component<Props> {
	render() {
		const { appStore } = this.props;
		const format = "DD-MMM-YYYY HH:mm:ss ddd";

		return (
			<div className="dev-tools">
				<div>Time (UTC): {appStore!.appModel.timeWatcher.currentTime.format(format)}</div>
				<div>Order {appStore!.isOrderAllowed ? "is" : "is not"} allowed</div>
				<div>{appStore!.appModel.timeWatcher.currentThursday.format(format)}</div>
				<div>{appStore!.appModel.timeWatcher.currentSunday.format(format)}</div>
			</div>
		);
	}
}

export default DevTools;
