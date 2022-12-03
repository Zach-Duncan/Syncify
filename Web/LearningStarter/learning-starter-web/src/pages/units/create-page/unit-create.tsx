import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Header, Input } from "semantic-ui-react";
import {
  ApiResponse,
  UnitCreateDto,
  UnitGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import "./unit-create.css";
import toast from "react-hot-toast";

const initialValues: UnitCreateDto = {
  name: "",
  abbreviation: "",
};

export const UnitCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: UnitCreateDto) => {
    const response = await axios.post<ApiResponse<UnitGetDto>>(
      `${BaseUrl}/api/units`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.units.listing);
      toast.success("Unit created", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
          <div className="unit-create-container">
            <Header>Create New Unit</Header>
          </div>
          <div className="unit-create-container">
            <label htmlFor="name">Name</label>
          </div>
          <div className="unit-create-container">
            <Field id="name" name="name">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="unit-create-container">
            <label htmlFor="abbreviation">Abbreviation</label>
          </div>
          <div className="unit-create-container">
            <Field id="abbreviation" name="abbreviation">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="unit-create-container">
            <Button
              positive
              icon="check"
              content="Create"
              labelPosition="left"
              type="submit"
            />
          </div>
        </Form>
      </Formik>
    </>
  );
};
