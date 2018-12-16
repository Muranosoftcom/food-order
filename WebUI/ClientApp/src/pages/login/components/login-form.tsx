import React from "react";
import { observer } from "mobx-react";
import Select from "react-select";
import { Form, FormGroup, Button, FormText } from "reactstrap";
import { LoginFormStore } from "../store/store";

interface Props {
	form: LoginFormStore;
}

const LoginForm: React.FC<Props> = ({ form }) => {
	const { selectedOption, options, onChange, selectedUser } = form;
	return (
		<Form tag="div">
			<FormGroup>
				<Select placeholder="Выбор пользователя" value={selectedOption} options={options} onChange={onChange} />
				<FormText color="muted">Не переживайте этот список виден лишь в тестовом режиме</FormText>
			</FormGroup>
			<Button
				type="button"
				color="primary"
				disabled={!selectedUser}
				onClick={form.onSubmit}
				style={{ width: "100%" }}
			>
				Вход
			</Button>
		</Form>
	);
};

export default observer(LoginForm);
