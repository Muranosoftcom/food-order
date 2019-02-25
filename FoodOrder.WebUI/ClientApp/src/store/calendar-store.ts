import { computed } from "mobx";

import AppModel from "../models/app";

class CalendarStore {
	constructor(private appModel: AppModel) {}

	@computed
	public get days() {
		return this.appModel.calendar.days;
	}
}

export default CalendarStore;
