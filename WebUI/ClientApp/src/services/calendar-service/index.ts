import axios from "axios";

import Day from "../../domain/day";

class CalendarService {
	constructor(private api: string) {
		this.api = api + "/calendar/"
	}

	public async getDays(): Promise<Day[]> {
		const url = this.api + "/next-two-weeks/";
		return (await axios.get(url)).data;
	}
}

export default CalendarService;
