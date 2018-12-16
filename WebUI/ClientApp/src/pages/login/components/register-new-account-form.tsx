import React from "react";
import { observer } from "mobx-react";
import { Form, FormFeedback, FormGroup, Input, Label, Button } from "reactstrap";

export const RegisterNewAccountFormFields = {
	NAME: "name",
	PASSWORD: "password",
};

export const RegisterNewAccountFormSettings = {
	[RegisterNewAccountFormFields.NAME]: {
		name: RegisterNewAccountFormFields.NAME,
		label: "Ваше имя и фамилия",
		placeholder: "Например, John Doe",
		rules: "required|string|between:3,25",
	},

	[RegisterNewAccountFormFields.PASSWORD]: {
		name: RegisterNewAccountFormFields.PASSWORD,
		label: "Пароль",
		placeholder: "Например, ******",
		rules: "required|string|alpha_num|between:5,15",
	},
};

const RegisterNewAccountForm = observer(({ form }) => {
	const nameField = form.$(RegisterNewAccountFormFields.NAME);
	const nameError = nameField.error;
	const nameHasError = Boolean(nameError);

	const passwordField = form.$(RegisterNewAccountFormFields.PASSWORD);
	const passwordError = passwordField.error;
	const passwordHasError = Boolean(passwordError);

	return (
		<Form>
			<FormGroup>
				<Label htmlFor={nameField.id}>{nameField.label}</Label>
				<Input {...nameField.bind()} invalid={nameHasError} valid={nameField.isDirty && !nameHasError} />
				<FormFeedback valid={!nameHasError}>{nameError}</FormFeedback>
			</FormGroup>

			<FormGroup>
				<Label htmlFor={passwordField.id}>{passwordField.label}</Label>
				<Input
					{...passwordField.bind({ type: "password" })}
					invalid={passwordHasError}
					valid={passwordField.isDirty && !passwordHasError}
				/>
				<FormFeedback valid={!passwordHasError}>{passwordError}</FormFeedback>
			</FormGroup>
			<Button
				type="submit"
				disabled={form.isEmpty || !form.isValid}
				onClick={form.onSubmit}
				style={{ width: "100%" }}
			>
				Создать новый аккаунт
			</Button>
		</Form>
	);
});

export default RegisterNewAccountForm;