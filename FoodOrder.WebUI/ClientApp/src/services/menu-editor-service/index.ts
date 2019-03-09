import ajax from "../../vendors/ajax";
import { DishCategoryDto, DishDto, SupplierDto } from "../../domain/dto";

class SupplierService {
	constructor(private readonly api: string) {
		this.api = api + "/supplier";
	}

	public async getAll() {
		return (await ajax().get(this.api)).data;
	}

	public create(supplier: SupplierDto) {
		const { supplierId, ...newSupplier } = supplier;

		return ajax().post(this.api, newSupplier);
	}

	public update(supplier: SupplierDto) {
		return ajax().put(this.api, supplier);
	}

	public async delete(supplierId: string) {
		return (await ajax().delete(this.api, { params: { supplierId } })).data;
	}
}

class CategoryService {
	constructor(private readonly api: string) {
		this.api = api + "/category";
	}

	public create(name: string, supplierId: string, position: number) {
		return ajax().post(this.api, { supplierId, name, position });
	}

	public update(category: DishCategoryDto) {
		return ajax().put(this.api, category);
	}

	public async delete(categoryId: string) {
		return (await ajax().delete(this.api, { params: { categoryId } })).data;
	}
}

class DishService {
	constructor(private readonly api: string) {
		this.api = api + "/dish";
	}

	public create(categoryId: string, dish: DishDto) {
		const { id, ...newDish } = dish;

		return ajax().post(this.api, {
			...newDish,
			categoryId
		});
	}

	public update(dish: DishDto) {
		return ajax().put(this.api, dish);
	}

	async delete(id: string) {
		return (await ajax().delete(this.api, { params: { id } })).data;
	}
}

class MenuEditorService {
	public readonly supplier: SupplierService;
	public readonly category: CategoryService;
	public readonly dish: DishService;

	constructor(private readonly api: string) {
		this.api = api + "/menu-editor";
		this.supplier = new SupplierService(this.api);
		this.category = new CategoryService(this.api);
		this.dish = new DishService(this.api);
	}
}

export default MenuEditorService;