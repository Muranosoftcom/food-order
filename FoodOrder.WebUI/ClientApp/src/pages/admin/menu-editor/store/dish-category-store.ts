import MenuEditorService from "../../../../services/menu-editor-service";
import { DishCategoryDto } from "../../../../domain/dto";
import MenuEditorPageStore from "./menu-editor-page-store";

class DishCategoryStore {
	constructor(readonly menuEditor: MenuEditorPageStore, readonly menuEditorService: MenuEditorService) {
	}

	createNew(categoryName: string, supplierId: string) {
		return this.menuEditor.performUpdate(() => this.menuEditorService.category.create(categoryName, supplierId));
	}

	update(category: DishCategoryDto) {
		return this.menuEditor.performUpdate(() => this.menuEditorService.category.update(category));
	}

	delete(categoryId: string) {
		return this.menuEditor.performUpdate(() => this.menuEditorService.category.delete(categoryId));
	}
}

export default DishCategoryStore;
