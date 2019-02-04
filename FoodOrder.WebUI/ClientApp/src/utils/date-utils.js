import { moment } from "../vendors";

const SHORT_DATE_FORMAT = "MM/DD/YYYY";

function trimTime(date) {
	return moment(date).set({ hour: 0, minute: 0, second: 0, millisecond: 0 });
}

export function toShortDate(date) {
	return moment(date).format(SHORT_DATE_FORMAT);
}

export function fromShortDate(shortDate) {
	return trimTime(moment(shortDate, SHORT_DATE_FORMAT));
}

export function getNextWeekWorkingDays(days) {
	const nextMonday = trimTime(
		moment()
			.add(1, "weeks")
			.isoWeekday("Monday"),
	);
	const nextFriday = trimTime(
		moment()
			.add(1, "weeks")
			.isoWeekday("Friday"),
	);

	return days.filter(day => day.toMoment().isBetween(nextMonday, nextFriday, null, "[]"));
}

export function getDayName(shortDate) {
	return fromShortDate(shortDate).format("dddd");
}

export function getDayShortName(shortDate) {
	return fromShortDate(shortDate).format("ddd");
}

export function prettifyDate(shortDate) {
	return fromShortDate(shortDate).format("D MMM");
}
