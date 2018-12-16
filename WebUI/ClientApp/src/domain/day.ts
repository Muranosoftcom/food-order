import { getDayShortName, fromShortDate } from "../utils/date-utils";

class Day {
	constructor(
		public shortDate: string = "",
		public isHoliday: boolean = false) {
	}

	public get shortName() {
		return getDayShortName(this.shortDate)
	}

	public toMoment() {
		return fromShortDate(this.shortDate);
	}
}

export default Day;
