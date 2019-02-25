import React, { Component, MouseEvent } from "react";
import { Button } from "reactstrap";

import { ReactComponent as DeleteIcon } from "../../../../../images/delete.svg";

interface Props {
	confirmMessage: string;
	onDelete(): void;
}

export class DeleteButton extends Component<Props> {
	private handleClick = (e: MouseEvent<HTMLButtonElement>) => {
		e.stopPropagation();

		if (window.confirm(this.props.confirmMessage)) {
			this.props.onDelete();
		}
	};

	public render() {
		return (
			<Button
				className="command-button"
				outline color="info"
				onClick={this.handleClick}
			>
				<DeleteIcon className="command-button__icon" />
			</Button>
		);
	}
}


