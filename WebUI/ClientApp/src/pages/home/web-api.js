import axios from "axios";

const API_ENDPOINT = "/api/order/get-today-order";

export async function getWeekOrder() {
	return (await axios.get(API_ENDPOINT)).data;
}
