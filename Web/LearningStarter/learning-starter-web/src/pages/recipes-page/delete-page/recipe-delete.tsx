import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Header, Input } from "semantic-ui-react";
import { ApiResponse, RecipeGetDto } from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./recipe-delete.css";
import toast from "react-hot-toast";

export const RecipeDeletePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [recipe, setRecipe] = useState<RecipeGetDto>();

  useEffect(() => {
    const fetchRecipe = async () => {
      const response = await axios.get<ApiResponse<RecipeGetDto>>(
        `/api/recipes/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setRecipe(response.data.data);
    };

    fetchRecipe();
  }, [id]);

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<RecipeGetDto>>(
      `/api/recipes/${id}`
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.recipes.listing);
      toast.success("Recipe successfully deleted", {
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
      {recipe && (
        <Formik initialValues={recipe} onSubmit={onSubmit}>
          <Form>
            <div className="recipe-delete-container">
              <Header>Delete Recipe</Header>
            </div>
            <div className="recipe-delete-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="recipe-delete-container">
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="recipe-delete-container">
              <label htmlFor="image">Image</label>
            </div>
            <div className="recipe-delete-container">
              <Field id="image" name="image">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="recipe-delete-container">
              <label htmlFor="servings">Servings</label>
            </div>
            <div className="recipe-delete-container">
              <Field id="servings" name="servings">
                {({ field }) => <Input type="number" {...field} />}
              </Field>
            </div>
            <div className="recipe-delete-container">
              <label htmlFor="directions">Directions</label>
            </div>
            <div className="recipe-delete-container">
              <Field id="directions" name="directions">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="recipe-delete-container">
              <label htmlFor="mealType.name">Meal Type</label>
            </div>
            <div className="recipe-delete-container">
              <Field name="mealType.name" id="mealType.name" className="field">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="recipe-delete-container">
              <label htmlFor="calendar.group.name">Calendar</label>
            </div>
            <div className="recipe-delete-container">
              <Field
                name="calendar.group.name"
                id="calendar.group.name"
                className="field"
              >
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="recipe-delete-container">
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
