import SyncifyImg from "../../../assets/Syncify.png";
import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Header, Input, Modal } from "semantic-ui-react";
import {
  ApiResponse,
  UserCreateDto,
  UserGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";

function UserCreateModal() {
  const [open, setOpen] = React.useState(false);
  const initialValues: UserCreateDto = {
    profileColorId: 0,
    firstName: "",
    lastName: "",
    username: "",
    password: "",
    phoneNumber: "",
    email: "",
    birthday: "",
  };

  const history = useHistory();

  const onSubmit = async (values: UserCreateDto) => {
    const response = await axios.post<ApiResponse<UserGetDto>>(
      `${BaseUrl}/api/users`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.home);
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Modal
          onClose={() => setOpen(false)}
          onOpen={() => setOpen(true)}
          open={open}
          trigger={<Button>Register</Button>}
        >
          <Modal.Header>Register For Membership</Modal.Header>
          <Modal.Content image>
            <img sizes="medium" src={SyncifyImg} alt="Syncify" />
            <Modal.Description>
              <Header>Sign Up</Header>
              <Form>
                <div className="input-fields">
                  <label htmlFor="profileColorId">Profile Color</label>
                </div>
                <div className="field-title">
                  <Field id="profileColorId" name="profileColorId">
                    {({ field }) => <Input type="number" {...field} />}
                  </Field>
                </div>
                <div className="field-title">
                  <label htmlFor="firstName">First Name</label>
                </div>
                <div className="field-title">
                  <Field id="firstName" name="firstName">
                    {({ field }) => <Input {...field} />}
                  </Field>
                </div>
                <div className="field-title">
                  <label htmlFor="lastName">Last Name</label>
                </div>
                <div className="field-title">
                  <Field id="lastName" name="lastName">
                    {({ field }) => <Input {...field} />}
                  </Field>
                </div>
                <div className="field-title">
                  <label htmlFor="username">Username</label>
                </div>
                <div className="field-title">
                  <Field id="username" name="username">
                    {({ field }) => <Input {...field} />}
                  </Field>
                </div>
                <div className="field-title">
                  <label htmlFor="password">Password</label>
                </div>
                <div className="field-title">
                  <Field id="password" name="password">
                    {({ field }) => <Input {...field} />}
                  </Field>
                </div>
                <div className="field-title">
                  <label htmlFor="phoneNumber">Phone Number</label>
                </div>
                <div className="field-title">
                  <Field id="phoneNumber" name="phoneNumber">
                    {({ field }) => <Input {...field} />}
                  </Field>
                </div>
                <div className="field-title">
                  <label htmlFor="email">Email</label>
                </div>
                <div className="field-title">
                  <Field id="email" name="email">
                    {({ field }) => <Input {...field} />}
                  </Field>
                </div>
                <div className="field-title">
                  <label htmlFor="birthday">Birthday</label>
                </div>
                <div className="field-title">
                  <Field id="birthday" name="birthday">
                    {({ field }) => <Input {...field} />}
                  </Field>
                </div>
                <Button type="submit" onClick={() => setOpen(false)}>
                  Register
                </Button>

                <Button onClick={() => setOpen(false)}> Cancel</Button>
              </Form>
            </Modal.Description>
          </Modal.Content>
        </Modal>
      </Formik>
    </>
  );
}

export default UserCreateModal;
