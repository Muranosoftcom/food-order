import React from "react";

import "./supplier-badge.scss";

interface Props {
	supplierName: string;
}

const SupplierBadge: React.FC<Props> = ({ supplierName }) => {
	return <div className="supplier-badge">{supplierName}</div>;
};

export default SupplierBadge;
