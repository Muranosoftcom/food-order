import axios from "axios";

const API_ENDPOINT = "/api/order/get-week-menu";

export async function getWeekMenu() {
	return (await axios.get(API_ENDPOINT)).data;
}