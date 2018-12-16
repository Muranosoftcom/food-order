import bcrypt from "bcryptjs";

class CryptoService {
	encodePassword(password: string) {
		return bcrypt.hash(password, 10);
	}
}

export default CryptoService;
