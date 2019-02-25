import React, { MouseEvent, Component } from "react";
import { Button } from "reactstrap";

import { ReactComponent as EditIcon } from "../../../../../images/edit.svg";

interface Props {
	onClick(): void;
}

export class EditButton extends Component<Props> {
	private handleClick = (e: MouseEvent<HTMLButtonElement>) => {
		e.stopPropagation();

		this.props.onClick();
	};

	public render() {
		return (
			<Button
				outline
				color="info"
				className="command-button"
				onClick={this.handleClick}>
				<EditIcon className="command-button__icon" />
			</Button>
		);
	}
}

