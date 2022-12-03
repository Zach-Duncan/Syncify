import axios from "axios";
import { Field, Form, Formik } from "formik";
import { useUser } from "../../../authentication/use-auth";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import { ApiResponse, UserGetDto } from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "../delete-page/user-delete.css";

export const UsersDeletePage = () => {
  const history = useHistory();
  const userDelete = useUser();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [user, setUser] = useState<UserGetDto>();

  useEffect(() => {
    const fetchUser = async () => {
      const response = await axios.get<ApiResponse<UserGetDto>>(
        `/api/users/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setUser(response.data.data);
    };

    fetchUser();
  }, [id]);

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<UserGetDto>>(
      `/api/users/${id}`
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.users.listing);
    }
  };

  return (
    <>
      {user && (
        <Formik initialValues={user} onSubmit={onSubmit}>
          <Form>
            <div className="user-delete-container">
              <label htmlFor="profileColor.colors">Profile Color</label>
            </div>
            <div className="user-delete-container">
              <Field id="profileColor.colors" name="profileColor.colors">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-delete-container">
              <label htmlFor="firstName">First Name</label>
            </div>
            <div className="user-delete-container">
              <Field id="firstName" name="firstName">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-delete-container">
              <label htmlFor="lastName">Last Name</label>
            </div>
            <div className="user-delete-container">
              <Field id="lastName" name="lastName">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-delete-container">
              <label htmlFor="username">Username</label>
            </div>
            <div className="user-delete-container">
              <Field id="username" name="username">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-delete-container">
              <label htmlFor="phoneNumber">Phone Number</label>
            </div>
            <div className="user-delete-container">
              <Field id="phoneNumber" name="phoneNumber">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-delete-container">
              <label htmlFor="email">Email</label>
            </div>
            <div className="user-delete-container">
              <Field id="email" name="email">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>

            <div className="user-delete-container">
              <Button
                negative
                icon="trash"
                content="Delete"
                labelPosition="left"
                type="submit"
              />
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
