import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import {
  ApiResponse,
  UserCreateDto,
  UserGetDto,
  UserUpdateDto,
} from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./user-update.css";

export const UsersUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [user, setUser] = useState<UserGetDto>();

  useEffect(() => {
    const fetchUser = async () => {
      const response = await axios.get<ApiResponse<UserGetDto>>(
        `/api/users/${id}`
      );

      // const fetchUserOptions = async() => {
      //   const response = await axios.get<ApiResponse<UserGetDto>>(
      //     `/api/users/${id}`
      // };

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setUser(response.data.data);
    };

    fetchUser();
  }, [id]);

  const onSubmit = async (values: UserUpdateDto) => {
    const response = await axios.put<ApiResponse<UserGetDto>>(
      `/api/users/${id}`,
      values
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
        <Formik
          initialValues={{ ...user } as unknown as UserCreateDto}
          onSubmit={onSubmit}
        >
          <Form>
            <div className="user-update-container">
              <label htmlFor="profileColor.colors">Profile Color</label>
            </div>
            <div className="user-update-container">
              <Field id="profileColor.colors" name="profileColor.colors">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-update-container">
              <label htmlFor="firstName">First Name</label>
            </div>
            <div className="user-update-container">
              <Field id="firstName" name="firstName">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-update-container">
              <label htmlFor="lastName">Last Name</label>
            </div>
            <div className="user-update-container">
              <Field id="lastName" name="lastName">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-update-container">
              <label htmlFor="username">Username</label>
            </div>
            <div className="user-update-container">
              <Field id="username" name="username">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-update-container">
              <label htmlFor="phoneNumber">Phone Number</label>
            </div>
            <div className="user-update-container">
              <Field id="phoneNumber" name="phoneNumber">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="user-update-container">
              <label htmlFor="email">Email</label>
            </div>
            <div className="user-update-container">
              <Field id="email" name="email">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>

            <div className="user-update-container">
              <Button type="submit">Submit</Button>
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
