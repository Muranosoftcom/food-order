import moment, { Moment } from "moment";
import { action, computed, observable } from "mobx";

export default class TimeWatcher {
	private interval: any;

	constructor(private readonly tickInterval: number) {}

	@observable
	public currentTime: Moment = moment();

	start() {
		this.interval = setInterval(this.tick, 1000 * this.tickInterval);
		this.tick();
	}

	stop() {
		this.interval && clearInterval(this.interval);
	}

	@action
	tick = () => {
		this.currentTime = moment();
	};

	@computed
	get currentThursday() {
		return moment(this.currentTime).isoWeekday(4).set({ hour: 16, minute: 0, second: 0 });
	}

	@computed
	get currentSunday() {
		return moment(this.currentTime).isoWeekday(7).set({ hour: 12, minute: 0, second: 0 });
	}
}
