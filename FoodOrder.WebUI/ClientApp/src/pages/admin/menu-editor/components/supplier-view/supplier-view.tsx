import React, { Component } from "react";
import { inject } from "mobx-react";

import Expander from "../../../../../components/expander/expander";
import { AddNewItem } from "../add-new-item/add-new-item";
import { CategoryView } from "../category-view/category-view";
import { EditButton } from "../edit-button/edit-button";
import { DeleteButton } from "../delete-button/delete-button";
import { SupplierDto } from "../../../../../domain/dto";
import DishCategoryStore from "../../store/dish-category-store";

import "./supplier-view.scss";

interface Props {
	dishCategoryStore?: DishCategoryStore;
	supplier: SupplierDto;
	selectedSupplier: SupplierDto | null;
	onEdit(supplier: SupplierDto): void;
	onDelete(supplierId: string): void;
}

@inject(({ menuEditorPageStore }) => ({
	dishCategoryStore: menuEditorPageStore.dishCategoryStore
}))
export class SupplierView extends Component<Props> {
	private handleEdit = () => {
		this.props.onEdit(this.props.supplier);
	};

	private handleDelete = () => {
		this.props.onDelete(this.props.supplier.supplierId);
	};

	private handleAddCategory = () => {
		const categoryName = (prompt("Название категории:") || "").trim();
		const { supplier, dishCategoryStore } = this.props;

		if (categoryName) {
			dishCategoryStore!.createNew(categoryName.trim(), supplier.supplierId);
		}
	};

	render() {
		const { supplier, selectedSupplier } = this.props;
		const { supplierName, supplierId, categories } = supplier;

		return (
			<Expander
				className="supplier"
				headerClassName="supplier__header"
				caption={
					<>
						<span className="supplier__name">{supplierName}</span>
						<EditButton onClick={this.handleEdit}>
							{selectedSupplier && supplierId === selectedSupplier.supplierId ? "x" : "edit"}
						</EditButton>
						<DeleteButton
							confirmMessage="Вы точно хотите удалить Поставщика?"
							onDelete={this.handleDelete}
						/>
					</>
				}
			>
				<div className="supplier__categories">
					{categories.map(category => (
						<CategoryView key={category.id} category={category} />
					))}
				</div>
				<div className="supplier__add-new">
					<AddNewItem onClick={this.handleAddCategory}>Добавить категорию</AddNewItem>
				</div>
			</Expander>
		);
	}
}
