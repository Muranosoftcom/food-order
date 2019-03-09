import { action, computed, observable, runInAction } from "mobx";
import orderBy from "lodash.orderby";

import { SupplierDto } from "../../../../domain/dto";
import MenuEditorService from "../../../../services/menu-editor-service";
import MenuEditorPageStore from "./menu-editor-page-store";


const emptySupplier: SupplierDto = {
	supplierId: "",
	supplierName: "",
	canMultiSelect: false,
	availableMoneyToOrder: 0,
	position: 0,
	categories: []
};

class SupplierStore {
	constructor(readonly menuEditor: MenuEditorPageStore, readonly menuEditorService: MenuEditorService) {
		this.fetchAllSuppliers();
	}

	@observable
	public suppliers: SupplierDto[] = [];

	@observable
	public supplierToEdit: SupplierDto | null = null;

	@computed
	public get isSupplierEditorVisible() {
		return this.supplierToEdit !== null;
	}

	public addSupplier = () => {
		this.editSupplier({...emptySupplier, position: this.suppliers.length});
	};

	public editSupplier = (supplier: SupplierDto) => {
		if (this.isSupplierEditorVisible) {
			return;
		}

		runInAction(() => {
			this.supplierToEdit = supplier;
		})
	};

	public saveSupplier = async (supplier: SupplierDto) => {
		if (supplier.supplierId) {
			await this.update(supplier);
		} else {
			await this.createNew(supplier);
		}

		this.cancelEditSupplier();
	};

	public deleteSupplier = (supplierId: string) => {
		return this.delete(supplierId);
	};

	public cancelEditSupplier = action(() => {
		this.supplierToEdit = null;
	});

	public async fetchAllSuppliers() {
		const suppliers: SupplierDto[] = await this.menuEditorService.supplier.getAll();

		runInAction(() => {
			this.suppliers = orderBy(suppliers, ["position"]);
		});

		return;
	};

	private createNew(supplier: SupplierDto) {
		return this.menuEditor.performUpdate(() => this.menuEditorService.supplier.create(supplier));
	}

	private update(supplier: SupplierDto) {
		return this.menuEditor.performUpdate(() => this.menuEditorService.supplier.update(supplier));
	}

	private delete(supplierId: string) {
		return this.menuEditor.performUpdate(() => this.menuEditorService.supplier.delete(supplierId));
	}
}

export default SupplierStore;
