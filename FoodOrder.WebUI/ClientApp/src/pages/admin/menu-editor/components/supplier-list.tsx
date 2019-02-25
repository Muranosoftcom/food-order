import React, { Component } from "react";

import { SupplierDto } from "../../../../domain/dto";
import { SupplierView } from "./supplier-view/supplier-view";

interface Props {
	suppliers: SupplierDto[];
	onEditSupplier(s: SupplierDto): void;
	onDeleteSupplier(supplierId: string): void;
}

interface State {
	activeSupplier: SupplierDto | null;
}

export class SuppliersList extends Component<Props, State> {
	state: State = {
		activeSupplier: null,
	};

	private handleEdit = (supplier: SupplierDto) => {
		this.setState({
			activeSupplier: supplier,
		});

		this.props.onEditSupplier(supplier);
	};

	private handleDelete = (supplierId: string) => {
		this.props.onDeleteSupplier(supplierId);

		this.setState({
			activeSupplier: null,
		});
	};

	public render() {
		return (
			<div>
				{this.props.suppliers.map(supplier => (
					<SupplierView
						key={supplier.supplierId}
						supplier={supplier}
						selectedSupplier={this.state.activeSupplier}
						onEdit={this.handleEdit}
						onDelete={this.handleDelete}
					/>
				))}
			</div>
		)
	}
}