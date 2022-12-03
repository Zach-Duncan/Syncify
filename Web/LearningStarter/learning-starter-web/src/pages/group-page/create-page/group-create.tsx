import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Input } from "semantic-ui-react";
import {
  ApiResponse,
  GroupCreateDto,
  GroupGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import "./group-create.css";
import toast from "react-hot-toast";

const initialValues: GroupCreateDto = {
  name: "",
  image: "",
};

export const GroupCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: GroupCreateDto) => {
    const response = await axios.post<ApiResponse<GroupGetDto>>(
      `${BaseUrl}/api/groups`,
      values
    );

    if (response.data.hasErrors) {
      toast.error("Error Occured, please try again", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.group.listing);
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
          <div className="group-create-container">
            <label htmlFor="name">Name</label>
          </div>
          <div className="group-create-container">
            <Field id="name" name="name">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="ingredient-create-container">
            <label htmlFor="image">Image</label>
          </div>
          <div className="ingredient-create-container">
            <Field id="image" name="image">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="ingredient-create-container">
            <Button type="submit">Create</Button>
          </div>
        </Form>
      </Formik>
    </>
  );
};
