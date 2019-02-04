import React from "react";
import { Container } from "reactstrap";

interface Props {
	className: string;
}

const Footer = ({ className }: Props) => {
	return (
		<footer className={`footer ${className}`}>
			<Container>&copy; 2018 Murano Hackathon Food order group</Container>
		</footer>
	);
};

export default Footer;
