import { action, computed, observable } from "mobx";

import AppStore from "../../../store/app-store";
import PageStore from "../../../store/page-store";
import FormStoreBase from "./form-store-base";
import { RegisterNewAccountFormSettings } from "../components/register-new-account-form";
import { UserDto } from "../../../domain/dto";

interface Option {
	label: string;
	value: string;
}

export class LoginFormStore {
	constructor(private readonly users: UserDto[], private readonly appStore: AppStore) {}

	@observable
	selectedUserId: string | null = null;

	@observable
	selectedOption: Option | null = null;

	@computed
	public get options() {
		return this.users.map(user => ({
			label: user.name,
			value: user.id,
		}));
	}

	@computed
	get selectedUser() {
		if (!this.selectedOption) {
			return null
		}

		return this.users.find(user => user.id === this.selectedOption!.value) || null;
	}

	@action.bound
	onChange = (option: any) => {
		this.selectedOption = option;
	};

	@action.bound
	onSubmit = async () => {
		if (this.selectedUser) {
			const { name, password } = this.selectedUser;
			await this.appStore.operationManager.runWithProgress(() => {
				return this.appStore.identity.login(name, password)
			})
		}
	};
}

class LoginPageStore extends PageStore {
	@observable
	public signInForm: FormStoreBase;
	constructor(appStore: AppStore) {
		super(appStore);

		this.signInForm = new FormStoreBase(RegisterNewAccountFormSettings, async (form: any) => {
			const { name, password } = form.values();

			await this.appStore.operationManager.runWithProgress(() => {
				return appStore.identity.signIn(name, password);
			});

			form.clear();
			this.onShowRegisterNewForm()
		});
	}

	@computed
	get isAuthenticated() {
		return this.appStore.identity.isAuthenticated;
	};

	@observable
	isRegisterNew = false;

	@action.bound
	onShowRegisterNewForm() {
		this.isRegisterNew = !this.isRegisterNew;
	};

	@computed
	public get loginForm() {
		const { users } = this.appStore.identity;

		if(users.length > 0) {
			return new LoginFormStore(users, this.appStore);
		}

		return null;
	};

	@computed
	public get showLoginForm() {
		return !this.isRegisterNew && this.loginForm !== null;
	}

	@computed
	public get showSignInForm() {
		return this.isRegisterNew;
	}
}

export default LoginPageStore;
