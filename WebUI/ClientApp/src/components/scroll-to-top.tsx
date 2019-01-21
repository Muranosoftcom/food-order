import React from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";

interface Props extends RouteComponentProps<{}> {}

class ScrollToTop extends React.Component<Props> {
	componentDidUpdate(prevProps: Props) {
		if (this.props.location !== prevProps.location) {
			window.scrollTo(0, 0);
		}
	}

	render() {
		return this.props.children;
	}
}

export default withRouter(ScrollToTop);
