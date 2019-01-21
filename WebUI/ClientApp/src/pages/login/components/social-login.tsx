import React from "react";
// @ts-ignore
import SocialLogin from "react-social-login";

// @ts-ignore
const Button = ({ children, triggerLogin, ...props }) => (
	<button onClick={triggerLogin} {...props}>
		{children}
	</button>
);

export default SocialLogin(Button);
