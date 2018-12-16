import { action, computed, observable, runInAction } from "mobx";

import Day from "../domain/day";
import CalendarService from "../services/calendar-service";
import { getNextWeekWorkingDays } from "../utils/date-utils";

class CalendarModel {
	constructor(private calendarService: CalendarService) {}

	@observable
	public days: Day[] = [];

	@computed
	get nextWeekWorkingDays() {
		return getNextWeekWorkingDays(this.days);
	}

	@action
	public async fetchDays() {
		try {
			const days = await this.calendarService.getDays();

			runInAction(() => {
				this.days = days.map(d => new Day(d.shortDate, d.isHoliday));
			})
		} catch (e) {
			console.error(e)
		}
	}

	public getDay(shortDate: string) {
		return this.days.find(d => d.shortDate === shortDate);
	}
}

export default CalendarModel;
