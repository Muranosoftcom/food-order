import React, { Component } from "react";
import { inject } from "mobx-react";

import Expander from "../../../../../components/expander/expander";
import { AddNewItem } from "../add-new-item/add-new-item";
import { EditButton } from "../edit-button/edit-button";
import { DeleteButton } from "../delete-button/delete-button";
import { DishView } from "../dish-view/dish-view";
import { DishCategoryDto } from "../../../../../domain/dto";
import DishCategoryStore from "../../store/dish-category-store";
import DishStore from "../../store/dish-store";

import "./category-view.scss";

interface Props {
	category: DishCategoryDto;
	dishCategoryStore?: DishCategoryStore;
	dishStore?: DishStore
}

@inject(({ menuEditorPageStore }) => ({
	dishCategoryStore: menuEditorPageStore.dishCategoryStore,
	dishStore: menuEditorPageStore.dishStore,
}))
export class CategoryView extends Component<Props> {
	private handleAddNewDish = () => {
		const { dishStore, category } = this.props;

		dishStore!.addNewDish(category.id);
	};

	private handleEdit = () => {
		const { category, dishCategoryStore } = this.props;
		const categoryName = (prompt("Название категории:", category.name) || "").trim();

		if (categoryName) {
			dishCategoryStore!.update({
				...category,
				name: categoryName,
			});
		}
	};

	private handleDelete = () => {
		this.props.dishCategoryStore!.delete(this.props.category.id);
	};

	public render() {
		const { category } = this.props;

		return (
			<Expander
				className="category"
				headerClassName="category__header"
				caption={
					<>
						<span className="category__name">{category.name}</span>
						<EditButton onClick={this.handleEdit} />
						<DeleteButton
							confirmMessage="Вы точно хотите удалить Категорию?"
							onDelete={this.handleDelete}
						/>
					</>
				}
			>
				<div className="category__dishes">
					{category.dishes.map(dish => (
						<DishView key={dish.id} dish={dish} categoryId={category.id} />
					))}
				</div>
				<div className="category__add-new">
					<AddNewItem onClick={this.handleAddNewDish}>
						Добавить блюдо
					</AddNewItem>
				</div>
			</Expander>
		);
	}
}
