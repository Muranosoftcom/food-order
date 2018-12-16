export function translateDayName(dayShortName: string) {
	if (dayShortName === "Mon") {
		return "Понедельник";
	}
	if (dayShortName === "Tue") {
		return "Вторник";
	}

	if (dayShortName === "Wed") {
		return "Среда";
	}

	if (dayShortName === "Thu") {
		return "Четверг";
	}

	if (dayShortName === "Fri") {
		return "Пятница";
	}

	return "Выходной";
}
