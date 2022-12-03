import "../recipe-ingredient.css";
import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { useHistory } from "react-router-dom";
import { Button, Header, Input } from "semantic-ui-react";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import {
  ApiResponse,
  RecipeIngredientCreateDto,
  RecipeIngredientGetDto,
} from "../../../constants/types";

const initialValues: RecipeIngredientCreateDto = {
  recipeId: 0,
  ingredientId: 0,
  quantity: 0,
  unitId: 0,
};

export const RecipeIngredientsCreatePage = () => {
  const history = useHistory();

  const onSubmit = async (values: RecipeIngredientCreateDto) => {
    const response = await axios.post<ApiResponse<RecipeIngredientGetDto>>(
      `${BaseUrl}/api/recipe-ingredients`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.recipeIngredients.listing);
    }
  };

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
          <div className="recipe-ingredient">
            <Header>Create Recipe Ingredients</Header>
          </div>
          <div className="recipe-ingredient">
            <label htmlFor="recipe.name">Recipe</label>
          </div>
          <div className="recipe-ingredient">
            <Field id="recipe.name" name="recipe.name">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="recipe-ingredient">
            <label htmlFor="ingredient.name">Ingredient</label>
          </div>
          <div className="recipe-ingredient">
            <Field id="ingredient.name" name="ingredient.name">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="recipe-ingredient">
            <label htmlFor="quantity">Quantity</label>
          </div>
          <div className="recipe-ingredient">
            <Field id="quantity" name="quantity">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
          </div>
          <div className="recipe-ingredient">
            <label htmlFor="unit.abbreviation">Unit</label>
          </div>
          <div className="recipe-ingredient">
            <Field id="unit.abbreviation" name="unit.abbreviation">
              {({ field }) => <Input {...field} />}
            </Field>
          </div>
          <div className="recipe-ingredient">
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
