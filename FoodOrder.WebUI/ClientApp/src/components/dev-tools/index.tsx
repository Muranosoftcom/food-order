import React from "react";
import { observable, action } from "mobx";
import { inject, observer } from "mobx-react";
import { Button } from "reactstrap";

import AppStore from "../../store/app-store";
import "./index.scss";

interface Props {
	appStore?: AppStore;
}

@inject("appStore")
@observer
class DevTools extends React.Component<Props> {
	@observable
	public isVisible: boolean = false;

	private toggle = action(() => {
		this.isVisible = !this.isVisible;
	});

	public render() {
		const { appStore } = this.props;
		const format = "DD-MMM-YYYY HH:mm:ss ddd";

		return this.isVisible ? (
			<div className="dev-tools">
				<Button color="danger" size="sm" className="dev-tools__close-button" onClick={this.toggle}>x</Button>
				<div>Time (UTC): {appStore!.appModel.timeWatcher.currentTime.format(format)}</div>
				<div>Order {appStore!.isOrderAllowed ? "is" : "is not"} allowed</div>
				<div>{appStore!.appModel.timeWatcher.currentThursday.format(format)}</div>
				<div>{appStore!.appModel.timeWatcher.currentSunday.format(format)}</div>
			</div>
		): (
			<div title="Open dev tools" className="dev-tools--collapsed-mode" onClick={this.toggle}>
				open
			</div>
		);
	}
}

export default DevTools;
