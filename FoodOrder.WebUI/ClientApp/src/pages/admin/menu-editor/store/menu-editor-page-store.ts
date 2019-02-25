import PageStore from "../../../../store/page-store";
import AppStore from "../../../../store/app-store";
import MenuEditorService from "../../../../services/menu-editor-service";
import DishStore from "./dish-store";
import DishCategoryStore from "./dish-category-store";
import SupplierStore from "./supplier-store";

class MenuEditorPageStore extends PageStore {
	public supplierStore: SupplierStore;
	public dishCategoryStore: DishCategoryStore;
	public dishStore: DishStore;

	constructor(appStore: AppStore, readonly menuEditorService: MenuEditorService) {
		super(appStore);

		this.supplierStore = new SupplierStore(this, menuEditorService);
		this.dishCategoryStore = new DishCategoryStore(this, menuEditorService);
		this.dishStore = new DishStore(this, menuEditorService);
	}

	get suppliers() {
		return this.supplierStore.suppliers;
	}

	public submitMenuEditing() {
		alert("submitMenuEditing");
	}

	async performUpdate(callback: () => Promise<any>) {
		await this.appStore.operationManager.runWithProgress(() => callback());

		this.supplierStore.fetchAllSuppliers();
	}
}

export default MenuEditorPageStore;