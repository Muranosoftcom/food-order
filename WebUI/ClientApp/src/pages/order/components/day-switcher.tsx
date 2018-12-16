import React from "react";
import { Nav } from "reactstrap";

import DayTab from "./day-tab";
import Day from "../../../domain/day";

interface Props {
	days: Day[];
	activeDay: Day | null;
	onSwitch(day: Day): void;
}

function DaySwitcher({ days, activeDay, onSwitch }: Props) {
	return (
		<Nav className="my-4" tabs>
			{days.map(day => (
				<DayTab key={day.shortDate} day={day} isActive={day === activeDay} onClick={onSwitch} />
			))}
		</Nav>
	);
}

export default DaySwitcher;
