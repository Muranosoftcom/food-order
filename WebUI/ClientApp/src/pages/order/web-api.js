import axios from "axios";

export async function getWeekMenu() {
	return (await axios.get("/api/order/get-week-menu")).data;
}

export function postOrder(ids) {
	return axios({
		method: "POST",
		url: "/api/order/post-order",
		data: ids.map(({ id }) => id),
	});
}
