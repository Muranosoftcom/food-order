import { DayOfWeek } from "../../../../domain/day-of-week";

export function getShortDayName(day: DayOfWeek) {
	switch (day) {
		case DayOfWeek.Monday:
			return "пн";
		case DayOfWeek.Tuesday:
			return "вт";
		case DayOfWeek.Wednesday:
			return "ср";
		case DayOfWeek.Thursday:
			return "чт";
		case DayOfWeek.Friday:
			return "пт";
		case DayOfWeek.Saturday:
			return "сб";
		case DayOfWeek.Sunday:
			return "вс";
		default:
			throw new Error("Unknown DayOfWeek");
	}
}