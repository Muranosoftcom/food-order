import React, { ReactNode } from "react";
import { Collapse } from "reactstrap";

import { ReactComponent as ChevronDown } from "../../images/chevron-down.svg";
import { ReactComponent as ChevronUp } from "../../images/chevron-up.svg";

import "./index.scss";

interface Props {
	className: string;
	headerClassName: string;
	caption: ReactNode;
	defaultOpen: boolean;
}

interface State {
	isOpen: boolean;
}

class Expander extends React.Component<Props, State> {
	public static defaultProps: Pick<Props, "defaultOpen"> = {
		defaultOpen: false,
	};

	public state = {
		isOpen: this.props.defaultOpen,
	};

	private toggleOpen = () => {
		this.setState(prevState => ({ isOpen: !prevState.isOpen }));
	};

	private renderChevron() {
		return this.state.isOpen
			? <ChevronDown className="expander__chevron expander__chevron-down" />
			: <ChevronUp className="expander__chevron expander__chevron-up" />;
	}

	public render() {
		const { isOpen } = this.state;
		const { className, headerClassName, caption, children } = this.props;

		return (
			<div className={className}>
				<div
					className={`expander ${headerClassName}`}
					onClick={this.toggleOpen}
				>
					{this.renderChevron()}{caption}
				</div>
				<Collapse isOpen={isOpen}>
					<div className="expander__content">{children}</div>
				</Collapse>
			</div>
		);
	}
}

export default Expander;
