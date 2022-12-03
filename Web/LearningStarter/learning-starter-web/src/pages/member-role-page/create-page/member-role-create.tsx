import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Input } from "semantic-ui-react";
import {
  ApiResponse,
  MemberRoleCreateDto,
  MemberRoleGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import "./member-role-create.css";

const initialValues: MemberRoleCreateDto = {
  name: "",
};

export const MemberRoleCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: MemberRoleCreateDto) => {
    const response = await axios.post<ApiResponse<MemberRoleGetDto>>(
      `${BaseUrl}/api/member-roles`,
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
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
            <div className="member-role-create-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="member-role-create-container">
            <Field id="name" name="name" >
              {({ field }) => <Input {...field} />}
            </Field>
            </div>

            <div className="member-role-create-container">
              <Button type="submit">Create</Button>
            </div>
        </Form>
      </Formik>
    </>
  );
};
