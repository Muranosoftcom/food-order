import React from "react";

import visibleOnlyForAuthenticatedUser from "../../hocs/visible-only-for-authenticated-user";

class MyOrder extends React.Component {
	render() {
		return (
			<section>
				<h1 className="page-heading">Мой заказ на сегодня</h1>
				<hr />
				<p>Здесь будет перечень заказанных вами обедов</p>
				<p>А также обед который вы могли забронировать в таблице расшаренных</p>
			</section>
		);
	}
}

export default visibleOnlyForAuthenticatedUser(MyOrder);
