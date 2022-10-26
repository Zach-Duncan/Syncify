import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Input } from "semantic-ui-react";
import {
  ApiResponse,
  MealTypeCreateDto,
  MealTypeGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import "./meal-type-create.css";

const initialValues: MealTypeCreateDto = {
  name: "",
};

export const MealTypeCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: MealTypeCreateDto) => {
    const response = await axios.post<ApiResponse<MealTypeGetDto>>(
      `${BaseUrl}/api/meal-types`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.mealTypes.listing);
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
            <div className="meal-type-create-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="meal-type-create-container">
            <Field id="name" name="name" >
              {({ field }) => <Input {...field} />}
            </Field>
            </div>

            <div className="meal-type-create-container">
              <Button type="submit">Create</Button>
            </div>
        </Form>
      </Formik>
    </>
  );
};

