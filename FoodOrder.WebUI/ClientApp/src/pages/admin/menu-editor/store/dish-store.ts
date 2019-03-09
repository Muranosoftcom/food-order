import { DishDto } from "../../../../domain/dto";
import MenuEditorService from "../../../../services/menu-editor-service";
import MenuEditorPageStore from "./menu-editor-page-store";
import { computed, observable, runInAction } from "mobx";
import { DayOfWeek } from "../../../../domain/day-of-week";

const emptyDish: DishDto = {
	id: "",
	name: "",
	price: 0,
	negativeReviews: 0,
	positiveReviews: 0,
	availableAt: [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday],
	categoryId: "",
};

class DishStore {
	constructor(readonly menuEditor: MenuEditorPageStore, readonly menuEditorService: MenuEditorService) {
	}

	@observable
	public dishToEdit: DishDto | null = null;

	@computed
	public get dishEditorVisible() {
		return this.dishToEdit !== null;
	}

	addNewDish = (categoryId: string) => {
		const dish = {
			...emptyDish,
		};

		this.edit(categoryId, dish);
	};

	public edit = (categoryId: string, dish: DishDto) => {
		if(this.dishEditorVisible) {
			return;
		}

		runInAction(() => {
			this.dishToEdit = {
				...dish,
				categoryId,
			};
		})
	};

	public save = async (dish: DishDto) => {
		if (dish && dish.id) {
			await this.update(dish);
		} else {
			await this.createNew(dish.categoryId, dish);
		}

		this.cancelEdit();
	};

	public cancelEdit = () => {
		runInAction(() => {
			this.dishToEdit = null;
		});
	};

	private createNew(categoryId: string, dish: DishDto) {
		return this.menuEditor.performUpdate(() => this.menuEditorService.dish.create(categoryId, dish));
	}

	private update(dish: DishDto): any {
		return this.menuEditor.performUpdate(() => this.menuEditorService.dish.update(dish));
	}

	public delete(id: string): any {
		return this.menuEditor.performUpdate(() => this.menuEditorService.dish.delete(id));
	}
}

export default DishStore;
