import React, { Component } from "react";
import { Button, Col, Row } from "reactstrap";
import { inject, observer } from "mobx-react";
import MenuEditorPageStore from "./store/menu-editor-page-store";
import { AddNewItem } from "./components/add-new-item/add-new-item";
import SupplierEditDialog from "./components/dialogs/supplier-edit-dialog";
import { SuppliersList } from "./components/supplier-list";

import "./index.scss";
import DishEditDialog from "./components/dialogs/dish-edit-dialog";

interface Props {
	menuEditorPageStore?: MenuEditorPageStore
}

@inject("menuEditorPageStore")
@observer
class MenuEditorPage extends Component<Props> {
	public handleSync = () => {
		// this.props.adminPageStore!.syncMenu();
	};

	private handleSave = () => {
		this.props.menuEditorPageStore!.submitMenuEditing();
	};

	public render() {
		const { menuEditorPageStore } = this.props;
		const { currentUser } = menuEditorPageStore!.appStore.identity;
		const { dishStore, supplierStore } = menuEditorPageStore!;

		return (
			currentUser && currentUser.isAdmin &&
				<>
					<Row>
						<Col>
							<h1 className="page-heading">Редактор меню</h1>
						</Col>
						<Col className="d-flex justify-content-end">
							<Button color="link" onClick={this.handleSave} disabled>Сохранить изменения</Button>
						</Col>
					</Row>
					<Row>
						<Col className="menu-editor">
							<SuppliersList
								suppliers={menuEditorPageStore!.suppliers}
								onEditSupplier={supplierStore.editSupplier}
								onDeleteSupplier={supplierStore.deleteSupplier}
							/>
							<div className="menu-editor__add-new">
								<AddNewItem onClick={supplierStore.addSupplier}>Добавить поставщика</AddNewItem>
							</div>
						</Col>
					</Row>
					{supplierStore.isSupplierEditorVisible &&
						<SupplierEditDialog
							supplier={supplierStore.supplierToEdit!}
							onSave={supplierStore.saveSupplier}
							onCancel={supplierStore.cancelEditSupplier}
						/>
					}
					{dishStore.dishEditorVisible &&
						<DishEditDialog
							dish={dishStore.dishToEdit!}
							onSave={dishStore.save}
							onCancel={dishStore.cancelEdit}
						/>
					}
				</>
		);
	}
}

export default MenuEditorPage;