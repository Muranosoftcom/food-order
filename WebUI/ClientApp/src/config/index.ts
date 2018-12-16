export const config = {
	isProduction: process.env.NODE_ENV === "production",
	isAuthenticated: process.env.REACT_APP_IS_AUTHENTICATED === "true",
	backendDomain: process.env.REACT_APP_BACKEND_DOMAIN || "",
	secret: process.env.REACT_APP_JWT_SECRET || "",
	timeUpdateInterval: Number(process.env.REACT_APP_TIME_UPDATE_INTERVAL || 5), // in seconds
};
