import MenuService from "./menu-service";
import AuthService from "./auth-service";
import OrderService from "./order-service";
import CalendarService from "./calendar-service";
import MenuEditorService from "./menu-editor-service";

export function createServices(apiEndpoint: string) {
	return {
		menuService: new MenuService(apiEndpoint),
		orderService: new OrderService(apiEndpoint),
		authService: new AuthService(),
		calendarService: new CalendarService(apiEndpoint),
		menuEditorService: new MenuEditorService(apiEndpoint),
	};
}
