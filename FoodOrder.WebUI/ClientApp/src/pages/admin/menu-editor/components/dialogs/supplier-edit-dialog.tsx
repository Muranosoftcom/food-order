import React, { FormEvent } from "react";
import { observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import {
	Button,
	Modal,
	ModalHeader,
	ModalBody,
	ModalFooter,
	Input,
	Label,
	Form,
	FormGroup,
	FormText,
} from "reactstrap";
import { SupplierDto } from "../../../../../domain/dto";

interface Props {
	supplier: SupplierDto;
	onSave(supplier: SupplierDto): void;
	onCancel(): void;
}

@observer
class SupplierEditDialog extends React.Component<Props> {
	@observable supplier: SupplierDto = this.props.supplier;

	private handleClose = () => {
		this.props.onCancel();
	};

	private handleSave = () => {
		runInAction(() => {
			this.supplier.supplierName = this.supplier.supplierName.trim();
		});

		this.props.onSave(this.supplier)
	};

	private handleSubmit(e: FormEvent) {
		e.preventDefault();
	}

	public render() {
		return (
			<Modal isOpen>
				<ModalHeader>Данные о поставщике</ModalHeader>
				<ModalBody>
					<Form onSubmit={this.handleSubmit}>
						<FormGroup>
							<Label for="supplierName">Название поставщика</Label>
							<Input type="text" name="supplierName" id="supplierName"
								   value={this.supplier.supplierName}
								   onChange={event => {
								   		runInAction(() => {
								   			this.supplier.supplierName = event.target.value.trim();
								   		});
								   }}
							/>
						</FormGroup>
						<FormGroup>
							<Label for="availableMoneyToOrder">Доступная сумма к заказу</Label>
							<Input type="number" name="availableMoneyToOrder" id="availableMoneyToOrder"
								   value={this.supplier.availableMoneyToOrder}
								   onChange={event => {
									   runInAction(() => {
										   this.supplier.availableMoneyToOrder = Number(event.target.value);
									   });
								   }}
							/>
						</FormGroup>
						<FormGroup>
							<FormGroup check>
								<Label check>
									<Input type="checkbox"
										   checked={this.supplier.canMultiSelect}
										   onChange={event => {
											   runInAction(() => {
												   this.supplier.canMultiSelect = Boolean(event.target.checked);
											   });
										   }}
									/> Можно выбрать несколько?
								</Label>
							</FormGroup>
							<FormText color="muted">
								Опция управляет возможностью выбора разных блюд из одной категории.
								Если опция не выбрана, то будет активирован обязательный выбор одного блюда из каждой категории
							</FormText>
						</FormGroup>
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

export default SupplierEditDialog;
