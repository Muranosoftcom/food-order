import AppModel from "../models/app";

import MenuService from "../services/menu-service";
import OrderService from "../services/order-service";
import AuthService from "../services/auth-service";
import CalendarService from "../services/calendar-service";
import MenuEditorService from "../services/menu-editor-service";

import HomePageStore from "../pages/home/store/home-page-store";
import OrderPageStore from "../pages/order/store/order-page-store";
import WeekOrderPageStore from "../pages/week-order/store/week-order-page-store";
import AdminPageStore from "../pages/admin/store/admin-page-store";
import MenuEditorPageStore from "../pages/admin/menu-editor/store/menu-editor-page-store";
import LoginPageStore from "../pages/login/store/store";
import MyOrderStore from "./my-order-store";
import IdentityStore from "./identity-store";
import AppStore from "./app-store";

interface Services {
	menuService: MenuService;
	orderService: OrderService;
	authService: AuthService;
	calendarService: CalendarService;
	menuEditorService: MenuEditorService;
}

function createStores({ menuService, orderService, authService, calendarService, menuEditorService }: Services) {
	const appModel = new AppModel({ calendarService, menuService, orderService });
	const identityStore = new IdentityStore(authService);

	const appStore = new AppStore({ appModel, identityStore });

	const myOrderStore = new MyOrderStore(appModel, appStore);
	const homePageStore = new HomePageStore(appStore, appModel);
	const orderPageStore = new OrderPageStore(appStore, appModel);
	const weekOrderPageStore = new WeekOrderPageStore(appStore, appModel);
	const loginPageStore = new LoginPageStore(appStore);
	const adminPageStore = new AdminPageStore(appStore);
	const menuEditorPageStore = new MenuEditorPageStore(appStore, menuEditorService);

	return { appStore, homePageStore, orderPageStore, weekOrderPageStore, loginPageStore, myOrderStore, adminPageStore, menuEditorPageStore };
}

export { createStores };
