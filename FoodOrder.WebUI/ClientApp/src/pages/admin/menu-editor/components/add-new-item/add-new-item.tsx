import React from "react";
import { Button } from "reactstrap";

import { ReactComponent as AddIcon } from "../../../../../images/add.svg";
import "./add-new-item.scss";

interface Props {
	className?: string;
	onClick(): void;
	children: string;
}

export function AddNewItem({ className = "", onClick, children }: Props) {
	return (
		<Button className={`add-new-item ${className}`} onClick={onClick} color="primary" size="sm">
			<AddIcon className="add-new-item__add-icon" />
			<span className="add-new-item__text">{children}</span>
		</Button>
	);
}
