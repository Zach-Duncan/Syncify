import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Input } from "semantic-ui-react";
import {
  ApiResponse,
  RecipeCreateDto,
  RecipeGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import "./recipe-create.css";

const initialValues: RecipeCreateDto = {
  name: "",
  image: "",
  servings: 0,
  directions: "",
  mealTypeId: 0,
  calendarId: 0,
};

export const RecipeCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: RecipeCreateDto) => {
    const response = await axios.post<ApiResponse<RecipeGetDto>>(
      `${BaseUrl}/api/recipes`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.recipes.listing);
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
          <div className="recipe-create-container">
            <label htmlFor="name">Name</label>
          </div>
          <div className="recipe-create-container">
            <Field id="name" name="name">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="recipe-create-container">
            <label htmlFor="image">Image</label>
          </div>
          <div className="recipe-create-container">
            <Field id="image" name="image">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="recipe-create-container">
            <label htmlFor="servings">Servings</label>
          </div>
          <div className="recipe-create-container">
            <Field id="servings" name="servings">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
          </div>
          <div className="recipe-create-container">
            <label htmlFor="directions">Directions</label>
          </div>
          <div className="recipe-create-container">
            <Field id="directions" name="directions">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="recipe-create-container">
            <label htmlFor="mealTypeId">MealTypeId</label>
          </div>
          <div className="recipe-create-container">
            <Field id="mealTypeId" name="mealTypeId">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
          </div>
          <div className="recipe-create-container">
            <label htmlFor="calendarId">CalendarId</label>
          </div>
          <div className="recipe-create-container">
            <Field id="calendarId" name="calendarId">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
          </div>
          <div className="recipe-create-container">
            <Button type="submit">Create</Button>
          </div>
        </Form>
      </Formik>
    </>
  );
};
