import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Header, Input } from "semantic-ui-react";
import { ApiResponse, ShoppingListGetDto } from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./shopping-list-delete.css";
import toast from "react-hot-toast";

export const ShoppingListDeletePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [shoppingList, setShoppingList] = useState<ShoppingListGetDto>();

  useEffect(() => {
    const fetchShoppingList = async () => {
      const response = await axios.get<ApiResponse<ShoppingListGetDto>>(
        `/api/shopping-lists/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setShoppingList(response.data.data);
    };

    fetchShoppingList();
  }, [id]);

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<ShoppingListGetDto>>(
      `/api/shopping-lists/${id}`
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.shoppingLists.listing);
      toast.success("Shopping List item deleted", {
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
      {shoppingList && (
        <Formik initialValues={shoppingList} onSubmit={onSubmit}>
          <Form>
            <div className="shopping-list-delete-container">
              <Header>Delete Shopping List Item</Header>
            </div>
            <div className="shopping-list-delete-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="shopping-list-delete-container">
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="shopping-list-delete-container">
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
