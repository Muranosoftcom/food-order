import axios from "axios";
import { DayMenuDto } from "../../domain/dto";

export default class MenuService {
	private api: string;

	constructor(api: string) {
		this.api = `${api}/menu/week-menu/`;
	}

	public async getWeekMenu(): Promise<DayMenuDto[]> {
		return (await axios.get(this.api)).data;
	}
}