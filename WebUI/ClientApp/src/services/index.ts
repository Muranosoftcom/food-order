import MenuService from "./menu-service";
import AuthService from "./auth-service";
import OrderService from "./order-service";
import CalendarService from "./calendar-service";

export function createServices(apiEndpoint: string) {
	return {
		menuService: new MenuService(apiEndpoint),
		orderService: new OrderService(apiEndpoint),
		authService: new AuthService(),
		calendarService: new CalendarService(apiEndpoint),
	};
}
