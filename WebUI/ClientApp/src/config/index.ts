export const config = {
	isProduction: process.env.NODE_ENV === "production",
	backendDomain: process.env.REACT_APP_BACKEND_DOMAIN || "",
	timeUpdateInterval: Number(process.env.REACT_APP_TIME_UPDATE_INTERVAL || 5), // in seconds
	googleClientId: process.env.REACT_APP_GOOGLE_CLIENT_ID,
	secret: process.env.REACT_APP_JWT_SECRET || "",
};
