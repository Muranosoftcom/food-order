import React from "react";
import cn from "classnames";
import { NavItem, NavLink } from "reactstrap";

import Day from "../../../../../domain/day";
import "./day-tab.scss";
import { getDayShortName, prettifyDate } from "../../../../../utils/date-utils";

interface Props {
	day: Day;
	isActive: boolean;
	onClick(day: Day): void;
}

class DayTab extends React.Component<Props> {
	private handleClick = () => {
		if (this.props.isActive) {
			return;
		}

		this.props.onClick(this.props.day);
	};

	public render() {
		const { isHoliday, shortDate } = this.props.day;
		const classNames = cn("day-tab", {
			"day-tab--holiday": isHoliday,
			"day-tab--active": this.props.isActive,
		});

		return (
			<NavItem className={classNames}>
				<NavLink onClick={this.handleClick}>
					<div className={cn("day-tab__day-name", { "day-tab__day-name--holiday": isHoliday })}>
						{getDayShortName(shortDate)}
					</div>
					<div className="day-tab__short-date">
						({prettifyDate(shortDate)})
					</div>
				</NavLink>
			</NavItem>
		);
	}
}

export default DayTab;
