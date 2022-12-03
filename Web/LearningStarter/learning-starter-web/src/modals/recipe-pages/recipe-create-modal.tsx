import "../../modals/modal.css";
import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import {
  Button,
  Dropdown,
  Header,
  Input,
  Modal,
  TextArea,
} from "semantic-ui-react";
import {
  ApiResponse,
  OptionDto,
  RecipeCreateDto,
  RecipeGetDto,
} from "../../constants/types";
import { BaseUrl } from "../../constants/env-cars";
import toast from "react-hot-toast";

function RecipeCreateModal() {
  const [firstOpen, setFirstOpen] = useState(false);
  const [secondOpen, setSecondOpen] = useState(false);
  const [mealTypeOptions, setMealTypeOptions] = useState<OptionDto[]>();
  console.log("debug", mealTypeOptions);
  const [calendarOptions, setCalendarOptions] = useState<OptionDto[]>();
  console.log("debug", calendarOptions);
  const initialValues: RecipeCreateDto = {
    name: "",
    image: "",
    servings: 0,
    directions: "",
    mealTypeId: 0,
    calendarId: 0,
  };

  const onSubmit = async (values: RecipeCreateDto) => {
    const response = await axios.post<ApiResponse<RecipeGetDto>>(
      `${BaseUrl}/api/recipes`,
      values,
      {
        validateStatus: () => true,
      }
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
      setSecondOpen(true);
      toast.success("Recipe successfully created", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
    }
  };

  useEffect(() => {
    async function getMealTypeOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/meal-types/options"
      );

      setMealTypeOptions(response.data.data);
    }

    getMealTypeOptions();
  }, []);

  useEffect(() => {
    async function getCalendarOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/calendars/options"
      );

      setCalendarOptions(response.data.data);
    }

    getCalendarOptions();
  }, []);

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Modal
          as={Form}
          onClose={() => setFirstOpen(false)}
          onOpen={() => setFirstOpen(true)}
          open={firstOpen}
          trigger={
            <Button
              icon="plus circle"
              labelPosition="left"
              content="Recipe"
              positive
              onClick={() => setFirstOpen(true)}
            />
          }
        >
          <Modal.Header>Create Your Recipe</Modal.Header>
          <Modal.Content>
            <Modal.Description>
              <Header>Recipe</Header>
              <div className="field-title">
                <label htmlFor="name">Recipe Name</label>
              </div>
              <div className="field-title">
                <Field id="name" name="name">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>
              <div className="field-title">
                <label htmlFor="image">Image</label>
              </div>
              <div className="field-title">
                <Field id="image" name="image">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>
              <div className="field-title">
                <label htmlFor="servings">Servings</label>
              </div>
              <div className="field-title">
                <Field id="servings" name="servings">
                  {({ field }) => <Input type="number" {...field} />}
                </Field>
              </div>
              <div className="field-title">
                <label htmlFor="directions">Directions</label>
              </div>
              <div className="field-title">
                <Field id="directions" name="directions">
                  {({ field }) => <TextArea {...field} />}
                </Field>
              </div>
              <div className="input-fields">
                <label htmlFor="mealTypeId">Meal Type</label>
              </div>
              <div className="field-title">
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
              <div>
                <label htmlFor="calendarId">Calendar</label>
              </div>
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
            </Modal.Description>
          </Modal.Content>

          <Modal.Actions>
            <Button
              type="button"
              icon="cancel"
              content="Cancel"
              labelPosition="left"
              negative
              onClick={() => setFirstOpen(false)}
            />
            <Button
              type="submit"
              icon="food"
              content="Create Recipe!"
              labelPosition="left"
              positive
            />
          </Modal.Actions>
          <Modal
            onCLose={() => setSecondOpen(false)}
            open={secondOpen}
            size="small"
          >
            <Modal.Header>Success!</Modal.Header>
            <Modal.Content>
              <p>
                You have successfully created a recipe within Syncify. Please
                enjoy!
              </p>
            </Modal.Content>
            <Modal.Actions>
              <Button
                type="button"
                icon="home"
                content="Home"
                labelPosition="left"
                positive
                onClick={() => setFirstOpen(false)}
              />
            </Modal.Actions>
          </Modal>
        </Modal>
      </Formik>
    </>
  );
}

export default RecipeCreateModal;
