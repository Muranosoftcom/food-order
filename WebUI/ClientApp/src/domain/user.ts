export default class User {
	constructor(
		public readonly fullName: string = "",
		public readonly id: string = "",
		public readonly isAdmin: boolean = false,
		public readonly pictureUrl: string = "",
	) {}
}