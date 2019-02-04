import { computed } from "mobx";

import AppViewModel from "../models/app";

class CalendarStore {
	constructor(private appModel: AppViewModel) {}

	@computed
	public get days() {
		return this.appModel.calendar.days;
	}
}

export default CalendarStore;
