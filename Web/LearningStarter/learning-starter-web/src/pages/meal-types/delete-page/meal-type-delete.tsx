import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import { useRouteMatch } from "react-router-dom";
import { ApiResponse, MealTypeGetDto } from "../../../constants/types";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import toast from "react-hot-toast";

export const MealTypeDeletePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [mealTypes, setMealType] = useState<MealTypeGetDto>();

  useEffect(() => {
    const fetchMealType = async () => {
      const response = await axios.get<ApiResponse<MealTypeGetDto>>(
        `/api/meal-types/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setMealType(response.data.data);
    };

    fetchMealType();
  }, [id]);

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<MealTypeGetDto>>(
      `/api/meal-types/${id}`
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
      history.push(routes.mealTypes.listing);
      toast.success("Meal Type successfully deleted", {
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
      {mealTypes && (
        <Formik initialValues={mealTypes} onSubmit={onSubmit}>
          <Form>
            <div>
              <label htmlFor="name">Name</label>
            </div>
            <Field id="name" name="name">
              {({ field }) => <Input {...field} />}
            </Field>
            <div>
              <Button type="submit">Delete</Button>
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
