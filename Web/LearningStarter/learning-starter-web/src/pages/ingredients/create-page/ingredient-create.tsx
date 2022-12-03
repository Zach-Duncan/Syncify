import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Header, Input } from "semantic-ui-react";
import {
  ApiResponse,
  IngredientCreateDto,
  IngredientGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import "./ingredient-create.css";
import toast from "react-hot-toast";

const initialValues: IngredientCreateDto = {
  name: "",
  image: "",
};

export const IngredientCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: IngredientCreateDto) => {
    const response = await axios.post<ApiResponse<IngredientGetDto>>(
      `${BaseUrl}/api/ingredients`,
      values
    );

    if (response.data.hasErrors) {
      toast.error("Error Occured", {
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
      history.push(routes.ingredients.listing);
      toast.success("Ingredient created", {
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
          <div className="ingredient-create-container">
            <Header>Create New Ingredient</Header>
          </div>
          <div className="ingredient-create-container">
            <label htmlFor="name">Name</label>
          </div>
          <div className="ingredient-create-container">
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
