import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import {
  ApiResponse,
  RecipeIngredientGetDto,
  RecipeIngredientUpdateDto,
} from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";

export const RecipeIngredientsUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [recipeIngredient, setRecipeIngredient] =
    useState<RecipeIngredientGetDto>();

  useEffect(() => {
    const fetchRecipeIngredient = async () => {
      const response = await axios.get<ApiResponse<RecipeIngredientGetDto>>(
        `/api/recipe-ingredients/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setRecipeIngredient(response.data.data);
    };

    fetchRecipeIngredient();
  }, [id]);

  const onSubmit = async (values: RecipeIngredientUpdateDto) => {
    const response = await axios.put<ApiResponse<RecipeIngredientGetDto>>(
      `/api/recipe-ingredients/${id}`,
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
      {recipeIngredient && (
        <Formik initialValues={recipeIngredient} onSubmit={onSubmit}>
          <Form>
            <div>
              <label htmlFor="recipeId">Recipe</label>
            </div>
            <Field id="recipeId" name="recipe.name">
              {({ field }) => <Input {...field} />}
            </Field>
            <div>
              <label htmlFor="ingredientId">Ingredient</label>
            </div>
            <Field id="ingredientId" name="ingredient.name">
              {({ field }) => <Input {...field} />}
            </Field>
            <div>
              <label htmlFor="quantity">Quantity</label>
            </div>
            <Field id="quantity" name="quantity">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
            <div>
              <label htmlFor="unitId">Unit</label>
            </div>
            <Field id="unitId" name="unit.name">
              {({ field }) => <Input {...field} />}
            </Field>
            <div>
              <Button type="submit">Submit</Button>
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
