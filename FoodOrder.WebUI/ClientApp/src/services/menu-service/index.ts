import ajax from "../../vendors/ajax";
import { DayMenuDto } from "../../domain/dto";

export default class MenuService {
	private api: string;

	constructor(api: string) {
		this.api = `${api}/menu/week-menu/`;
	}

	public async getWeekMenu(): Promise<DayMenuDto[]> {
		return (await ajax().get(this.api)).data;
	}
}