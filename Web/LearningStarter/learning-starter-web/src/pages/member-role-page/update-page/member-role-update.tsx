import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import {
	ApiResponse,
	MemberRoleGetDto,
	MemberRoleUpdateDto,
} from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";

export const MemberRoleUpdatePage = () => {
	const history = useHistory();
	let match = useRouteMatch<{ id: string }>();
	const id = match.params.id;
	const [memberRole, setMemberRole] = useState<MemberRoleGetDto>();

	useEffect(() => {
		const fetchMemberRole = async () => {
			const response = await axios.get<ApiResponse<MemberRoleGetDto>>(
				`/api/member-roles/${id}`
			);

			if (response.data.hasErrors) {
				console.log(response.data.errors);
				return;
			}

			setMemberRole(response.data.data);
		};

		fetchMemberRole();
	}, [id]);

	const onSubmit = async (values: MemberRoleUpdateDto) => {
		const response = await axios.put<ApiResponse<MemberRoleGetDto>>(
			`/api/member-roles/${id}`,
			values
		);

		if (response.data.hasErrors) {
			response.data.errors.forEach((err) => {
				console.log(err.message);
			});
		} else {
			history.push(routes.memberRoles.listing);
		}
	};

	return (
		<>
			{memberRole && (
				<Formik initialValues={memberRole} onSubmit={onSubmit}>
					<Form>
						<div>
							<label htmlFor="name">Name</label>
						</div>
						<Field id="name" name="name">
							{({ field }) => <Input {...field} />}
						</Field>
						<div>
							<Button type="submit">Submit</Button>
						</div>
					</Form>
				</Formik>
			)}
		</>
	);
};
