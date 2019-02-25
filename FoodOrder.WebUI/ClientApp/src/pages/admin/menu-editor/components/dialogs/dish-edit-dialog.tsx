import React, { FormEvent } from "react";
import { observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import {
	Button,
	CustomInput,
	Form,
	FormGroup,
	Input,
	Label,
	Modal,
	ModalBody,
	ModalFooter,
	ModalHeader,
} from "reactstrap";
import { DishDto } from "../../../../../domain/dto";
import { DayOfWeek } from "../../../../../domain/day-of-week";

interface Props {
	dish: DishDto;
	onSave(dish: DishDto): void;
	onCancel(): void;
}

@observer
class DishEditDialog extends React.Component<Props> {
	@observable dish: DishDto = this.props.dish;

	@observable isMondaySelected = this.props.dish.availableAt.includes(DayOfWeek.Monday);
	@observable isTuesdaySelected = this.props.dish.availableAt.includes(DayOfWeek.Tuesday);
	@observable isWednesdaySelected = this.props.dish.availableAt.includes(DayOfWeek.Wednesday);
	@observable isThursdaySelected = this.props.dish.availableAt.includes(DayOfWeek.Thursday);
	@observable isFridaySelected = this.props.dish.availableAt.includes(DayOfWeek.Friday);

	private handleClose = () => {
		this.props.onCancel();
	};

	private handleSave = () => {
		runInAction(() => {
			this.dish.name = this.dish.name.trim();
			this.dish.availableAt = this.dishAvailableAt;
		});

		this.props.onSave(this.dish)
	};

	private handleSubmit(e: FormEvent) {
		e.preventDefault();
	}

	private get dishAvailableAt() {
		const result = [];

		if(this.isMondaySelected) {
			result.push(DayOfWeek.Monday)
		}

		if(this.isTuesdaySelected) {
			result.push(DayOfWeek.Tuesday)
		}

		if(this.isWednesdaySelected) {
			result.push(DayOfWeek.Wednesday)
		}

		if(this.isThursdaySelected) {
			result.push(DayOfWeek.Thursday)
		}

		if(this.isFridaySelected) {
			result.push(DayOfWeek.Friday)
		}

		return result;
	}

	private renderDaySelector() {
		return (
			<FormGroup>
				<Label for="exampleCheckbox">Блюдо доступно</Label>
				<div id="exampleCustomSwitch">
					<CustomInput
						type="checkbox" id="exampleCustomSwitch1" label="Понедельник"
						checked={this.isMondaySelected}
						onChange={event => {
							runInAction(() => {
								this.isMondaySelected = Boolean(event.target.checked);
							});
						}}
					/>
					<CustomInput
						type="checkbox" id="exampleCustomSwitch2" label="Вторник"
						checked={this.isTuesdaySelected}
						onChange={event => {
							runInAction(() => {
								this.isTuesdaySelected = Boolean(event.target.checked);
							});
						}}
					/>
					<CustomInput
						type="checkbox" id="exampleCustomSwitch3" label="Среда"
						checked={this.isWednesdaySelected}
						onChange={event => {
							runInAction(() => {
								this.isWednesdaySelected = Boolean(event.target.checked);
							});
						}}
					/>
					<CustomInput
						type="checkbox" id="exampleCustomSwitch4" label="Четверг"
						checked={this.isThursdaySelected}
						onChange={event => {
							runInAction(() => {
								this.isThursdaySelected = Boolean(event.target.checked);
							});
						}}
					/>
					<CustomInput
						type="checkbox" id="exampleCustomSwitch5" label="Пятница"
						checked={this.isFridaySelected}
						onChange={event => {
							runInAction(() => {
								this.isFridaySelected = Boolean(event.target.checked);
							});
						}}
					/>
				</div>
			</FormGroup>
		)
	}

	public render() {
		return (
			<Modal isOpen>
				<ModalHeader>Данные о поставщике</ModalHeader>
				<ModalBody>
					<Form onSubmit={this.handleSubmit}>
						<FormGroup>
							<Label for="name">Название блюда</Label>
							<Input
								type="text" name="name" id="name"
								value={this.dish.name}
								onChange={event => {
								   runInAction(() => {
									   this.dish.name = event.target.value;
								   });
							   }}
							/>
						</FormGroup>
						<FormGroup>
							<Label for="price">Цена, грн</Label>
							<Input
								type="number" name="price" id="price"
								value={this.dish.price}
								onChange={event => {
									runInAction(() => {
										this.dish.price = Number(event.target.value);
									});
								}}
							/>
						</FormGroup>
						{this.renderDaySelector()}
					</Form>
				</ModalBody>
				<ModalFooter>
					<Button color="primary" onClick={this.handleSave}>
						Сохранить
					</Button>
					<Button color="secondary" onClick={this.handleClose}>
						Отмена
					</Button>
				</ModalFooter>
			</Modal>
		);
	}
}

export default DishEditDialog;
