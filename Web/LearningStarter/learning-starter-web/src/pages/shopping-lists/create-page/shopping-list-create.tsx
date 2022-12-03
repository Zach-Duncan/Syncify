import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Header, Input } from "semantic-ui-react";
import {
  ApiResponse,
  ShoppingListCreateDto,
  ShoppingListGetDto,
} from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
import "./shopping-list-create.css";
import toast, { Toaster } from "react-hot-toast";

const initialValues: ShoppingListCreateDto = {
  name: "",
};
function save(
  ShoppingListCreatePage: () => JSX.Element
): Promise<ShoppingListCreateDto> {
  throw new Error("May Already Exist Try Again");
}
export const ShoppingListCreatePage = () => {
  const notify = () => {
    toast.success("Shopping List item created", {
      position: "top-center",
      style: {
        background: "#333",
        color: "#fff",
      },
    });
  };
  const history = useHistory();
  const onSubmit = async (values: ShoppingListCreateDto) => {
    const response = await axios.post<ApiResponse<ShoppingListGetDto>>(
      `${BaseUrl}/api/shopping-lists`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
        toast.error("Error Occured", {
          position: "top-center",
          style: {
            background: "#333",
            color: "#fff",
          },
        });
        <Header>Error has occured please try again</Header>;
      });
    } else {
      history.push(routes.shoppingLists.listing);
      toast.success("Shopping List item created", {
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
        <div className="shopping-list-create-container">
          <Form>
            <Header>Create Shopping List Item</Header>
            <div className="shopping-list-create-container">
              <label htmlFor="name">Name</label>
            </div>
            <Field id="name" name="name">
              {({ field }) => <Input {...field} />}
            </Field>
            <div className="shopping-list-create-container">
              <Button type="submit">Create</Button>
              <Toaster />
            </div>
          </Form>
        </div>
      </Formik>
    </>
  );
};
