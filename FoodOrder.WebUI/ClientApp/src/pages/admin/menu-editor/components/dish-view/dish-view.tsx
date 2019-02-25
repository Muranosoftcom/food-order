import React, { Component } from "react";
import { inject } from "mobx-react";

import { DishDto } from "../../../../../domain/dto";
import { EditButton } from "../edit-button/edit-button";
import { DeleteButton } from "../delete-button/delete-button";
import DishStore from "../../store/dish-store";

import "./dish-view.scss";
import { getShortDayName } from "../../utilities/get-short-day-name";

interface Props {
	dish: DishDto;
	categoryId: string;
	dishStore?: DishStore;
}

@inject(({ menuEditorPageStore }) => ({
	dishStore: menuEditorPageStore.dishStore,
}))
export class DishView extends Component<Props> {
	private handleEdit = () => {
		const { dish, dishStore, categoryId } = this.props;

		dishStore!.edit(categoryId, dish);
	};

	private handleDelete = () => {
		const { dishStore } = this.props;

		dishStore!.delete(this.props.dish.id);
	};

	private getAvailabilityStatus() {
		const { dish } = this.props;

		if (dish.availableAt.length > 0) {
			return dish.availableAt
				.map(getShortDayName)
				.join(" ");
		}

		return "Не доступно";
	}

	public render() {
		const { dish } = this.props;

		return (
			<div className="dish-view">
				<div className="dish-view__name">{dish.name}</div>
				<div className="dish-view__availability-status">({this.getAvailabilityStatus()})</div>
				<div>{dish.price} грн</div>
				<EditButton onClick={this.handleEdit} />
				<DeleteButton confirmMessage="Вы точно хотите удалить Блюдо?" onDelete={this.handleDelete} />
			</div>
		);
	}
}
