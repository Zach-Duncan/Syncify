import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import { ApiResponse, MemberRoleGetDto } from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./member-role-delete.css";

export const MemberRoleDeletePage = () => {
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

	const onSubmit = async () => {
		const response = await axios.delete<ApiResponse<MemberRoleGetDto>>(
			`/api/member-roles/${id}`
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
						<div className="member-role-delete-container">
							<label htmlFor="name">Name</label>
						</div>
						<div className="member-role-delete-container">
							<Field id="name" name="name">
								{({ field }) => <Input {...field} />}
							</Field>
						</div>
						<div className="member-role-delete-container">
							<Button type="submit">Delete</Button>
						</div>
					</Form>
				</Formik>
			)}
		</>
	);
};
