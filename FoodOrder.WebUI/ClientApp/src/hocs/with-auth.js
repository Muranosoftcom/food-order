import React from "react";

import { inject, observer } from "mobx-react";

function withAuth(Control) {
	return inject("appStore")(
		observer(
			class extends React.Component {
				handleClick = () => {
					if (!this.props.appStore.identity.isAuthenticated) {
						alert("Переходим на страницу аутентификации");
						return;
					}
					console.log(this.props);
					this.props.onClick();
				};

				render() {
					const { onClick, ...rest } = this.props;

					return <Control {...rest} onClick={this.handleClick} />;
				}
			},
		),
	);
}

export default withAuth;
