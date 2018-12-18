import { fromShortDate, getDayName } from "../utils/date-utils";

class Day {
	constructor(
		public shortDate: string = "",
		public isHoliday: boolean = false) {
	}

	public get dayName() {
		return getDayName(this.shortDate)
	}

	public toMoment() {
		return fromShortDate(this.shortDate);
	}
}

export default Day;
