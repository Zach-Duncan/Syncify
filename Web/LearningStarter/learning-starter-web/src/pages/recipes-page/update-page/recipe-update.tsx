import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Dropdown, Header, Input, TextArea } from "semantic-ui-react";
import {
  ApiResponse,
  OptionDto,
  RecipeGetDto,
  RecipeUpdateDto,
} from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./recipe-update.css";
import toast from "react-hot-toast";

export const RecipeUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [recipe, setRecipe] = useState<RecipeGetDto>();
  const [calendarOptions, setCalendarOptions] = useState<OptionDto[]>();
  console.log("debug", calendarOptions);
  const [mealTypeOptions, setMealTypeOptions] = useState<OptionDto[]>();
  console.log("debug", mealTypeOptions);

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

  useEffect(() => {
    async function getCalendarOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/calendars/options"
      );

      setCalendarOptions(response.data.data);
    }

    getCalendarOptions();
  }, []);

  useEffect(() => {
    async function getMealTypeOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/meal-types/options"
      );

      setMealTypeOptions(response.data.data);
    }

    getMealTypeOptions();
  }, []);

  const onSubmit = async (values: RecipeUpdateDto) => {
    const response = await axios.put<ApiResponse<RecipeGetDto>>(
      `/api/recipes/${id}`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.recipes.listing);
      toast.success("Recipe successfully updated", {
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
            <div className="recipe-update-container">
              <Header>Update Recipe</Header>
            </div>
            <div className="recipe-update-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="recipe-update-container">
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="recipe-update-container">
              <label htmlFor="image">Image</label>
            </div>
            <div className="recipe-update-container">
              <Field id="image" name="image">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="recipe-update-container">
              <label htmlFor="servings">Servings</label>
            </div>
            <div className="recipe-update-container">
              <Field id="servings" name="servings">
                {({ field }) => <Input type="number" {...field} />}
              </Field>
            </div>
            <div className="recipe-update-container">
              <label htmlFor="directions">Directions</label>
            </div>
            <div className="recipe-update-container">
              <Field id="directions" name="directions">
                {({ field }) => <TextArea {...field} />}
              </Field>
            </div>
            <div className="recipe-update-container">
              <label htmlFor="mealTypes">Meal Type</label>
            </div>
            <div className="recipe-update-container">
              <Field name="mealTypeId" id="mealTypeId" className="field">
                {({ field, form }) => (
                  <Dropdown
                    selection
                    options={mealTypeOptions}
                    {...field}
                    onChange={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                    onBlur={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                  />
                )}
              </Field>
            </div>
            <div className="recipe-update-container">
              <label htmlFor="calendar">Calendar</label>
            </div>
            <div className="recipe-update-container">
              <Field name="calendarId" id="calendarId" className="field">
                {({ field, form }) => (
                  <Dropdown
                    selection
                    options={calendarOptions}
                    {...field}
                    onChange={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                    onBlur={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                  />
                )}
              </Field>
            </div>
            <div className="recipe-update-container">
              <Button
                positive
                icon="check"
                content="Update"
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
