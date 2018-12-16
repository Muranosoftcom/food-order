import MenuService from "./menu-service";
import AuthService from "./auth-service";
import OrderService from "./order-service";
import CryptoService from "./crypto-service";
import CalendarService from "./calendar-service";

export function createServices(apiEndpoint: string) {
	return {
		menuService: new MenuService(apiEndpoint),
		orderService: new OrderService(apiEndpoint),
		authService: new AuthService(),
		cryptoService: new CryptoService(),
		calendarService: new CalendarService(apiEndpoint),
	};
}
