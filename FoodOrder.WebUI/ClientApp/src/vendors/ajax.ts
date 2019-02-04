import axios from "axios";
import { LOCAL_STORAGE_KEY } from "../services/auth-service";

export default function() {
	const token = window.localStorage.getItem(LOCAL_STORAGE_KEY);

	return axios.create({
		headers: {
			Authorization: `Bearer ${token}`,
			"Content-Type": "application/json",
		},
	});
}
